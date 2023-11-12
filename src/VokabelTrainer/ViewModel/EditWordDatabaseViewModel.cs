using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Services;

namespace VokabelTrainer.ViewModel
{
    public class EditWordDatabaseViewModel : BaseViewModel
    {
        private IList<LessonViewModel> _lessons;
        private IList<WordViewModel> _words;

        public override string Title => "Edit training data";

        public IList<LessonViewModel> Lessons { get => _lessons; set => SetProperty(ref _lessons, value); }
        public IList<WordViewModel> Words { get => _words; set => SetProperty(ref _words, value); }

        public EditWordDatabaseViewModel()
        {
            Load();
        }

        public void Load()
        {
            this.Words = CommonServices.Instance.Database.Words.Select(item => new WordViewModel(item)).ToList();
            this.Lessons = CommonServices.Instance.Database.Lessons.Select(item => new LessonViewModel(item)).ToList();
        }
    }
}
