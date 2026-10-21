using System;
using System.Collections.Generic;
using System.Linq;

namespace InstagramToolsLibrary
{
    public class ImageFilter
    {
        private List<InstagramImageData> _data;

        public ImageFilter(List<List<string>> data)
        {
            if (!DateTime.TryParse(data[0][0], out DateTime test))
            {
                data.RemoveAt(0);
            }
            _data = data.Select(x=> new InstagramImageData(x)).ToList();
        }

        public List<InstagramImageData> GetTopImages(int groupCount = 20, int selectionTopCount = 5)
        {
            List<InstagramImageData> result = new List<InstagramImageData>();
            _data = _data.OrderByDescending(x =>x.UploadTime).ToList();

            int processed = 0;

            while (processed < _data.Count)
            {
                var bestImages = GetBestImages(_data.Skip(processed).Take(groupCount), selectionTopCount);
                result.AddRange(bestImages);
                processed += groupCount;
            }
            return result;
        }

        private IEnumerable<InstagramImageData> GetBestImages(IEnumerable<InstagramImageData> images, int takeCount)
        {
            images = images.OrderByDescending(x => x.TotalEngagement);
            var average = images.Average(x => x.TotalEngagement);
            foreach (var image in images)
            {
                image.Engagement = image.TotalEngagement / average;
            }
            return images.Take(takeCount).ToList();
        }
    }

    public class InstagramImageData
    {
        public InstagramImageData(List<string> data)
        {
            UploadTime = DateTime.Parse(data[0]);
            PostUrl = data[1];
            CommentCount = int.Parse(data[2]);
            LikeCount = int.Parse(data[3]);
            ImageUrl = data[4];
        }

        public DateTime UploadTime { get; set; }
        public string PostUrl { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public string ImageUrl { get; set; }
        public int TotalEngagement => LikeCount + CommentCount;
        public double Engagement { get; set; }

        public string ToLineString()
        {
            return $"{UploadTime},{ImageUrl},{Engagement:0.##},{PostUrl},{LikeCount},{CommentCount}";
        }
    }
}
