using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VokabelTrainer.Model;
using VokabelTrainer.Services;

namespace VokabelTrainer.ViewModel
{
    public class EditWordDatabaseViewModel : BaseViewModel
    {
        private RelayCommand _AddLessonCommand = null;
        private RelayCommand<LessonViewModel> _RemoveLessonCommand = null;
        private RelayCommand _AddWordCommand = null;
        private RelayCommand<WordViewModel> _RemoveWordCommand = null;


        public override string Title => "Edit training data";

        public ObservableCollection<LessonViewModel> Lessons { get; } = new ObservableCollection<LessonViewModel>();
        public ObservableCollection<WordViewModel> Words  { get; } = new ObservableCollection<WordViewModel>();

        private string _NewLessonName;

        public string NewLessonName
        {
            get { return _NewLessonName; }
            set { SetProperty(ref _NewLessonName, value); }
        }

        private string _NewEntryKey;

        public string NewEntryKey
        {
            get { return _NewEntryKey; }
            set { SetProperty(ref _NewEntryKey, value); }
        }

        private string _NewEntryaValue;

        public string NewEntryValue
        {
            get { return _NewEntryaValue; }
            set { SetProperty(ref _NewEntryaValue, value); }
        }



        public EditWordDatabaseViewModel()
        {
            Load();
        }

        public void Load()
        {
            this.Words.Clear();
            this.Lessons.Clear();
            foreach (var item in CommonServices.Instance.Database.Words.Select(item => new WordViewModel(item)))
            {
                this.Words.Add(item);
            }
            foreach(var item in CommonServices.Instance.Database.Lessons.Select(item => new LessonViewModel(item)))
            {
                this.Lessons.Add(item);
            }
        }

        public ICommand AddWordCommand
        {
            get
            {
                if (_AddWordCommand == null)
                {
                    _AddWordCommand = new RelayCommand(AddWord);
                }
                return _AddWordCommand;
            }
        }

        public ICommand AddLessonCommand
        {
            get
            {
                if (_AddLessonCommand == null)
                {
                    _AddLessonCommand = new RelayCommand(AddLesson);
                }
                return _AddLessonCommand;
            }
        }

        public ICommand RemoveWordCommand
        {
            get
            {
                if (_RemoveWordCommand == null)
                {
                    _RemoveWordCommand = new RelayCommand<WordViewModel>(RemoveWord);
                }
                return _RemoveWordCommand;
            }
        }

        public ICommand RemoveLessonCommand
        {
            get
            {
                if (_RemoveLessonCommand == null)
                {
                    _RemoveLessonCommand = new RelayCommand<LessonViewModel>(RemoveLesson);
                }
                return _RemoveLessonCommand;
            }
        }

        public void AddWord()
        {
            if (!String.IsNullOrWhiteSpace(NewEntryKey) && !String.IsNullOrWhiteSpace(NewEntryValue))
            {
                WordItem item = new WordItem()
                {
                    ForeignWord = NewEntryKey,
                    OwnWord = NewEntryValue
                };
                CommonServices.Instance.Database.Words.Add(item);
                CommonServices.Instance.Database.SaveChanges();
                this.Words.Add(new WordViewModel(item));
            }
        }

        public void RemoveWord(WordViewModel item)
        {
            if(item != null)
            {
                CommonServices.Instance.Database.Words.Remove(item.Model);
                CommonServices.Instance.Database.SaveChanges();
                this.Words.Remove(item);
            }
        }

        public void AddLesson()
        {
            if (!String.IsNullOrWhiteSpace(NewLessonName))
            {
                Lesson item = new Lesson()
                {
                    Name = NewLessonName,
                };
                CommonServices.Instance.Database.Lessons.Add(item);
                CommonServices.Instance.Database.SaveChanges();
                this.Lessons.Add(new LessonViewModel(item));
            }
        }


        public void RemoveLesson(LessonViewModel item)
        {
            if (item != null)
            {
                CommonServices.Instance.Database.Lessons.Remove(item.Model);
                CommonServices.Instance.Database.SaveChanges();
                this.Lessons.Remove(item);
            }
        }
    }
}
