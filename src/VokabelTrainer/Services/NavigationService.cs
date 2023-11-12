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

        public Task Navigate<T>()
        {
            T page = CommonServices.Instance.GetPageInstance<T>();

            return Shell.Current.Navigation.PushAsync(page as Page, true);
        }

        public Task Back()
        {
            return Shell.Current.Navigation.PopAsync();
        }
    }
}
