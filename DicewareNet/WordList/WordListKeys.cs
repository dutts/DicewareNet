using System.Collections;
using System.Collections.Generic;

namespace DicewareNet.WordList
{
    public class WordListKeys : IEnumerable<long>
    {
        private const int MinValue = 1;
        private const int MaxValue = 6;

        public IEnumerator<long> GetEnumerator()
        {
            for (var a = MinValue; a <= MaxValue; a++ )
            {
                for (var b = MinValue; b <= MaxValue; b++)
                {
                    for (var c = MinValue; c <= MaxValue; c++)
                    {
                        for (var d = MinValue; d <= MaxValue; d++)
                        {
                            for (var e = MinValue; e <= MaxValue; e++)
                            {
                                yield return a*10000 + b*1000 + c*100 + d*10 + e;
                            }
                        }
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static WordListKeys Keys => new WordListKeys();
    }
}
