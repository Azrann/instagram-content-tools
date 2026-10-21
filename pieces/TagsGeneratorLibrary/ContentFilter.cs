using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InstagramToolsLibrary
{
    public class ContentFilter
    {
        private InstagramContentData instagramContentData;

        public ContentFilter(List<List<string>> data)
        {
            instagramContentData = new InstagramContentData(data);
        }

        public List<InstagramItemContentData> GetTopImages(int groupCount = 20, int selectionTopCount = 5, string username = null)
        {
            List<InstagramItemContentData> result = new List<InstagramItemContentData>();
            var data = instagramContentData.Data.OrderByDescending(x =>x.UploadTime).ToList();

            if (username != null)
            {
                data = data.Where(x => x.Username == username).ToList();
            }

            int processed = 0;

            while (processed < data.Count)
            {
                var bestImages = GetBestImages(data.Skip(processed).Take(groupCount), selectionTopCount);
                result.AddRange(bestImages);
                processed += groupCount;
            }

            return instagramContentData.ToPlainList(result);
        }

        private IEnumerable<InstagramItemContentData> GetBestImages(IEnumerable<InstagramItemContentData> images, int takeCount)
        {
            images = images.OrderByDescending(x => x.TotalEngagement);
            var average = images.Average(x => x.TotalEngagement);
            foreach (var image in images)
            {
                image.Engagement = (image.TotalEngagement / average) * 100;
            }
            return images.Take(takeCount).ToList();
        }
    }

    public class InstagramContentData
    {
        public List<InstagramItemContentData> Data { get; set; }
        public Dictionary<string, List<InstagramItemContentData>> CarrouselContent { get; set; }

        public InstagramContentData(List<List<string>> data)
        {
            if (!DateTime.TryParse(data[0][0], out DateTime test))
            {
                data.RemoveAt(0);
            }
            Data = new List<InstagramItemContentData>();
            CarrouselContent = new Dictionary<string, List<InstagramItemContentData>>();
            var counter = 0;
            foreach (var listData in data)
            {
                counter++;
                var itemData = new InstagramItemContentData(listData);
                if (itemData.IsSideCar)
                {
                    if (!CarrouselContent.ContainsKey(itemData.Key()))
                    {
                        CarrouselContent.Add(itemData.Key(),new List<InstagramItemContentData>(){itemData});
                        Data.Add(itemData);
                    }
                    else
                    {
                        CarrouselContent[itemData.Key()].Add(itemData);
                    }
                }
                else
                {
                    Data.Add(itemData);
                }
            }
        }

        public string Key(InstagramItemContentData data)
        {
            return $"{data.UploadTime},{data.CommentCount},{data.LikeCount},{data.Username},\"{data.Description.Replace("\"", "\"\"")}\",\"{data.Location}\"";
        }

        public List<InstagramItemContentData> ToPlainList()
        {
            var result = Data;
            foreach (var carrouselData in Data.Where(x => x.IsSideCar))
            {
                result.Remove(carrouselData);
                result.AddRange(CarrouselContent[carrouselData.Key()]);
            }

            return result;
        }

        public List<InstagramItemContentData> ToPlainList(List<InstagramItemContentData> filteredData)
        {
            List<InstagramItemContentData> carrouselContentToRemove = new List<InstagramItemContentData>();
            List<InstagramItemContentData> carrouselContent = new List<InstagramItemContentData>();
            foreach (var carrouselData in filteredData.Where(x => x.IsSideCar))
            {
                carrouselContentToRemove.Add(carrouselData);
                foreach (var instagramItemContentData in CarrouselContent[carrouselData.Key()])
                {
                    instagramItemContentData.Engagement = carrouselData.Engagement;
                }
                carrouselContent.AddRange(CarrouselContent[carrouselData.Key()]);
            }

            foreach (var removeData in carrouselContentToRemove)
            {

                filteredData.Remove(removeData);
            }

            filteredData.AddRange(carrouselContent);
            return filteredData;
        }
    }
}
