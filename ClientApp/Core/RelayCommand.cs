using System;
using System.Windows.Input;

namespace ClientApp.Core
{
    internal class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action<object?> _execute;
        private Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter = null)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter = null)
        {
            _execute(parameter);
        }
    }
}
