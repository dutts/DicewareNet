using System;
using System.IO;
using System.Linq;
using System.Net;

namespace DicewareNet.WordList
{
    public class WordListWeb : WordList
    {
        public WordListWeb(string uri)
        {
            using (var wc = new WebClient())
            {
                wc.DownloadFile(new Uri(uri), "worldlist.tmp");

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