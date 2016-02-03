using System;
using System.IO;
using System.Linq;
using System.Net;

namespace DicewareNet.WordList
{
    public class WordListWeb8k : WordList
    {
        private const string WordListUri = "http://world.std.com/~reinhold/diceware8k.txt";

        public WordListWeb8k()
        {
            using (var wc = new WebClient())
            {
                wc.DownloadFile(new Uri(WordListUri), "worldlist.tmp");

                using (var fr = new StreamReader("worldlist.tmp"))
                {
                    foreach (var key in WordListKeys.Keys)
                    {
                        string line = fr.ReadLine();

                        if (line == null) return;

                        WordDict.Add(key, line.Trim());
                    }
                }
            }
        }
    }
}