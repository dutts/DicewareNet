using System;
using System.Collections.Generic;
using System.Linq;

namespace DicewareNet.Dice
{
    public class DiceOrg : IDice
    {
        private readonly Random.Org.Random _rng;

        public DiceOrg()
        {
            _rng = new Random.Org.Random();
        }

        public long Roll(int numberOfDice)
        {
            var diceRolls = new List<int>(numberOfDice);
            diceRolls.AddRange(Enumerable.Range(0, numberOfDice).Select(_ => _rng.Next(1, 6)));

            long finalNumber = 0;
            for (var power = 0; power < numberOfDice; power++)
            {
                finalNumber += (long)(diceRolls[numberOfDice - 1 - power] * Math.Pow(10.0, power));
            }

            return finalNumber;
        }
    }
}
