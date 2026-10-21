using System;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Drawing;

namespace InstagramToolsLibrary
{
    public class LinkDownloader
    {
        public static void DownloadAnyToJpeg(string url, string savePath)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(url);

                using (MemoryStream mem = new MemoryStream(data))
                {
                    using (var yourImage = Image.FromStream(mem))
                    {
                        yourImage.Save(savePath, ImageFormat.Jpeg);
                    }
                }
            }
        }

        public static void DownloadFile(string url, string savePath)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(url), savePath);
            }
        }
    }
}