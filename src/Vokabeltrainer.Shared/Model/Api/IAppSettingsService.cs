using VokabelTrainer.Model;

namespace VokabelTrainer.Model.Api
{
    public interface IAppSettingsService
    {
        AppSettings Load();
        void Save(AppSettings settings);
    }
}