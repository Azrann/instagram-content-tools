using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace InstagramToolsLibrary
{
    public class ContentDownloader
    {
        private List<InstagramItemContentData> _data;

        public List<InstagramItemContentData> Data => _data;

        public ContentDownloader(List<List<string>> data)
        {
            if (!DateTime.TryParse(data[0][0], out DateTime test))
            {
                data.RemoveAt(0);
            }

            _data = data.Select(x => new InstagramItemContentData(x)).ToList();
        }

        public List<InstagramItemContentData> DownloadTop(string myUsername, int numDownloads = 20,
            string usernameFilter = null, bool imagesOnly = false)
        {
            Directory.CreateDirectory($"\\Content\\{myUsername}\\Top");
            Directory.CreateDirectory($"\\Content\\{myUsername}\\Normal");
            List<InstagramItemContentData> result = new List<InstagramItemContentData>();
            var data = _data.OrderByDescending(x => x.Engagement).ToList();

            if (usernameFilter != null)
            {
                data = data.Where(x => x.Username == usernameFilter).ToList();
            }

            int processed = 0;
            int downloaded = 0;
            while (processed < data.Count && downloaded < numDownloads)
            {
                var content = data[processed];

                if (imagesOnly && content.IsVideo())
                {
                    processed++;
                    continue;
                }

                var contentFile = GetContentFile(content, myUsername);
                var contentFileCompleted = GetContentFileCompleted(content, myUsername);
                var contentFileError = GetContentFileError(content, myUsername);
                if (!File.Exists(contentFile) && !File.Exists(contentFileCompleted) && !File.Exists(contentFileError))
                {
                    try
                    {
                        LinkDownloader.DownloadFile(content.ContentUrl(), GetContentFile(content, myUsername));
                        Random r = new Random();
                        var time = r.Next(1, 1000);
                        downloaded++;
                        Thread.Sleep(time);
                    }
                    catch (Exception e)
                    {
                        WriteFileError(GetContentFile(content, myUsername),
                            GetContentFileErrorDirectory(content, myUsername), e.Message);
                    }
                }

                processed++;
            }

            return result;
        }

        private string GetIdFromUrl(string url)
        {
            return url.Replace("https://www.instagram.com/p/", "").Replace("/", "");
        }

        private void WriteFileError(string filePath, string errorPath, string error)
        {
            var errorTextFile = errorPath + "errorDownload.txt";
            if (!File.Exists(errorTextFile))
            {
                using (FileStream fs = File.Create(errorTextFile))
                {
                }
            }

            using (StreamWriter w = File.AppendText(errorTextFile))
            {
                w.WriteLine($"{filePath}->{error}");
            }
        }

        private string GetFolder(InstagramItemContentData content)
        {
            return content.Engagement > 100 ? "top" : "normal";
        }

        private string GetContentFileCompleted(InstagramItemContentData content, string myUsername)
        {
            return
                $"/Content/{myUsername}/{GetFolder(content)}/Completed/{GetContentFileName(content)}";
        }

        private string GetContentFileError(InstagramItemContentData content, string myUsername)
        {
            return
                $"/Content/{myUsername}/{GetFolder(content)}/Error/{GetContentFileName(content)}";
        }

        private string GetContentFileCompletedDirectory(InstagramItemContentData content, string myUsername)
        {
            return $"/Content/{myUsername}/{GetFolder(content)}/Completed/";
        }

        private string GetContentFileErrorDirectory(InstagramItemContentData content, string myUsername)
        {
            return $"/Content/{myUsername}/{GetFolder(content)}/Error/";
        }

        private string GetContentFile(InstagramItemContentData content, string myUsername)
        {
                return
                    $"/Content/{myUsername}/{GetFolder(content)}/{GetContentFileName(content)}";
        }

        private string GetContentFileName(InstagramItemContentData content)
        {
            if (!content.IsSideCar)
            {
                return
                    $"{GetIdFromUrl(content.PostUrl)}.{GetFileExtension(content)}";
            }

            return $"{GetIdFromUrl(content.PostUrl)}-{GetCarrouselId(content)}.{GetFileExtension(content)}";

        }

        private string GetCarrouselId(InstagramItemContentData content)
        {
            var parts = content.ContentUrl().Split("?");
            var partsBySeparator = parts[0].Split("_");
            return partsBySeparator[partsBySeparator.Length - 2];
        }

        private string GetFileExtension(InstagramItemContentData content)
        {
            if (string.IsNullOrEmpty(content.VideoUrl))
            {
                return "jpeg";
            }

            return "mp4";
        }
    }
}