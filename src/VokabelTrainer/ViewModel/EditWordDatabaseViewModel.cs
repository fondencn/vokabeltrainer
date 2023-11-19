using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        private string _NewEntryInfo;
        private LessonViewModel _SelectedLesson;
        private string _NewLessonName;
        private string _NewEntryKey;
        private string _NewEntryValue;


        public override string Title => "Edit training data";

        public ObservableCollection<LessonViewModel> Lessons { get; } = new ObservableCollection<LessonViewModel>();

        public LessonViewModel SelectedLesson 
        { 
            get => _SelectedLesson;
            set => SetProperty(ref _SelectedLesson, value); 
        }


        public string NewLessonName
        {
            get { return _NewLessonName; }
            set { SetProperty(ref _NewLessonName, value); }
        }


        public string NewEntryKey
        {
            get { return _NewEntryKey; }
            set { SetProperty(ref _NewEntryKey, value); }
        }


        public string NewEntryValue
        {
            get { return _NewEntryValue; }
            set { SetProperty(ref _NewEntryValue, value); }
        }


        public string NewEntryInfo
        {
            get { return _NewEntryInfo; }
            set { SetProperty(ref _NewEntryInfo, value); }
        }

        public bool IsLessonSelected => this.SelectedLesson != null;

        public EditWordDatabaseViewModel()
        {
            this.AddPropertyChangedHandler(nameof(SelectedLesson), () => OnPropertyChanged(nameof(IsLessonSelected)));
        }

        public void Load()
        {
            this.Lessons.Clear();
            this.SelectedLesson = null;
            this.NewEntryInfo = null;
            this.NewEntryKey = null;
            this.NewEntryValue = null;
            this.NewLessonName = null;

            foreach(var item in CommonServices.Instance.Database.Lessons
                .Include(item => item.Words)
                .Select(item => new LessonViewModel(item)))
            {
                this.Lessons.Add(item);
            }
            OnPropertyChanged("Lessons");
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
            if (!String.IsNullOrWhiteSpace(NewEntryKey) && !String.IsNullOrWhiteSpace(NewEntryValue) && this.SelectedLesson != null)
            {
                WordItem item = new WordItem()
                {
                    ForeignWord = NewEntryKey,
                    OwnWord = NewEntryValue, 
                    Info = NewEntryInfo ?? String.Empty, 
                    Id_Lesson = this.SelectedLesson.Id,
                    Lesson = this.SelectedLesson.Model
                };
                CommonServices.Instance.Database.Words.Add(item);
                CommonServices.Instance.Database.SaveChanges();
                WordViewModel wvm = new WordViewModel(item);
                this.SelectedLesson.Words.Add(wvm);
                this.NewEntryKey = null;
                this.NewEntryValue = null;
                this.NewEntryInfo = null;
            }
        }

        public void RemoveWord(WordViewModel item)
        {
            if(item != null)
            {
                CommonServices.Instance.Database.Words.Remove(item.Model);
                CommonServices.Instance.Database.SaveChanges();
                this.SelectedLesson.Words.Remove(item);
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
                this.NewLessonName = null;
            }
        }


        public void RemoveLesson(LessonViewModel item)
        {
            if (item != null)
            {
                foreach(var word in item.Words)
                {
                    CommonServices.Instance.Database.Words.Remove(word.Model);
                }
                CommonServices.Instance.Database.Lessons.Remove(item.Model);
                CommonServices.Instance.Database.SaveChanges();
                this.Lessons.Remove(item);
            }
        }
    }
}
