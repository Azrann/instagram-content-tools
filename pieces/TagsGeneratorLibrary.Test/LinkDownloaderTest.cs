using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InstagramLibrary;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using PuppeteerSharp;
using PuppeteerSharp.Contrib.PageObjects;
using TagsGeneratorLibrary;
using Xunit;

namespace InstagramToolsLibrary.Test
{
    public class LinkDownloaderTest
    {
        [Fact]
        public void ICanDownloadAnImageAsync()
        {
            string url =
                "https://scontent-yyz1-1.cdninstagram.com/v/t51.2885-15/e35/87231114_143149317167538_8269208161850788033_n.jpg?_nc_ht=scontent-yyz1-1.cdninstagram.com&_nc_cat=106&_nc_ohc=mjXASGMreEkAX_5yAIs&oh=adec3e96a376e5e2c37556318283e01d&oe=5EFBBEBE";
            LinkDownloader.DownloadFile(url, "image1.jpg");
        }

        [Fact]
        public void ICanDownloadAnImage()
        {
            string url =
                "https://scontent-yyz1-1.cdninstagram.com/v/t51.2885-15/e35/87231114_143149317167538_8269208161850788033_n.jpg?_nc_ht=scontent-yyz1-1.cdninstagram.com&_nc_cat=106&_nc_ohc=mjXASGMreEkAX_5yAIs&oh=adec3e96a376e5e2c37556318283e01d&oe=5EFBBEBE";
            LinkDownloader.DownloadAnyToJpeg(url, "image2.jpg");
        }
        [Fact]
        public void ICanDownloadAVideo()
        {
            string url =
                "https://scontent-yyz1-1.cdninstagram.com/v/t50.2886-16/100969924_1419156151805921_8833919169448523311_n.mp4?_nc_ht=scontent-yyz1-1.cdninstagram.com&_nc_cat=105&_nc_ohc=1F5EMpves0cAX_tpPHw&oe=5EDB0693&oh=345d95888698641ea12b473818f15b1f";
            LinkDownloader.DownloadFile(url, "video1.mp4");
        }

        [Fact]
        public void Step1ICanFilterForSuperLujo()
        {
            string urlDescargados = "https://docs.google.com/spreadsheets/d/1bnJ_7_NvM-RhjNhWle1fZ-_uJCqqW0qHum_PU2RhweA/edit?usp=sharing";
            string urlProfiles = "https://docs.google.com/spreadsheets/d/1e735LBO8x8gjQvGmpbLnromiXnjoljs0pcng84fQvbM/edit?usp=sharing";
            string csvFileName = "SuperLujoTop2.csv";
            List<InstagramItemContentData> result = FilterCsv(urlDescargados, urlProfiles, csvFileName);
            Assert.True(result.Any());
        }

        [Fact]
        public void Step1ICanFilterForBillonariosMotivacion()
        {
            string urlDescargados = "https://docs.google.com/spreadsheets/d/1M3GNqopm8RcT-Uf_go_NOIP9HLTJ-BqcVyP8BYLdp5I/edit?usp=sharing";
            string urlProfiles = "https://docs.google.com/spreadsheets/d/1ey3Gcmhf56WlSwmO7lRbf1x0vNM4knkq2DQ0AiyFKtQ/edit?usp=sharing";
            string csvFileName = "BillonariosMotivacionTop.csv";
            List<InstagramItemContentData> result = FilterCsv(urlDescargados, urlProfiles, csvFileName);
            Assert.True(result.Any());
        }


        [Fact]
        public void Step2ICanDownloadContentForSuperLujoUnico()
        {
            string urlTops = "https://docs.google.com/spreadsheets/d/12XuqZID7Lr3isUZkQbeHED7-Cp4FJlyS2jy35ICOXho/edit?usp=sharing";
            string urlProfiles = "https://docs.google.com/spreadsheets/d/1e735LBO8x8gjQvGmpbLnromiXnjoljs0pcng84fQvbM/edit?usp=sharing";
            SheetsLibrary.Sheets sheetsService = new SheetsLibrary.Sheets(urlTops);
            var imagesList = sheetsService.GetRows("A:L");
            ContentDownloader contentDownloader = new ContentDownloader(imagesList);

            SheetsLibrary.Sheets profiles = new SheetsLibrary.Sheets(urlProfiles);
            var profileList = profiles.GetRows("A:B");
            profileList.RemoveAt(0);

            contentDownloader.DownloadTop("superlujo.unico", 1500,null);
        }

        [Fact]
        public void Step2ICanDownloadContentForBillonariosMotivacion()
        {
            string urlTops = "https://docs.google.com/spreadsheets/d/1ryR3pPRkQW5uA9VeWLzWzQsDAcsNHiStas5eQWd47_g/edit?usp=sharing";
            string urlProfiles = "https://docs.google.com/spreadsheets/d/1ey3Gcmhf56WlSwmO7lRbf1x0vNM4knkq2DQ0AiyFKtQ/edit?usp=sharing";
            SheetsLibrary.Sheets sheetsService = new SheetsLibrary.Sheets(urlTops);
            var imagesList = sheetsService.GetRows("A:L");
            ContentDownloader contentDownloader = new ContentDownloader(imagesList);

            SheetsLibrary.Sheets profiles = new SheetsLibrary.Sheets(urlProfiles);
            var profileList = profiles.GetRows("A:B");
            profileList.RemoveAt(0);

            contentDownloader.DownloadTop("billonarios.motivacion", 2300, null);
        }

