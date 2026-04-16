using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public abstract class AsyncCommandBase : ICommand
    {
        private bool _isExecuting;

        public bool IsExecuting
        {
            get => _isExecuting;
            private set
            {
                _isExecuting = value;
                OnCanExecuteChanged();
            }
        }

        public virtual event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter) => !IsExecuting;

        public void Execute(object? parameter)
        {
            ExecuteAndHandleAsync(parameter);
        }

        private async void ExecuteAndHandleAsync(object? parameter)
        {
            IsExecuting = true;
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception ex)
            {
                OnExecutionFailed(ex);
            }
            finally
            {
                IsExecuting = false;
            }
        }

        protected abstract Task ExecuteAsync(object? parameter);

        protected virtual void OnExecutionFailed(Exception ex) { }

        protected void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
