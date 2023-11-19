using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Model;

namespace VokabelTrainer.ViewModel
{
    public class LessonViewModel : BaseViewModel<Lesson>, IEquatable<LessonViewModel>, IComparable<LessonViewModel>
    {
        public int Id => Model.Id;
        private string _name;

        public string Name 
        {
            get => _name;
            set => SetProperty(ref _name, value); 
        }

        public LessonViewModel(Lesson model) : base(model)
        {
            this.Name = model.Name;
            this.Words = new ObservableCollection<WordViewModel>(model.Words
                .Select(item => new WordViewModel(item)));
        }

        public ObservableCollection<WordViewModel> Words { get; } 

        public int CompareTo(LessonViewModel other)
        {
            return this.Id.CompareTo(other?.Id);
        }

        public bool Equals(LessonViewModel other)
        {
            return this.Id.Equals(other?.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
