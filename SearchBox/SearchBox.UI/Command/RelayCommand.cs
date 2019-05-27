using System;
using System.Windows.Input;

namespace SearchBox.Command
{
    public class RelayCommand : ICommand
    {
        private Action execute;
        private Func<bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canExecute=null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private Action<T> execute;
        private Func<T,bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute,Func<T,bool> canExecute=null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            execute((T)parameter);
        }
    }
}
