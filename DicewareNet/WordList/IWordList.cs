using System.Collections.Generic;

namespace DicewareNet.WordList
{
    public interface IWordList
    {
        string Find(long number);
        bool TryFind(long number, out string result);

        IEnumerable<string> Lookup(IEnumerable<long> diceRolls, string seperator);
    }
}
