using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DicewareNet.WordList
{
    public class WordListFile : IWordList
    {
        private readonly Dictionary<long, string> _wordDict;
 
        public WordListFile(string fileName)
        {
            _wordDict = new Dictionary<long, string>();

            using (var fr = new StreamReader(fileName))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var lineParts = line.Split('\t');
                    if (lineParts.Count() != 2) continue;

                    long key;
                    if (Int64.TryParse(lineParts[0], out key))
                    {
                        _wordDict.Add(key, lineParts[1].Trim());
                    }
                }
            }
        }

        public string Find(long number)
        {
            return _wordDict[number];
        }

        public bool TryFind(long number, out string result)
        {
            return _wordDict.TryGetValue(number, out result);
        }

        public IEnumerable<string> Lookup(IEnumerable<long> diceRolls, string seperator = "")
        {
            return diceRolls.Select(r => String.Concat(Find(r), seperator));
        }
    }
}