        [Fact]
        public async void Step3ICanReuploadTheContentSuperLujo()
        {
            string signature = @"
⚜️ Te ha gustado el post? 
Síguenos @superlujo.unico 👈

#supercoches #cochesdelujo #autosdelujo #cochesdeportivos 
#vidamillonaria
mansiones #casasdelujo #casasmodernas #inversionesinmobiliarias
📷 DM para crédito
";
            string hashTagUrl =
                "https://docs.google.com/spreadsheets/d/15r-bUcc2xxilCECp0BKABxeK-1KYRY0GE2Qcr_61akI/edit?usp=sharing";
            string urlTops = "https://docs.google.com/spreadsheets/d/12XuqZID7Lr3isUZkQbeHED7-Cp4FJlyS2jy35ICOXho/edit?usp=sharing";
            string bufferAccountUrl = "https://login.buffer.com/login";
            string accountName = "superlujo.unico";
            int numPost = 12;

            await UploadContentWithHashTags(hashTagUrl, urlTops, bufferAccountUrl, accountName, numPost, signature);
        }

        [Fact]
        public async void Step3ICanReuploadTheContentBillonariosMotivacion()
        {
            string signature = @"
🔥 
⁣⠀⠀
💎 Te ha gustado?
🙌 Sigue @billonarios.motivacion


📷 DM para crédito
";
            string hashTagUrl =
                "https://docs.google.com/spreadsheets/d/1-GqaBlCq8kPZ3HEuP7fcMm54VlphgV5PKONm1KP5310/edit?usp=sharing";
            string urlTops = "https://docs.google.com/spreadsheets/d/1ryR3pPRkQW5uA9VeWLzWzQsDAcsNHiStas5eQWd47_g/edit?usp=sharing";
            string bufferAccountUrl = "https://publish.buffer.com/profile/5ed153a4cd96ae52f1536dcd/tab/queue";
            string accountName = "billonarios.motivacion";
            int numPost = 6;

            await UploadContentWithHashTags(hashTagUrl, urlTops, bufferAccountUrl, accountName, numPost, signature,1);
        }

        [Fact]
        public void ICanGetVideoDuration()
        {
            var under60 = FileUnder60Sec("\\Content\\Top\\CAcvX5jAKCL.mp4");
        }

        [Fact]
        public void ICanLoadCarrousel()
        {
            string urlTops = "https://docs.google.com/spreadsheets/d/11GBY5pM_SRI1S12iJYHDVIXBUAh_dhI1_1hkVxSLit4/edit?usp=sharing";

            SheetsLibrary.Sheets sheetsService = new SheetsLibrary.Sheets(urlTops);
            var contentList = sheetsService.GetRows("A:L");

            var contentUploader = new InstagramContentData(contentList);
            int c = 0;
            foreach (var data in contentUploader.Data)
            {
               
                if (data.IsSideCar && contentUploader.CarrouselContent.ContainsKey(data.Key()))
                {
                    c++;
                    var ey = "";
                }
            }
        }
        

        private async Task UploadContentWithHashTags(string hashTagUrl, string url, string bufferAccountUrl, string accountName, int numPost, string signature,int skipFirst =0)
        {
            SheetsLibrary.Sheets sheetsService = new SheetsLibrary.Sheets(url);
            var contentList = sheetsService.GetRows("A:L");

            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false
            });
            Page page = await browser.NewPageAsync();
            //await page.AddCookies(bufferCookies);
            var startPage = await page.GoToAsync<BufferLoginPage>(bufferAccountUrl);
            var queuePage = await startPage.Login("user@example.invalid", "Buffer123!");

            ContentUploader contentUploader = new ContentUploader(contentList, GetHashTags(hashTagUrl), queuePage, new RemoveHashTextFilter());
            await contentUploader.UploadTop(accountName, numPost, signature, skipFirst: skipFirst);
        }

        private static List<InstagramItemContentData> FilterCsv(string urlDescargados, string urlProfiles, string csvFileName)
        {
            SheetsLibrary.Sheets sheetsService = new SheetsLibrary.Sheets(urlDescargados);
            var imagesList = sheetsService.GetRows("A:K");

            SheetsLibrary.Sheets profiles = new SheetsLibrary.Sheets(urlProfiles);
            var profileList = profiles.GetRows("A:B");
            profileList.RemoveAt(0);

            ContentFilter filter = new ContentFilter(imagesList);
            var result = new List<InstagramItemContentData>();
            foreach (var profile in profileList)
            {
                var numPost = (20 * int.Parse(profile[1]) / 100);
                result.AddRange(filter.GetTopImages(20, numPost, profile[0].Replace("https://www.instagram.com/", "").Replace("/", "")));
            }

            using (var file = File.CreateText(csvFileName))
            {
                foreach (var imageFilter in result)
                {
                    file.WriteLine(imageFilter.ToLineString());
                }
            }

            return result;
        }

        private static TimeSpan GetVideoDuration(string filePath)
        {
            var tfile = TagLib.File.Create(filePath);
            string title = tfile.Tag.Title;
            TimeSpan duration = tfile.Properties.Duration;
            return duration;
        }

        private bool FileUnder60Sec(string filePath)
        {
            return GetVideoDuration(filePath) < TimeSpan.FromSeconds(60);
        }

        public List<string> GetHashTags(string url)
        {
            
            SheetsLibrary.Sheets sheetsService = new SheetsLibrary.Sheets(url);
            var tagList = sheetsService.Get2Rows();
            Generator generator = new Generator(tagList.ToDictionary(x => x.Key, y => int.Parse(y.Value)));
            List<string> result = new List<string>();

            Enumerable.Range(0, 10).ToList().ForEach(x => result.Add(GetRandomTags(generator)));
            return result;
        }

        private static string GetRandomTags(Generator generator)
        {
            var result = generator.Generate(6, 3, 1);
            var tagsString = string.Join(" ", result);
            return tagsString;
        }
    }
}
