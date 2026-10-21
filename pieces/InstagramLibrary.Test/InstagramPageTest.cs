using System;
using System.Threading.Tasks;
using PuppeteerSharp;
using Xunit;
using PuppeteerSharp.Contrib.Extensions;
using PuppeteerSharp.Contrib.PageObjects;

namespace InstagramLibrary.Test
{
    public class InstagramPageTest
    {
        private static string cookies = "REPLACE_WITH_YOUR_VALUE";

        private static string bufferCookies =
            "REPLACE_WITH_YOUR_VALUE";
        
        [Fact]
        public async void WhenIDownloadAPostICanSeeNumPages()
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false
            });
            Page page = await browser.NewPageAsync();
            await page.AddCookies(cookies);
            var startPage = await page.GoToAsync<InstagramMainPage>("https://www.instagram.com/");
            var notificationBtn = await startPage.TurnOnNotificationsBtn;
            if (await notificationBtn.IsVisibleAsync())
            {
                await notificationBtn.ClickAsync();
            }

            var resultsPage = await startPage.Search("coffebeans",3);
            Console.ReadKey();
        }

            [Fact]
            public async void ICanUploadContentToBuffer()
            {
                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = false
                });
                Page page = await browser.NewPageAsync();
                //await page.AddCookies(bufferCookies);
                var startPage = await page.GoToAsync<BufferLoginPage>("https://publish.buffer.com/profile/5ed2d14470c9bc6ff642d912/tab/queue");
                var queuePage = await startPage.Login("user@example.invalid", "Buffer123!");
                var slots = await queuePage.AvailableSlots();

                await queuePage.SendPost(slots[0], "\\Content\\Top\\B_7Lh-cJWki.jpeg",
                    "Peazo cochazo como esto funque me la meneo","","#test #test");
            }
    }
}
