using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Services;

namespace VokabelTrainer.ViewModel
{
    public class EditWordDatabaseViewModel : BaseViewModel
    {
        public override string Title => "Edit training data";

        public ObservableCollection<LessonViewModel> Lessons { get; } = new ObservableCollection<LessonViewModel>();
        public ObservableCollection<WordViewModel> Words  { get; } = new ObservableCollection<WordViewModel>();

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
    }
}
