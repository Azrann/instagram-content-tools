using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using InstagramToolsLibrary;
using Xunit;

namespace TagsGeneratorLibrary.Test
{
    public class ImageFilterTest
    {
        [Fact]
        public void ICanFilterPostForMakeBillions()
        {
            string url =
                "https://docs.google.com/spreadsheets/d/1woU11APga8i-jUTHsYnPrtb0iUQADqD5XJQK3alqU4s/edit?usp=sharing";
            SheetsLibrary.Sheets sheetsService = new SheetsLibrary.Sheets(url);
            var imagesList = sheetsService.GetRows("A:E");
            ImageFilter filter = new ImageFilter(imagesList);
            var result = filter.GetTopImages();

            using (var file = File.CreateText("MakeBillionsSelection.csv"))
            {
                foreach (var imageFilter in result)
                {
                    file.WriteLine(imageFilter.ToLineString());
                }
            }
            Assert.True(result.Any());
        }

        [Fact]
        public void ICanFilterPostsForBillionaresClub()
        {
            string url = "https://docs.google.com/spreadsheets/d/1k67QYeoRj1NZxV2TiDjV26IqwWf2uq-18ePdgxOex1c/edit?usp=sharing";
            SheetsLibrary.Sheets sheetsService = new SheetsLibrary.Sheets(url);
            var imagesList = sheetsService.GetRows("A:E");
            ImageFilter filter = new ImageFilter(imagesList);
            var result = filter.GetTopImages(20,10);

            using (var file = File.CreateText("TheBillionaresClub.csv"))
            {
                foreach (var imageFilter in result)
                {
                    file.WriteLine(imageFilter.ToLineString());
                }
            }
            Assert.True(result.Any());
        }
    }
}
