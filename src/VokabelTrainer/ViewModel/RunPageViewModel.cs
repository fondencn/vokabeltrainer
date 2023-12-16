using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VokabelTrainer.Model;
using VokabelTrainer.Services;

namespace VokabelTrainer.ViewModel
{
    public class RunPageViewModel : BaseViewModel
    {
        private bool _isCurrentGuessCorrect;
        private bool _isCurrentGuessWrong;
        private bool _isTippRequested;
        private string _currentGuess;
        private WordViewModel _currentWord;
        private RelayCommand _guessCommand = null;
        private RelayCommand _continueCommand = null;
        private RelayCommand _showTippCommand = null;

        public override string Title => "Training for Lesson  \"" + Lesson.Name + "\"";

        public double PercentCorrect => Run.PercentCorrect;

        public LessonViewModel Lesson { get; }

        public WordViewModel CurrentWord 
        { 
            get => _currentWord;
            private set => SetProperty(ref _currentWord, value);
        }

        public TrainingRun Run { get; }

        public RunPageViewModel(LessonViewModel lesson)
        {
            Lesson = lesson;
            Run = new TrainingRun();
            Run.Lesson = lesson.Model;
            Run.Id_Lesson = lesson.Id;
            Run.StartDate = DateTime.Now;
            CommonServices.Instance.Database.Runs.Add(Run);
            CommonServices.Instance.Database.SaveChanges();
            this.Continue();

            this.AddPropertyChangedHandler(nameof(CurrentGuess), () => OnPropertyChanged(nameof(IsCheckEnabled)));
            this.AddPropertyChangedHandler(nameof(IsCurrentGuessCorrect), () => OnPropertyChanged(nameof(IsCheckEnabled)));
            this.AddPropertyChangedHandler(nameof(IsCurrentGuessCorrect), RaiseGuessChanged);
        }

        public event EventHandler<bool> GuessChanged;

        private void RaiseGuessChanged()
        {
            if (!this._isTippRequested)
            {
                this.GuessChanged?.Invoke(this, IsCurrentGuessCorrect);
            }
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
            set => SetProperty(ref _currentGuess, value?.Trim()); 
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


        public ICommand ShowTippCommand
        {
            get
            {
                if (_showTippCommand == null)
                {
                    _showTippCommand = new RelayCommand(ShowTipp);
                }
                return _showTippCommand;
            }
        }



        public void Guess()
        {
            if(this.IsCurrentGuessCorrect)
            {
                /* Enter Taste bei bereits korrekter Eingabe macht weiter mit dem nächsten Wort */
                Continue();
                return;
            }

            bool isCorrect = String.Equals( this.CurrentGuess, this.CurrentWord.OwnWord, StringComparison.CurrentCulture );
            this.IsCurrentGuessCorrect = isCorrect;
            this.IsCurrentGuessWrong = !isCorrect;

            TrainingItem trainingItem = new TrainingItem()
            {
                Id_TrainingRun = this.Run.Id, 
                Id_Word = this.CurrentWord.Id, 
                IsCorrect = isCorrect, 
                TrainingDate = DateTime.Now, 
                Word = this.CurrentWord.Model, 
                Run = this.Run
            };
            this.Run.Items.Add(trainingItem);
            CommonServices.Instance.Database.RunItems.Add(trainingItem);
            CommonServices.Instance.Database.SaveChanges();

            this.OnPropertyChanged(nameof(PercentCorrect));
        }

        public void Continue()
        {
            /* Nächstes Wort berechnen und Eingabe zurücksetzen... */
            this.CurrentGuess = null;
            this.CurrentWord = null;
            this.IsCurrentGuessCorrect = false;
            this.IsCurrentGuessWrong = false;


            this.CurrentWord = new WordViewModel( CommonServices.Instance.Propability.GetNextWord());
        }

        public void EndRun()
        {
            this.Run.EndDate = DateTime.Now;
            CommonServices.Instance.Database.SaveChanges();
        }

        public void ShowTipp()
        {
            this._isTippRequested = true; //supresses animation on ui
            this.CurrentGuess = this.CurrentWord.OwnWord;
            this.IsCurrentGuessCorrect = true;
            this.IsCurrentGuessWrong = false;
            this._isTippRequested = false;
        }
    }
}
