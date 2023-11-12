using Microsoft.EntityFrameworkCore;
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



        public DatabaseService(DbContextOptions options) : base(options)
        {
        }

        public static DatabaseService CreateDefault(IAppSettingsService settings)
        {
            AppSettings appSettings = settings.Load();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseSqlite("Filename=" + appSettings.DBPath);
            return new DatabaseService(builder.Options);
        }
    }
}
