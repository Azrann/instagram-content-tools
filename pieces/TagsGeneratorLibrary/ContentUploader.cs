using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InstagramLibrary;

namespace InstagramToolsLibrary
{
    public class ContentUploader
    {
        private readonly List<string> _hashTags;
        private readonly BufferQueuePage _page;
        private readonly string _queueName;
        private InstagramContentData _data;
        private ITextFilter _textFilter;

        public ContentUploader(List<List<string>> data, List<string> hashTags, BufferQueuePage page, ITextFilter textFilter = null,string queueName = "superlujo.unico")
        {
            this._hashTags = hashTags;
            _page = page;
            _queueName = queueName;

            _data = new InstagramContentData(data);
            _textFilter = textFilter ?? new DefaultTextFilter();
        }

        public async Task<List<InstagramItemContentData>> UploadTop(string myUsername, int numUploads = 20, string signature = "", string filterUsername = null, bool onlyPhotos = false, int skipFirst =0)
        {
            Directory.CreateDirectory($"\\Content\\{myUsername}\\Top\\Completed");
            Directory.CreateDirectory($"\\Content\\{myUsername}\\Normal\\Completed");
            Directory.CreateDirectory($"\\Content\\{myUsername}\\Top\\Error");
            Directory.CreateDirectory($"\\Content\\{myUsername}\\Normal\\Error");
            List<InstagramItemContentData> result = new List<InstagramItemContentData>();
            var data = _data.Data.OrderByDescending(x =>x.Engagement).ToList();

            if (filterUsername != null)
            {
                data = data.Where(x => x.Username == filterUsername).ToList();
            }

            if (onlyPhotos)
            {
                data = data.Where(x => string.IsNullOrEmpty(x.VideoUrl)).ToList();
            }

            await _page.Select(_queueName);
            Thread.Sleep(4000);
            await _page.AvailableSlots();
            _page.SkipSlots(skipFirst);

            int processed = 0;
            int uploaded = 0;
            while (processed < data.Count && uploaded < numUploads)
            {
                var content = data[processed];
                //if (!content.IsSideCar || _data.CarrouselContent[content.Key()].Count > 3)
                //{
                //    processed++;
                //    continue;
                //}
                if (!string.IsNullOrEmpty(signature))
                {
                    content.Description += signature;
                }

                if (!content.IsSideCar && await TryScheduleSingleContent(myUsername, content,uploaded))
                {
                    uploaded++;
                }

                if (content.IsSideCar)

                {
                    if (!_data.CarrouselContent.ContainsKey(content.Key()))
                    {
                        var errorHere = 0;
                    }
                    else
                    {
                        if (_data.CarrouselContent[content.Key()].Count == 1 && await TryScheduleSingleContent(myUsername, content, uploaded))
                        {
                            uploaded++;
                        }
                        if (_data.CarrouselContent[content.Key()].Count > 1 && await TryScheduleMultipleContent(myUsername, _data.CarrouselContent[content.Key()], uploaded))
                        {
                            uploaded++;
                        }
                    }

                }

                processed ++;
            }

            MoveErrorFiles($"/Content/{myUsername}/top/Error/error.txt");
            File.Delete($"/Content/{myUsername}/top/Error/error.txt");
            MoveErrorFiles("/Content/{myUsername}/normal/Error/error.txt");
            File.Delete($"/Content/{myUsername}/normal/Error/error.txt");
            return result;
        }

        private async Task<bool> TryScheduleMultipleContent(string myUsername, List<InstagramItemContentData> instagramItemsContentData, int uploaded)
        {
            bool firstContent = true;
            bool result = true;
            if (instagramItemsContentData.Take(4).Any(x => x.IsVideo()))
            {
                result = await TryAddSingleVideoWithContent(myUsername, instagramItemsContentData.First(x => x.IsVideo()), uploaded);
                result = result && await TryScheduleCarrousel(myUsername, instagramItemsContentData);
                return result;
            }

            foreach (var content in instagramItemsContentData.Take(4))
            {
                if (firstContent)
                {
                    if (content.IsVideo())
                    {
                        result = await TryAddSingleVideoWithContent(myUsername, content, uploaded);
                    }
                    else
                    {
                        result = await TryAddSingleImageWithContent(myUsername, content, uploaded);
                    }
                    firstContent = false;
                }
                else
                {
                    if (content.IsVideo())
                    {
                        result = result && await TryAddSingleVideo(myUsername, content);
                    }
                    else
                    {
                        result = result && await TryAddSingleImage(myUsername, content);
                    }
                    firstContent = false;
                }
            }

            result = result && await TryScheduleCarrousel(myUsername, instagramItemsContentData);
            return result;
        }

