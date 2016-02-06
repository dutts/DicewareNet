using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DicewareNet.Dice
{
    public class CryptoDice : IDice
    {
        private readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
        private readonly byte[] _uint32Buffer = new byte[4];

        public Task<long> RollAsync(int numberOfDice)
        {
            var diceRolls = new List<int>(numberOfDice);
            diceRolls.AddRange(Enumerable.Range(0, numberOfDice).Select(_ => NextRandom(1, 7)));

            long finalNumber = 0;
            for (var power = 0; power < numberOfDice; power++)
            {
                finalNumber += (long)(diceRolls[numberOfDice - 1 - power] * Math.Pow(10.0, power));
            }

            return Task.FromResult(finalNumber);
        }

        private int NextRandom(int minValue, int maxValue)
        {
            long diff = maxValue - minValue;
            while (true)
            {
                _rng.GetBytes(_uint32Buffer);
                var rand = BitConverter.ToUInt32(_uint32Buffer, 0);

                const long max = (1 + (long)uint.MaxValue);
                var remainder = max%diff;

                if (rand < max - remainder)
                {
                    return (int) (minValue + (rand%diff));
                }
            }
        }
    }
}
