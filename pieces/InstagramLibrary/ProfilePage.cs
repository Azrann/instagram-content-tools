using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Contrib.PageObjects;

namespace InstagramLibrary
{
    public class ProfilePage : PageObject
    {
        [Selector("h2")]
        public virtual Task<ElementHandle> Name { get; }

        [Selector("#react-root > section > main > div > header > section > ul > li:nth-child(1) > span > span")]
        public virtual Task<ElementHandle> PostsLbl { get; }

        [Selector("#react-root > section > main > div > header > section > ul > li:nth-child(2) > a > span")]
        public virtual Task<ElementHandle> FollowersLbl { get; }

        [Selector("#react-root > section > main > div > header > section > ul > li:nth-child(2) > a")]
        public virtual Task<ElementHandle> FollowersBtn { get; }

        [Selector("#react-root > section > main > div > header > section > ul > li:nth-child(3) > a > span")]
        public virtual Task<ElementHandle> FollowingLbl { get; }

        [Selector("#react-root > section > main > div > header > section > ul > li:nth-child(3) > a")]
        public virtual Task<ElementHandle> FollowingBtn { get; }

        [Selector("#react-root > section > main > div > header > section > div.nZSzR > div.Igw0E.IwRSH.eGOV_._4EzTm > span > span.vBF20._1OSdk > button")]
        public virtual Task<ElementHandle> FollowBackBtn { get; }

    }
}