using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VokabelTrainer.Services
{
    public class DialogService
    {
        private readonly Page _page;
        public DialogService(MainPage page)
        {
            this._page = page;
        }

        public Task ShowMessageAsync(string title, string msg, string ok = "OK")
        {
            return this._page.DisplayAlert(title, msg, ok);
        }
        public Task<string> ProtmpAsync(string title, string msg, string accept = "Ja", string cancel = "Nein")
        {
            return this._page.DisplayPromptAsync(title, msg, accept, cancel);
        }
    }
}
