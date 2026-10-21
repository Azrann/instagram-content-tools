using System.IO;
using System.Linq;
using Xunit;

namespace InstagramToolsLibrary.Test
{
    public class ContentFilterTest
    {
        [Fact]
        public void ICanFilterPostForMakeBillions()
        {
            string url =
                "https://docs.google.com/spreadsheets/d/1woU11APga8i-jUTHsYnPrtb0iUQADqD5XJQK3alqU4s/edit?usp=sharing";
            SheetsLibrary.Sheets sheetsService = new SheetsLibrary.Sheets(url);
            var imagesList = sheetsService.GetRows("A:E");
            ContentFilter filter = new ContentFilter(imagesList);
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
            string url =
                "https://docs.google.com/spreadsheets/d/1k67QYeoRj1NZxV2TiDjV26IqwWf2uq-18ePdgxOex1c/edit?usp=sharing";
            SheetsLibrary.Sheets sheetsService = new SheetsLibrary.Sheets(url);
            var imagesList = sheetsService.GetRows("A:E");
            ContentFilter filter = new ContentFilter(imagesList);
            var result = filter.GetTopImages(20, 10);

            using (var file = File.CreateText("TheBillionaresClub.csv"))
            {
                foreach (var imageFilter in result)
                {
                    file.WriteLine(imageFilter.ToLineString());
                }
            }

            Assert.True(result.Any());
        }

        [Fact]
        public void ICanUseTheTextFilter()
        {
            var text = @"Daily essentials.
Tag someone that needs to see this!🥂
Help us to grow the community 👩🏼‍🤝‍👩🏻
-
Follow @luxurylivesonly to see more of the Luxury Lifestyle 👑
-
And if you like the content Turn on Post Notifications so you never miss out❗️
-
📷 @whatusmenlike ⁣
#luxurylivesonly
~
.
.
-
#luxurywatch #luxurywatches #luxurywatchlife #mensessentials #essentials #royaloak #Luxuries #luxury_life #luxurylife #luxuryclub #billionaire #luxurystyle #instaluxury #luxuryitems #lavish #rich #thebillionairesclub #luxury #luxurygoods #luxurysafari #richgang #luxurylover #luxury4play";

            var filter = new RemoveHashTextFilter();
            var result = filter.Filter(text);
            Assert.False(result.Contains("#luxurylife"));
        }
    }
}