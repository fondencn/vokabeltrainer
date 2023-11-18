using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Model;

namespace VokabelTrainer.ViewModel
{
    public class LessonViewModel : BaseViewModel<Lesson>
    {
        private string _name;

        public string Name 
        {
            get => _name;
            set => SetProperty(ref _name, value); 
        }

        public LessonViewModel(Lesson model) : base(model)
        {
            this.Name = model.Name;
        }
    }
}
