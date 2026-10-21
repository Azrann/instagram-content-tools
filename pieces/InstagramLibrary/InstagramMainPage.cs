using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Contrib.Extensions;
using PuppeteerSharp.Contrib.PageObjects;
using PuppeteerSharp.Input;

namespace InstagramLibrary
{
    public class InstagramMainPage : PageObject
    {
        [Selector(".x3qfX")] //Click first before writting
        public virtual Task<ElementHandle> SearchInput { get; }

        [Selector(".piCib .HoLwm")]
        public virtual Task<ElementHandle> TurnOnNotificationsBtn { get; }

        [Selector("qNELH")]
        public virtual Task<ElementHandle> MyProfileBtn { get; }

        public async Task<InstagramSearchResultPage> Search(string text, int delaySeconds)
        {
            Random rnd = new Random();
            await Task.Delay(rnd.Next(1,1600) + (delaySeconds * 1000));
            var input = await SearchInput;
            if (await input.IsHiddenAsync())
            {
                return null;
            }

            await input.TypeAsync(text, TypeOptionsExt.Delay500());
            await input.PressAsync(Key.Enter, PressOptionsExt.Delay1200());
            await Task.Delay(1100);
            await Page.Keyboard.PressAsync(Key.Enter);
            await Task.Delay(500);
            await Page.Keyboard.PressAsync(Key.Enter);
            return await Page.WaitForNavigationAsync<InstagramSearchResultPage>();
        }
    }

    public class InstagramSearchResultPage : PageObject
    {
        [Selector("#search")]
        public virtual Task<ElementHandle> SearchResults { get; }
    }

    public class BufferQueuePage : PageObject
    {
        private string uploadBtnSelector =
            "div.UploadZone__DropzoneWithStyles-mgtm0r-1.KQmzv.MediaAttachment__uploadZone___1UU7z input";

        private string textEditorSelector = ".DraftEditor-editorContainer";

        public void SkipSlots(int skipFirst)
        {
            for (int i = 0; i < skipFirst; i++)
            {
                AllSlots.RemoveAt(0);
            }
        }
        
        private string SchedulePostBtnSelector = ".UpdateSaver__buttonsWrapper___2Swlk button";

        private string FirstCommentTxtSelector = ".FirstCommentComposerBar__Wrapper-sc-1k2ykrm-1 textarea";

        private string LocationInputSelector = ".LocationComposerBar__locationAutocompleteContainer___1QGra input";

        private string LocationOptions = ".LocationComposerBar__optionRow___12qwT";

        private string UploadingSelector = ".MediaAttachment__uploadButtonUIIsUploading___4C1a9";
        public List<ElementHandle> AllSlots;

        public async Task<List<ElementHandle>> AvailableSlots()
        {
            if (AllSlots != null)
            {
                return AllSlots;
            }
            AllSlots = new List<ElementHandle>();

            var slots = await this.Page.QuerySelectorAllAsync("#main > div > div > div");
            foreach (var slot in slots)
            {
                var value = await (await slot.GetPropertyAsync("innerHTML")).JsonValueAsync<string>();
                if (!string.IsNullOrEmpty(value) && value.Length < 30 && (value.Contains("AM") || value.Contains("PM")))
                {
                    AllSlots.Add(slot);
                }
            }

            return AllSlots;
        }

        public async Task SendPost(ElementHandle slot, string contentUrl, string text, string location, string hashtags)
        {
            await OpenScheduleDialog(slot);

            await UploadFile(contentUrl);

            await FillInContent(text, location, hashtags,contentUrl);

            await SchedulePost();
        }
        public async Task CreatePost(ElementHandle slot, string contentUrl, string text, string location, string hashtags, bool hashtagsInBody = false)
        {
            await OpenScheduleDialog(slot);

            await UploadFile(contentUrl);

            await FillInContent(text, location, hashtags, contentUrl, hashtagsInBody);

            await WaitUploadFinish();
        }
        private static async Task OpenScheduleDialog(ElementHandle slot)
        {
            await slot.HoverAsync();
            await slot.ClickAsync();
        }

