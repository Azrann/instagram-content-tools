using System;
using System.Collections.Generic;

namespace InstagramToolsLibrary
{
    public class InstagramItemContentData
    {
        public InstagramItemContentData(List<string> data)
        {
            UploadTime = DateTime.Parse(data[0]);
            PostUrl = data[1];
            CommentCount = int.Parse(data[2]);
            LikeCount = int.Parse(data[3]);
            ImageUrl = data[4];
            VideoUrl = data[5];
            ViewCount = !string.IsNullOrEmpty(data[5]) ? int.Parse(data[6]) : 0;
            IsSideCar = !string.IsNullOrEmpty(data[7]) && bool.Parse(data[7]);
            Username = data.Count > 8 ? data[8] : "";
            Description = data.Count > 9 ? data[9] : "";
            Location = data.Count > 10 ? data[10] : "";
            Engagement = data.Count > 11 ? int.Parse(data[11]) : 0;
        }

        public DateTime UploadTime { get; set; }
        public string PostUrl { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public int ViewCount { get; set; }
        public bool IsSideCar { get; set; }
        public string Username { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int TotalEngagement => LikeCount + CommentCount;
        public double Engagement { get; set; }

        public string ToLineString()
        {
            return $"{UploadTime},{PostUrl},{CommentCount},{LikeCount},{ImageUrl},{VideoUrl},{ViewCount},{IsSideCar},{Username},\"{Description.Replace("\"","\"\"")}\",\"{Location}\",{Engagement:0.}";
        }

        public string Key()
        {
            var result = $"{UploadTime},{CommentCount},{LikeCount},{Username},\"{Location}\",\"{Description.Replace("\"", "\"\"")}\"".Trim();
            return result.Substring(0, Math.Min(300, result.Length - 1));
        }

        public string ContentUrl()
        {
            if (IsVideo())
            {
                return VideoUrl;
            }

            return ImageUrl;
        }

        public bool IsVideo()
        {
            if (string.IsNullOrEmpty(VideoUrl))
            {
                return false;
            }

            return true;
        }
    }
}