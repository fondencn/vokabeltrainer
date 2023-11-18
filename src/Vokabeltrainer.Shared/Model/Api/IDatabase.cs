using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VokabelTrainer.Model.Api
{
    public interface IDatabase
    {
        DbSet<Lesson> Lessons { get;  }
        DbSet<WordItem> Words { get; }
        DbSet<TrainingRun> Runs { get; }

        void SaveChanges();
    }
}
