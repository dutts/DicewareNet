using System.Threading.Tasks;

namespace DicewareNet.Dice
{
    public interface IDice
    {
        Task<long> RollAsync(int numberOfDice);
    }
}