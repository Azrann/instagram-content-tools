using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Input;

namespace InstagramLibrary
{
    public static class TypeOptionsExt
    {
        public static TypeOptions Delay500() =>
            new TypeOptions()
            {
                Delay = 500
            };

        public static TypeOptions Delay1200() =>
            new TypeOptions()
            {
                Delay = 1200
            };

        public static TypeOptions Delay(int myDelay) =>
            new TypeOptions()
            {
                Delay = myDelay
            };
    }

    public static class PressOptionsExt
    {
        public static PressOptions Delay500() =>
            new PressOptions()
            {
                Delay = 500
            };

        public static PressOptions Delay1200() =>
            new PressOptions()
            {
                Delay = 1200
            };

        public static PressOptions Delay(int myDelay) =>
            new PressOptions()
            {
                Delay = myDelay
            };
    }

    public static class PuppeteerExtensions
    {
        public static async Task AddCookies(this Page page, string cookies)
        {
            var cookiesDictionary = LoadCookies(cookies);
            foreach (var cookie in cookiesDictionary)
            {
                await page.SetCookieAsync(new CookieParam()
                {
                    Domain = cookie.Key == "ig_cb" ? "www.instagram.com" : ".instagram.com",
                    Name = cookie.Key,
                    Value = cookie.Value,
                    Path = "/"
                });
            }
        }


        public static List<string> bannedCookies = new List<string>()
        {
            "urlgen"
        };

        public static Dictionary<string, string> LoadCookies(string cookies)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            var cookiesArray = cookies.Split(";");
            foreach (var elem in cookiesArray)
            {
                var keyvalue = elem.Split("=");
                string value = keyvalue[1];
                if (keyvalue.Length > 2)
                {
                    value += $"={keyvalue[2]}";
                }

                var cookieKey = keyvalue[0].Replace(" ", "");
                if (!bannedCookies.Contains(cookieKey))
                {
                    result.Add(cookieKey, value);
                }
            }

            return result;
        }

        public static string GetCookieString(IOrderedEnumerable<KeyValuePair<string, string>> cookies)
        {
            string result;
            var list = cookies.Select(x => $"{x.Key}={x.Value}");
            return string.Join(";", list);
        }
        public static CookieContainer GetCookieContainer(Dictionary<string, string> cookies)
        {
            CookieContainer result = new CookieContainer();
            foreach (var cookie in cookies)
            {
                result.Add(new Uri(".instagram.com"), new Cookie(cookie.Key, cookie.Value));
            }
            return result;
        }
    }
}