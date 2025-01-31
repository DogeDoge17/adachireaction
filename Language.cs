using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adachi_reaction_bot
{
    public class Language
    {
        public string code = "";
        public string[] words = { };
        public float chance = 100;

        public Language(string path, string code, double chance)
        {
            this.chance = (float)chance;
            this.code = code;

            words = File.OpenText(path).ReadToEnd().Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).Select(wr => wr.Trim()).ToArray();
        }

        public static Language RollChances(List<Language> langs)
        {
            float totalWeight = 0f;
            langs.ForEach(lang => totalWeight += lang.chance);

            float randomValue = (float)Random.Shared.NextDouble() * totalWeight;
            float cumulativeWeight = 0f;

            for (int i = 0; i < langs.Count; i++)
            {
                cumulativeWeight += langs[i].chance;
                if (randomValue <= cumulativeWeight)
                {
                    return langs[i];
                }
            }
            return langs.Count > 0 ? langs[0] : null;
        }

        public Word GetWord()
        {
            Word williamRobinson;
            williamRobinson.raw = (words[Random.Shared.Next(0, words.Length)] + "!").ToUpper();
            williamRobinson.formatted = DrawingHelper.RemoveAccents(williamRobinson.raw);
            return williamRobinson;
        }
    }

    public struct Word
    {
        public string raw = "";
        public string formatted = "";

        public Word(string raw, string formatted)
        {
            this.raw = raw;
            this.formatted = formatted;
        }

        public Word() { }
    }

}
