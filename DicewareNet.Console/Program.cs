using System.Linq;
using DicewareNet.Dice;
using DicewareNet.WordList;

namespace DicewareNet.Console
{
    internal class Program
    {
        public const int NumberOfDice = 5;
        public const int NumberOfRolls = 7;
        public static ClrDice Rng = new ClrDice();

        private static void Main(string[] args)
        {
            var wordDict = new WordListWeb8k();
            do
            {
                var diceRolls = Enumerable.Range(0, NumberOfRolls).Select(_ => Rng.Roll(NumberOfDice));
                var words = wordDict.Lookup(diceRolls, " ");
                System.Console.WriteLine(string.Concat(words));
            } while (System.Console.ReadLine() != "q");
        }
    }
}