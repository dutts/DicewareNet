using System;
using System.Linq;
using System.Threading.Tasks;
using RandomNet;

namespace DicewareNet.Dice
{
    public class DiceOrg : IDice
    {
        public async Task<long> RollAsync(int numberOfDice)
        {
            var diceRolls = (await IntegerGenerator.GenerateAsync(numberOfDice, 1, 6)).ToArray();

            long finalNumber = 0;
            for (var power = 0; power < numberOfDice; power++)
            {
                finalNumber += (long)(diceRolls[numberOfDice - 1 - power] * Math.Pow(10.0, power));
            }

            return finalNumber;
        }
    }
}
