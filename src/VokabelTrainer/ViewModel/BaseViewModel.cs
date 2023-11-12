using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VokabelTrainer.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        public virtual string Title { get; } = "Kein Titel";

        protected void OnPropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public void SetProperty<T>(ref T value, T newVal, [CallerMemberName] string caller = null)
        {
            value = newVal;
            if (_InternalPropertyChangedHandlers.ContainsKey(caller))
            {
                _InternalPropertyChangedHandlers[caller]();
            }
            if (_InternalPropertyChangedHandlers.ContainsKey("AllProperties"))
            {
                _InternalPropertyChangedHandlers["AllProperties"]();
            }

            if (MainThread.IsMainThread)
            {
                OnPropertyChanged(caller);
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(() => OnPropertyChanged(caller));
            }
        }

        private readonly Dictionary<string, Action> _InternalPropertyChangedHandlers = new Dictionary<string, Action>();

        protected void AddPropertyChangedHandler(string propName, Action handler)
        {
            if (!_InternalPropertyChangedHandlers.TryAdd(propName ?? "AllProperties", handler)) 
            {
                _InternalPropertyChangedHandlers[propName ?? "AllProperties"] = handler;
            }
        }
    }


    public abstract class BaseViewModel<TModel> : BaseViewModel
    {
        public TModel Model { get; }
        public BaseViewModel(TModel model)
        {
            Model = model;  
        }
    }
}
