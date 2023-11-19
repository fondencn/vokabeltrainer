using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VokabelTrainer.Services;

namespace VokabelTrainer.ViewModel
{
    public class RunPageViewModel : BaseViewModel
    {
        private bool _isCurrentGuessCorrect;
        private bool _isCurrentGuessWrong;
        private string _currentGuess;
        private WordViewModel _currentWord;
        private RelayCommand _guessCommand = null;
        private RelayCommand _continueCommand = null;

        public override string Title => "Training for Lesson  \"" + Lesson.Name + "\"";

        public LessonViewModel Lesson { get; }

        public WordViewModel CurrentWord 
        { 
            get => _currentWord;
            private set => SetProperty(ref _currentWord, value);
        }

        public RunPageViewModel(LessonViewModel lesson)
        {
            Lesson = lesson;
            this.Continue();

            this.AddPropertyChangedHandler(nameof(CurrentGuess), () => OnPropertyChanged(nameof(IsCheckEnabled)));
            this.AddPropertyChangedHandler(nameof(IsCurrentGuessCorrect), () => OnPropertyChanged(nameof(IsCheckEnabled)));
        }


        public bool IsCurrentGuessCorrect
        {
            get => _isCurrentGuessCorrect; 
            set => SetProperty(ref _isCurrentGuessCorrect, value); 
        }


        public bool IsCurrentGuessWrong
        {
            get => _isCurrentGuessWrong; 
            set => SetProperty(ref _isCurrentGuessWrong, value); 
        }

        public bool IsCheckEnabled => !String.IsNullOrWhiteSpace(this.CurrentGuess) && !this.IsCurrentGuessCorrect;

        public string CurrentGuess
        {
            get => _currentGuess; 
            set => SetProperty(ref _currentGuess, value); 
        }


        public ICommand GuessCommand
        {
            get
            {
                if(_guessCommand == null)
                {
                    _guessCommand = new RelayCommand(Guess);
                }
                return _guessCommand;
            }
        }


        public ICommand ContinueCommand
        {
            get
            {
                if (_continueCommand == null)
                {
                    _continueCommand = new RelayCommand(Continue);
                }
                return _continueCommand;
            }
        }


        public void Guess()
        {
            bool isCorrect = String.Equals( this.CurrentGuess, this.CurrentWord.OwnWord, StringComparison.CurrentCulture );
            this.IsCurrentGuessCorrect = isCorrect;
            this.IsCurrentGuessWrong = !isCorrect;
        }

        public void Continue()
        {
            // Nächstes Wort berechnen und Eingabe zurücksetzen...
            this.CurrentGuess = null;
            this.CurrentWord = null;
            this.IsCurrentGuessCorrect = false;
            this.IsCurrentGuessWrong = false;


            this.CurrentWord = new WordViewModel( CommonServices.Instance.Propability.GetNextWord());
        }
    }
}
