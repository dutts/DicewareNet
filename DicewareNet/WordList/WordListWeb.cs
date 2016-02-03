using System;
using System.IO;
using System.Linq;
using System.Net;

namespace DicewareNet.WordList
{
    public class WordListWeb : WordList
    {
        private const string WordListUri = "http://world.std.com/~reinhold/diceware.wordlist.asc";

        public WordListWeb()
        {
            using (var wc = new WebClient())
            {
                wc.DownloadFile(new Uri(WordListUri), "worldlist.tmp");

                using (var fr = new StreamReader("worldlist.tmp"))
                {
                    string line;
                    while ((line = fr.ReadLine()) != null)
                    {
                        var lineParts = line.Split('\t');
                        if (lineParts.Count() != 2) continue;

                        long key;
                        if (long.TryParse(lineParts[0], out key))
                        {
                            WordDict.Add(key, lineParts[1].Trim());
                        }
                    }
                }
            }
        }
    }
}