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

        public Page GetPageInstance<T>() where T : Page
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
            builder.Services.AddSingleton<IDatabase>(DatabaseService.CreateDefault(settings));
            builder.Services.AddDbContext<DatabaseService>(options => options.UseSqlite("Filename=" + settings.Load().DBPath));
            builder.Services.AddSingleton<IPropabilityGenerator>(new PropabilityGeneratorService());
            builder.Services.AddSingleton<INavigationService>(new NavigationService());

            // Views
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<StartNewLessonPage>();
            builder.Services.AddTransient<ViewStatisticsPage>();



            var app =  builder.Build();
            _services = app.Services;
            return app;
        }
    }
}
