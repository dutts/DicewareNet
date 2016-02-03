using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DicewareNet.Dice;
using DicewareNet.Gui.ImageSource;
using DicewareNet.WordList;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace DicewareNet.Gui.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public const int NumberOfDice = 5;
        public const int NumberOfRolls = 7;
        public static IDice Rng = new CryptoDice();
        
        private ICommand _generateCommand;

        private readonly IWordList _wordList;
        private readonly IImageSource _imageSource;

        private ObservableCollection<WordImage> _words;

        public MainWindowViewModel()
        {
            _wordList = new WordListWeb8k();
            _imageSource = new FlickrImageSource();
        }

        public ObservableCollection<WordImage> Words
        {
            get { return _words; }
            set { SetProperty(ref _words, value); }
        }

        public ICommand GenerateCommand
        {
            get
            {
                return _generateCommand ?? (_generateCommand = new DelegateCommand(async () =>
                {
                    Words = new ObservableCollection<WordImage>();
                    var diceRolls = Enumerable.Range(0, NumberOfRolls).Select(_ => Rng.Roll(NumberOfDice));
                    Words =
                        new ObservableCollection<WordImage>(
                            _wordList.Lookup(diceRolls, " ").Select(w => new WordImage(w)));

                    foreach (var wordImage in Words)
                    {
                        wordImage.Image = await _imageSource.GetImageForWordAsync(wordImage.Word);
                    }
                }));
            }
        }
    }
}