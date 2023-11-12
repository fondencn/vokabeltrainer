using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Model;
using VokabelTrainer.Model.Api;

namespace VokabelTrainer.Services
{
    public class DatabaseService : DbContext, IDatabase
    {
        private DbSet<Lesson> Lessons => this.Set<Lesson>();

        IList<Lesson> IDatabase.Lessons => this.Lessons.ToList();

        private DbSet<WordItem> Words => this.Set<WordItem>();

        IList<WordItem> IDatabase.Words => this.Words.ToList();

        private DbSet<TrainingRun> Runs => this.Set<TrainingRun>();

        IList<TrainingRun> IDatabase.Runs => this.Runs.ToList();

#if IS_MODEL
        private const string Settings_DBPath = "../VokabelTrainer/Resources/Raw/Vokabeln.db";
#else
        private static string Settings_DBPath => CommonServices.Instance.Settings.Load().DBPath;
#endif

        public DatabaseService()
        {
                
        }

        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {
                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=" + Settings_DBPath);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
