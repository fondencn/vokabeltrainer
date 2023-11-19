using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Model.Api;
using VokabelTrainer.View;

namespace VokabelTrainer.Services
{
    internal class CommonServices
    {
        private IServiceProvider _services;

        public INavigationService Navigation => _services.GetService<INavigationService>();
        public DialogService Dialog => _services.GetService<DialogService>();   
        public LoggingService Logging => _services.GetService<LoggingService>();
        public IDatabase Database => _services.GetService<IDatabase>();
        public IAppSettingsService Settings => _services.GetService<IAppSettingsService>();
        public IPropabilityGenerator Propability => _services.GetService<IPropabilityGenerator>();

        public T GetPageInstance<T>()
        {
            return _services.GetService<T>();
        }

        public static CommonServices Instance { get; } = new CommonServices();
        private CommonServices()
        {

        }

        public MauiApp Initialize(MauiAppBuilder builder)
        {
            // Services (as singletons)
            builder.Services.AddSingleton<LoggingService>();
            builder.Services.AddSingleton<DialogService>();
            IAppSettingsService settings = new AppSettingsService();
            builder.Services.AddSingleton(settings);
            DatabaseService db = new DatabaseService();
            builder.Services.AddSingleton<IDatabase>(db);
            string dbPath = settings.Load().DBPath;
            builder.Services.AddDbContext<DatabaseService>(options => options.UseSqlite("Filename=" + dbPath));
            builder.Services.AddSingleton<IPropabilityGenerator>(new PropabilityGeneratorService());
            builder.Services.AddSingleton<INavigationService>(new NavigationService());

            // Views
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<StartNewLessonPage>();
            builder.Services.AddTransient<ViewStatisticsPage>();



            // Create App Instance and apply deps
            var app =  builder.Build();

            // Remember service registry for easy access later
            _services = app.Services;


            // Apply db schema upon startup
            db.Database.Migrate();
            string actualDbPath = DatabaseService.Settings_DBPath;
            if (!File.Exists(actualDbPath))
            {
                throw new InvalidOperationException("Database file does not exist after db.Database.Migrate() -> Crashing now...");
            }
            return app;
        }
    }
}