        private async Task FillInContent(string text, string location, string hashtags, string contentUrl, bool hashtagsInBody = false)
        {
            var captionInput = await this.Page.QuerySelectorAsync(textEditorSelector);
            await captionInput.ClickAsync();
            await Page.Keyboard.TypeAsync(text);
            await Page.Keyboard.TypeAsync(contentUrl);
            if (!string.IsNullOrEmpty(hashtags))
            {
                var firstComment = await this.Page.QuerySelectorAsync(FirstCommentTxtSelector);
                if (firstComment != null && !hashtagsInBody)
                {
                    await firstComment.ClickAsync();
                    await Page.Keyboard.TypeAsync(hashtags);
                }
                else {
                    await Page.Keyboard.TypeAsync(hashtags);
                }
            }

            if (!string.IsNullOrEmpty(location))
            {
                var locationTxt = await this.Page.QuerySelectorAsync(LocationInputSelector);
                if (locationTxt != null)
                {
                    await locationTxt.ClickAsync();
                    await Page.Keyboard.TypeAsync(location);
                    await Page.WaitForSelectorAsync(LocationOptions);
                    var locationOptions = await this.Page.QuerySelectorAsync(LocationOptions);
                    await locationOptions.ClickAsync();
                }
            }
        }

        public async Task UploadFile(string contentUrl)
        {
            await this.Page.WaitForSelectorAsync(uploadBtnSelector);
            var uploadBtn = await this.Page.QuerySelectorAsync(uploadBtnSelector);
            await uploadBtn.UploadFileAsync(contentUrl);
            await this.Page.WaitForSelectorAsync(uploadBtnSelector);
        }

        public async Task SchedulePost()
        {
            await WaitUploadFinish();

            var scheduleBtn = await this.Page.QuerySelectorAsync(SchedulePostBtnSelector);
            await scheduleBtn.ClickAsync();
            var  html = await Page.WaitForSelectorAsync(SchedulePostBtnSelector, new WaitForSelectorOptions()
            {
                Hidden = true
            });
        }

        public async Task WaitUploadFinish()
        {
            var html = await Page.WaitForSelectorAsync(SchedulePostBtnSelector + ":not(:disabled)", new WaitForSelectorOptions()
            {
                Timeout = 240000
            });

            html = await Page.WaitForSelectorAsync(UploadingSelector, new WaitForSelectorOptions()
            {
                Hidden = true
            });
        }

        public async Task Select(string queueName)
        {
            var labels = await this.Page.QuerySelectorAllAsync("label");
            foreach (var slot in labels)
            {
                var value = await(await slot.GetPropertyAsync("innerHTML")).JsonValueAsync<string>();
                if (!string.IsNullOrEmpty(value) && value.Length < 30 && (value.Contains(queueName)))
                {
                    await slot.ClickAsync();
                    break;
                }
            }

            return;
        }
    }

    public class BufferLoginPage : PageObject
    {
        [Selector("#email")]
        public virtual Task<ElementHandle> UsernameTxt { get; }
        [Selector("#password")]
        public virtual Task<ElementHandle> PasswordTxt { get; }
        [Selector("#login-form-submit")]
        public virtual Task<ElementHandle> LoginBtn { get; }

        public async Task<BufferQueuePage> Login(string username, string pass)
        {
            var usernameTxt = await UsernameTxt;
            if (await usernameTxt.IsVisibleAsync())
            {
                await usernameTxt.TypeAsync("user@example.invalid", TypeOptionsExt.Delay(100));
            }
            await (await PasswordTxt).TypeAsync("Buffer123!", TypeOptionsExt.Delay(100));
            await (await LoginBtn).ClickAsync();
            await Task.Delay(1100);
            return await Page.WaitForNavigationAsync<BufferQueuePage>();
        }
    }
}
