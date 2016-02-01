using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Mvvm;

namespace DicewareNet.Gui.ViewModels
{
    public class WordImage : BindableBase
    {
        private BitmapImage _image;

        public WordImage(string word)
        {
            Word = word;
        }

        public string Word { get; }

        public BitmapImage Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }
    }
}