using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;

namespace DicewareNet.Console
{
    internal class Program
    {
        public const int NumberOfDice = 5;
        public const int NumberOfRolls = 7;
        public static Random Rng = new Random();

        private static void Main(string[] args)
        {
            // Load in the word dict
            var wordDict = new Dictionary<long, string>();
            using (var fr = new StreamReader("..\\..\\diceware_wordlist.txt"))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var lineParts = line.Split('\t');
                    if (lineParts.Count() != 2) continue;

                    long key;
                    if (Int64.TryParse(lineParts[0], out key))
                    {
                        wordDict.Add(key, lineParts[1].Trim());
                    }
                }
            }

            while (System.Console.ReadLine() != "q")
            {
                // Generate words "randomly"
                var words =
                    Enumerable.Range(0, NumberOfRolls)
                        .Select(_ => DiceRoll())
                        .Select(roll => wordDict[roll] + " ")
                        .ToList();

                System.Console.WriteLine(String.Concat(words));
            }
        }

        public static long DiceRoll()
        {
            var diceRolls = new List<int>(NumberOfDice);
            diceRolls.AddRange(Enumerable.Range(0, NumberOfDice).Select(_ => Rng.Next(1, 7)));

            long finalNumber = 0;
            for (var power = 0; power < NumberOfDice; power++)
            {
                finalNumber += (long) (diceRolls[(NumberOfDice - 1) - power]*Math.Pow(10.0, (double) power));
            }

            return finalNumber;
        }
    }
}
