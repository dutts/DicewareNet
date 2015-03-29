using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using DicewareNet.Dice;
using DicewareNet.WordList;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace DicewareNet.Gui.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public const int NumberOfDice = 5;
        public const int NumberOfRolls = 7;
        public static ClrRandom Rng = new ClrRandom();

        private IWordList _wordList;

        private ObservableCollection<string> _words;
        public ObservableCollection<string> Words
        {
            get { return _words; }
            set { SetProperty(ref _words, value); }
        }

        private ICommand _generateCommand;
        public ICommand GenerateCommand 
        {
            get
            {
                return _generateCommand ?? (_generateCommand = new DelegateCommand(() =>
                {
                    var diceRolls = Enumerable.Range(0, NumberOfRolls).Select(_ => Rng.DiceRoll(NumberOfDice));
                    var words = _wordList.Lookup(diceRolls, " ");
                    Words = new ObservableCollection<string>(words);
                }));
            }
        }

        public MainWindowViewModel()
        {
            _wordList = new WordListFile("..\\..\\diceware_wordlist.txt");
        }
    }
}
