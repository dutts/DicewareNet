using System.Linq;
using DicewareNet.Dice;
using DicewareNet.WordList;

namespace DicewareNet.Console
{
    internal class Program
    {
        public const int NumberOfDice = 5;
        public const int NumberOfRolls = 7;
        public static ClrRandom Rng = new ClrRandom();

        private static void Main(string[] args)
        {
            // Load in the word dict
            var wordDict = new WordListWeb("http://world.std.com/~reinhold/diceware.wordlist.asc");
                // WordListFile("..\\..\\diceware_wordlist.txt");

            do
            {
                var diceRolls = Enumerable.Range(0, NumberOfRolls).Select(_ => Rng.DiceRoll(NumberOfDice));
                var words = wordDict.Lookup(diceRolls, " ");
                System.Console.WriteLine(string.Concat(words));
            } while (System.Console.ReadLine() != "q");
        }
    }
}