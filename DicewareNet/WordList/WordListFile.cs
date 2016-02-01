using System.IO;
using System.Linq;

namespace DicewareNet.WordList
{
    public class WordListFile : WordList
    {
        public WordListFile(string fileName)
        {
            using (var fr = new StreamReader(fileName))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var lineParts = line.Split('\t');
                    if (lineParts.Count() != 2) continue;

                    long key;
                    if (long.TryParse(lineParts[0], out key))
                    {
                        WordDict.Add(key, lineParts[1].Trim());
                    }
                }
            }
        }
    }
}