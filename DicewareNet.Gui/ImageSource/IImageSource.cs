using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DicewareNet.Gui.ImageSource
{
    interface IImageSource
    {
        Task<BitmapImage> GetImageForWordAsync(string word);
    }
}
