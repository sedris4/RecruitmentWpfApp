using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecruitmentWpfApp.Commands
{
    public class ParameterizedAsyncRelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler OnCompletion;
        public event EventHandler OnFailure;

        private bool _isExecuting;
        private readonly Func<T, Task> _execute;
        private readonly Predicate<object> _canExecute;

        public ParameterizedAsyncRelayCommand(Func<T, Task> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        public void ExecuteAsync(object parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    // Schedule task
                    Task executingTask = Task.Run(() => _execute((T)parameter));

                    executingTask.ContinueWith((t) => _isExecuting = false);
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync(parameter);
        }
    }
}