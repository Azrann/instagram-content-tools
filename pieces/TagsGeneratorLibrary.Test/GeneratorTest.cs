using System.Linq;
using System.Text;
using TagsGeneratorLibrary;
using Xunit;

namespace InstagramToolsLibrary.Test
{
    public class GeneratorTest
    {
        [Fact]
        public void ICanGenerateTagsForBillonariosMotivacion()
        {
            string url =
                "https://docs.google.com/spreadsheets/d/1-GqaBlCq8kPZ3HEuP7fcMm54VlphgV5PKONm1KP5310/edit?usp=sharing";
            SheetsLibrary.Sheets sheetsService = new SheetsLibrary.Sheets(url);
            var tagList = sheetsService.Get2Rows();
            Generator generator = new Generator(tagList.ToDictionary(x => x.Key,y=> int.Parse(y.Value)));
            StringBuilder sb = new StringBuilder();

            Enumerable.Range(0, 10).ToList().ForEach(x=>sb.AppendLine(GetRandomTags(generator)));
            
            var result = sb.ToString();
            Assert.True(result.Any());
        }

        private static string GetRandomTags(Generator generator)
        {
            var result = generator.Generate(8, 6, 2);
            var tagsString = string.Join(" ", result);
            return tagsString;
        }

        [Fact]
        public void ICanGenerateTagsForSuperlujoUnicos()
        {

        }
    }
}
