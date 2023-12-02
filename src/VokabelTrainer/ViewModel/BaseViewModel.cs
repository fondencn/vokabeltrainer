using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VokabelTrainer.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        public virtual string Title { get; } = "Kein Titel";


        private readonly Dictionary<string, List<Action>> _InternalPropertyChangedHandlers = new Dictionary<string, List<Action>>();

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
                foreach(Action action in _InternalPropertyChangedHandlers[caller])
                {
                    if (MainThread.IsMainThread)
                    {
                        action();
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(() => action());
                    }
                }
            }

            if (_InternalPropertyChangedHandlers.ContainsKey("AllProperties"))
            {
                foreach (Action action in _InternalPropertyChangedHandlers["AllProperties"])
                {
                    if (MainThread.IsMainThread)
                    {
                        action();
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(() => action());
                    }
                }
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


        protected void AddPropertyChangedHandler(string propName, Action handler)
        {
            string key = propName ?? "AllProperties";
            if (!_InternalPropertyChangedHandlers.ContainsKey(key))
            {
                _InternalPropertyChangedHandlers.Add(key, new List<Action>() { handler });
            } 
            else
            {
                _InternalPropertyChangedHandlers[key].Add(handler);
            }
        }
    }


    public abstract class BaseViewModel<TModel> : BaseViewModel
    {
        public TModel Model { get; }
        public BaseViewModel(TModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model), "Es muss ein gültiges Model-Objekt vom Typ " + typeof(TModel).Name + " übergeben werden!");
            }
            Model = model;  
        }
    }
}
