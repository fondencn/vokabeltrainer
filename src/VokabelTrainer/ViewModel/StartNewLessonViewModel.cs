using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VokabelTrainer.Services;

namespace VokabelTrainer.ViewModel
{
    public class StartNewLessonViewModel : BaseViewModel
    {
        private LessonViewModel _selectedLesson;
        private RelayCommand _startLessonCommand = null;

        public override string Title => "Start new training";

        public ObservableCollection<LessonViewModel> Lessons { get; } = new ObservableCollection<LessonViewModel>();

        public LessonViewModel SelectedLesson { get => _selectedLesson; set => this.SetProperty(ref _selectedLesson, value); }

        public StartNewLessonViewModel()
        {
            Load();
        }

        public void Load()
        {
            this.Lessons.Clear();
            this.SelectedLesson = null;
            foreach (var item in CommonServices.Instance.Database.Lessons)
            {
                this.Lessons.Add(new LessonViewModel(item));
            }
        }

        public ICommand StartLessonCommand
        {
            get
            {
                if (_startLessonCommand == null)
                {
                    _startLessonCommand = new RelayCommand(StartLesson);
                }
                return _startLessonCommand;
            }
        }

        public void StartLesson()
        {
            if(this.SelectedLesson != null)
            {

            }
        }
    }
}