        private async Task<bool> TryAddSingleImage(string myUsername, InstagramItemContentData content)
        {
            var imageFileName = GetContentFile(content, myUsername);

            if (File.Exists(imageFileName))
            {
                try
                {
                    await _page.UploadFile(imageFileName);
                    await _page.WaitUploadFinish();
                    Thread.Sleep(1000);
                    return true;
                }
                catch (Exception)
                {
                    await HandleError(content, myUsername);
                }
            }

            return false;
        }

        private async Task<bool> TryAddSingleVideo(string myUsername, InstagramItemContentData content)
        {
            var videoFileName = GetContentFile(content, myUsername);
            if (File.Exists(videoFileName))
            {
                if (FileUnder60Sec(videoFileName))
                {
                    try
                    {
                        await _page.UploadFile(videoFileName);
                        await _page.WaitUploadFinish();
                        Thread.Sleep(1000);
                        return true;
                    }
                    catch (Exception)
                    {
                        await HandleError(content, myUsername);
                    }
                }
                else
                {
                    WriteFileError(GetContentFile(content, myUsername), GetContentFileErrorDirectory(content, myUsername), GetContentFileError(content, myUsername) + "-over60sec.mp4");
                }
            }

            return false;
        }

        private async Task<bool> TryAddSingleImageWithContent(string myUsername, InstagramItemContentData content, int uploaded)
        {
            var imageFileName = GetContentFile(content, myUsername);

            if (File.Exists(imageFileName))
            {
                try
                {
                    await _page.CreatePost(_page.AllSlots[uploaded], imageFileName, _textFilter.Filter(content.Description),
                        content.Location, _hashTags[uploaded], true);
                    Thread.Sleep(1000);
                    return true;
                }
                catch (Exception)
                {
                    await HandleError(content, myUsername);
                }
            }

            return false;
        }

        private async Task<bool> TryAddSingleVideoWithContent(string myUsername, InstagramItemContentData content, int uploaded)
        {
            var videoFileName = GetContentFile(content, myUsername);
            if (File.Exists(videoFileName))
            {
                if (FileUnder60Sec(videoFileName))
                {
                    try
                    {
                        await _page.CreatePost(_page.AllSlots[uploaded], videoFileName, _textFilter.Filter(content.Description),
                            content.Location, _hashTags[uploaded], true);
                        Thread.Sleep(1000);
                        return true;
                    }
                    catch (Exception)
                    {
                        await HandleError(content, myUsername);
                    }
                }
                else
                {
                    WriteFileError(GetContentFile(content, myUsername), GetContentFileErrorDirectory(content, myUsername), GetContentFileError(content, myUsername) + "-over60sec.mp4");
                }
            }

            return false;
        }

