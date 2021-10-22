using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecruitmentWpfApp.Commands
{
    public class AsyncRelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler OnCompletion;
        public event EventHandler OnFailure;

        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public void ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    // Schedule task
                    Task<Task> t = Task.Factory.StartNew(() => _execute(), TaskCreationOptions.None);
                    t.Result.ContinueWith(TaskCompletion, TaskContinuationOptions.OnlyOnRanToCompletion);
                    t.Result.ContinueWith(TaskFailure, TaskContinuationOptions.OnlyOnFaulted);
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

        private void TaskCompletion(Task t)
        {
            OnCompletion?.Invoke(this, EventArgs.Empty);
        }

        private void TaskFailure(Task t)
        {
            OnFailure?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync();
        }
    }

}