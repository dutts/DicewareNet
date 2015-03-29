using System;
using System.Collections.Generic;
using System.Linq;

namespace DicewareNet.WordList
{
    public abstract class WordList : IWordList
    {
        protected readonly Dictionary<long, string> WordDict = new Dictionary<long, string>();

        public string Find(long number)
        {
            return WordDict[number];
        }

        public bool TryFind(long number, out string result)
        {
            return WordDict.TryGetValue(number, out result);
        }

        public IEnumerable<string> Lookup(IEnumerable<long> diceRolls, string seperator = "")
        {
            return diceRolls.Select(r => String.Concat(Find(r), seperator));
        }
    }
}
