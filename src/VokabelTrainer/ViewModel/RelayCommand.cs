using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VokabelTrainer.ViewModel
{
    internal class RelayCommand<T> : ICommand 
    {
        private readonly Func<T, bool> canExecute;
        private readonly Action<T> execute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute) : this(null, execute)
        {
        }

        public RelayCommand (Func<T, bool> canExecute, Action<T> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public void RaiseCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => canExecute == null ? true : canExecute((T)parameter);

        public void Execute(object parameter) => execute((T)parameter);
    }

    internal class RelayCommand : ICommand
    {
        private readonly Func<bool> canExecute;
        private readonly Action execute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute) : this(null, execute)
        {
        }

        public RelayCommand(Func<bool> canExecute, Action execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public void RaiseCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => canExecute == null ? true : canExecute();

        public void Execute(object parameter) => execute();
    }


    internal class AsyncRelayCommand : ICommand
    {
        private readonly Func<bool> canExecute;
        private readonly Func<Task> execute;

        public event EventHandler CanExecuteChanged;

        public AsyncRelayCommand(Func<Task> execute) : this(null, execute)
        {
        }

        public AsyncRelayCommand(Func<bool> canExecute, Func<Task> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public void RaiseCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => canExecute == null ? true : canExecute();

        public void Execute(object parameter) => execute();
    }
}
