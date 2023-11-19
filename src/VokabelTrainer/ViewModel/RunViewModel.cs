using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Model;

namespace VokabelTrainer.ViewModel
{
    public class RunViewModel : BaseViewModel<TrainingRun>
    {
        public string StartDate => Model.StartDate.ToString("dd.MM.yy HH:mm");

        public string EndDate => Model.EndDate.ToString("dd.MM.yy HH:mm");

        public string PercentCorrect => String.Format("{0:P1}", Model.PercentCorrect);

        public int Count => Model.Count;

        public string LessonName => Model.Lesson.Name;

        public RunViewModel(TrainingRun model) : base(model)
        {
        }
    }
}