        private async Task<bool> TryScheduleCarrousel(string myUsername, List<InstagramItemContentData> instagramItemsContentData)
        {
            try
            {
                await _page.SchedulePost();
                Thread.Sleep(1000);
                foreach (var content in instagramItemsContentData)
                {
                    var imageFileNameCompleted = GetContentFileCompleted(content, myUsername);
                    File.Move(GetContentFile(content, myUsername), GetContentFileCompleted(content, myUsername));
                }
                
                return true;
            }
            catch (Exception)
            {
                foreach (var content in instagramItemsContentData)
                {
                    await HandleError(content, myUsername);
                }

                return false;
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

        private async Task<bool> TryScheduleSingleContent(string myUsername, InstagramItemContentData content, int uploaded)
        {
            if (content.IsVideo())
            {
                return await TryScheduleSingleVideo(myUsername, content, uploaded);
            }

            return await TryScheduleSingleImage(myUsername, content, uploaded);
        }

        private async Task<bool> TryScheduleSingleImage(string myUsername, InstagramItemContentData content, int uploaded)
        {
            var imageFileName = GetContentFile(content, myUsername);

            if (File.Exists(imageFileName))
            {
                if (File.Exists(GetContentFileCompleted(content, myUsername)) || File.Exists(GetContentFileError(content, myUsername)))
                {
                    File.Delete(imageFileName);
                    return false;
                }

                try
                {
                    await _page.SendPost(_page.AllSlots[uploaded], imageFileName, _textFilter.Filter(content.Description),
                        content.Location, _hashTags[uploaded]);
                    Thread.Sleep(1000);
                    File.Move(imageFileName, GetContentFileCompleted(content,myUsername));
                    return true;
                }
                catch (Exception ex)
                {
                    await HandleError(content, myUsername);
                }
            }

            return false;
        }


        private async Task<bool> TryScheduleSingleVideo(string myUsername, InstagramItemContentData content, int uploaded)
        {
            var videoFileName = GetContentFile(content, myUsername);
            if (File.Exists(videoFileName))
            {
                if (File.Exists(GetContentFileCompleted(content, myUsername)) || File.Exists(GetContentFileError(content, myUsername)))
                {
                    File.Delete(videoFileName);
                    return false;
                }

                if (FileUnder60Sec(videoFileName))
                {
                    try
                    {
                        await _page.SendPost(_page.AllSlots[uploaded], videoFileName, _textFilter.Filter(content.Description),
                            content.Location, _hashTags[uploaded]);
                        Thread.Sleep(1000);
                        File.Move(videoFileName, GetContentFileCompleted(content, myUsername));
                        return true;
                    }
                    catch (Exception)
                    {
                        await HandleError(content, myUsername);
                    }
                }
                else
                {
                    WriteFileError(GetContentFile(content,myUsername), GetContentFileErrorDirectory(content, myUsername), GetContentFileError(content, myUsername) + "-over60sec.mp4");
                }
            }

            return false;
        }

        private async Task HandleError(InstagramItemContentData content, string myUsername)
        {
            await _page.Page.ScreenshotAsync(GetContentFileError(content,myUsername) + "error.jpeg");
            await _page.Page.Mouse.ClickAsync(50, 50);
            WriteFileError(GetContentFile(content, myUsername), GetContentFileErrorDirectory(content, myUsername), GetContentFileError(content, myUsername));
        }

        private void MoveErrorFiles(string contentTopErrorErrorTxt)
        {
            if (!File.Exists(contentTopErrorErrorTxt))
            {
                return;
            }

            string[] lines = File.ReadAllLines(contentTopErrorErrorTxt);

            foreach (string line in lines)
            {
                var filesPath = line.Split(",");
                File.Move(filesPath[0],filesPath[1]);
            }
        }

        private string GetIdFromUrl(string url)
        {
            return url.Replace("https://www.instagram.com/p/", "").Replace("/", "");
        }

        private void WriteFileError(string filePath, string errorPath, string errorFilePath)
        {
            var errorTextFile = errorPath + "error.txt";
            if (!File.Exists(errorTextFile))
            {
                using (FileStream fs = File.Create(errorTextFile))
                {
                }
            }
            using (StreamWriter w = File.AppendText(errorTextFile))
            {
                w.WriteLine($"{filePath},{errorFilePath}");
            }
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
    }

    public interface ITextFilter
    {
        string Filter(string text);
    }

    public class DefaultTextFilter : ITextFilter
    {
        public string Filter(string text)
        {
            return text;
        }
    }

    public class RemoveHashTextFilter : ITextFilter
    {
        string HashList = "#luxurylivesonly #Luxuries #luxury_life #luxurylife #luxuryclub #billionaire #luxurystyle #instaluxury #luxuryitems #lavish #rich #thebillionairesclub #luxury #luxurygoods #luxurysafari #richgang #luxurylover #luxury4play #Luxuries #luxury_life #luxurylife #luxuryclub #billionaire #luxurystyle #instaluxury #luxuryitems #lavish #rich #thebillionairesclub #luxury #luxurygoods #luxurysafari #richgang #luxurylover #luxury4play #losangeles #california #mansion #estate #house #home #goals";
        string HashList2 = "supercoches cochesdelujo autosdelujo cochesdeportivos vidamillonaria mansiones casasdelujo casasmodernas inversionesinmobiliarias";

        public string Filter(string text)
        {
            string result = text;
            foreach (var hash in HashList.Split(" "))
            { 
                result = result.Replace(hash, "");
            }
            
            result = result.Replace("Help us to grow the community", "Ayúdanos a crecer 👇");
            result = result.Replace("Follow @luxurylivesonly to see more of the Luxury Lifestyle", "Sigue @superlujo.unico para ver el estilo de vida del 1%");
            result = result.Replace("And if you like the content Turn on Post Notifications so you never miss out", "No te pierdas nuestro contenido activa las notificaciones");
            result = result.Replace("Tag someone that needs to see this!", "");
            result = result.Replace("Dm for credit", "Dm para crédito");
            result = result.Replace("#", "");
            foreach (var hash in HashList2.Split(" "))
            {
                result = result.Replace(hash, "#"+ hash);
            }

            int numAts = result.Count(f => f == '@');
            if (numAts > 4)
            {
                result = result.Replace("@", "");
            }

            return result;
        }
    }
}
