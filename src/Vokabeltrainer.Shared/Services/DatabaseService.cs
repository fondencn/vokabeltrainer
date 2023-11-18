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
        #region IDatabase Implementation
        void IDatabase.SaveChanges() => this.SaveChanges();

        public DbSet<Lesson> Lessons => this.Set<Lesson>();
        public DbSet<WordItem> Words => this.Set<WordItem>();
        public DbSet<TrainingRun> Runs => this.Set<TrainingRun>();
        #endregion






#if IS_MODEL
        private const string Settings_DBPath = "../VokabelTrainer/Resources/Raw/Vokabeln.db";
#else
        private static string Settings_DBPath
        {
            get
            {
                string fileInSettings = CommonServices.Instance.Settings.Load().DBPath;
                if (Path.IsPathFullyQualified(fileInSettings))
                {
                    return fileInSettings;
                } 
                else
                {
                    string realPath = Path.Combine(FileSystem.Current.AppDataDirectory, fileInSettings.TrimStart('.', '/'));
                    return realPath;
                }
            }
        }
#endif

        public DatabaseService()
        {
                
        }

        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {
                
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if !IS_MODEL
            CommonServices.Instance.Logging.LogDebug("Using DB " + Settings_DBPath);
            CommonServices.Instance.Logging.LogDebug("DB Fulll Path interpretation is " + Path.GetFullPath(Settings_DBPath));
#endif
            optionsBuilder.UseSqlite("Filename=" + Settings_DBPath);
            base.OnConfiguring(optionsBuilder);
        }

        
    }
}
