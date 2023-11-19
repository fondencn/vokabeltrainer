using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Model;
using VokabelTrainer.Model.Api;

namespace VokabelTrainer.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        public AppSettings Load()
        {
            AppSettings settings = new AppSettings();
            settings.Username = Preferences.Default.Get("Username", "");
            settings.DBPath = Preferences.Default.Get("DBPath", "./Vokabeln.db");
            settings.IsDarkTheme = Preferences.Default.Get("IsDarkTheme", false);

            return settings;
        }

        public void Save(AppSettings settings)
        {
            Preferences.Default.Set("Username", settings.Username);
            Preferences.Default.Set("DBPath", settings.DBPath);
            Preferences.Default.Set("IsDarkTheme", settings.IsDarkTheme);
        }
    }
}
