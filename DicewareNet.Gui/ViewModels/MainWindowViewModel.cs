using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using DicewareNet.Dice;
using DicewareNet.Gui.Properties;
using DicewareNet.WordList;
using FlickrNet;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace DicewareNet.Gui.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public const int NumberOfDice = 5;
        public const int NumberOfRolls = 7;
        public static IRandom Rng = new CryptoRandom();
        private readonly Flickr _flickr;

        private ICommand _generateCommand;

        private readonly IWordList _wordList;

        private ObservableCollection<WordImage> _words;

        public MainWindowViewModel()
        {
            _wordList = new WordListWeb("http://world.std.com/~reinhold/diceware.wordlist.asc");
                // new WordListFile("..\\..\\diceware_wordlist.txt");
            _flickr = new Flickr(Settings.Default.FlickrAPIKey);
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
                    var diceRolls = Enumerable.Range(0, NumberOfRolls).Select(_ => Rng.DiceRoll(NumberOfDice));
                    Words =
                        new ObservableCollection<WordImage>(
                            _wordList.Lookup(diceRolls, " ").Select(w => new WordImage(w)));

                    foreach (var wordImage in Words)
                    {
                        Uri wordImageUri;
                        var photo = await SearchForPhotoAsync(wordImage.Word);
                        var photoResult = photo.FirstOrDefault();
                        if (photoResult != null &&
                            Uri.TryCreate(photoResult.LargeSquareThumbnailUrl, UriKind.Absolute, out wordImageUri))
                        {
                            wordImage.Image = new BitmapImage(wordImageUri);
                        }
                    }
                }));
            }
        }

        private Task<PhotoCollection> SearchForPhotoAsync(string word)
        {
            var t = new TaskCompletionSource<PhotoCollection>();
            var options = new PhotoSearchOptions
            {
                Tags = word,
                PerPage = 1,
                Page = 1,
                Extras = PhotoSearchExtras.ThumbnailUrl
            };
            _flickr.PhotosSearchAsync(options, s => t.TrySetResult(s.Result));
            return t.Task;
        }
    }
}