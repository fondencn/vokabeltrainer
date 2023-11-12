using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VokabelTrainer.Model.Api
{
    public interface IDatabase
    {
        IList<Lesson> Lessons { get;  }
        IList<WordItem> Words { get; }
        IList<TrainingRun> Runs { get; }
    }
}
