using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsGeneratorLibrary
{
    public class Generator
    {
        private readonly Dictionary<string, int> _tags;

        public Generator(Dictionary<string, int> tags)
        {
            _tags = tags;
        }

        public IEnumerable<string> Generate(int numSmall, int numMedium, int numBig)
        {
            List<string> result = new List<string>();
            result.AddRange(Generate(numSmall,_tags.Where(x => x.Value < 100).Select(x => x.Key)));
            result.AddRange(Generate(numMedium, _tags.Where(x => x.Value > 100 && x.Value < 1000).Select(x => x.Key)));
            result.AddRange(Generate(numBig, _tags.Where(x => x.Value > 1000).Select(x => x.Key)));
            return result;
        }

        public IEnumerable<string> Generate(int numTags, IEnumerable<string> tags)
        {
            if (numTags < 1)
            {
                return new List<string>();
            }

            if (numTags > tags.Count() - 1)
            {
                return tags;
            }

            List<string> result = new List<string>();
            List<string> newList = new List<string>(tags); 
            Random random = new Random();
            for (int i = 0; i < numTags; i++)
            {
                int position = random.Next(0,newList.Count - 1);
                result.Add(newList[position]);
                newList.RemoveAt(position);
            }

            return result;
        }
    }

    static class Extensions
    {
        public static IList<T> Clone<T>(this IEnumerable<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
