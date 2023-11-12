using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Model.Api;

namespace VokabelTrainer.Services
{
    public class NavigationService : INavigationService
    {
        public NavigationService()
        {
                
        }

        public Task Navigate<T>() where T: Page
        {
            Page page = CommonServices.Instance.GetPageInstance<T>();

            return Shell.Current.Navigation.PushAsync(page, true);
        }

        public Task Back()
        {
            return Shell.Current.Navigation.PopAsync();
        }
    }
}
