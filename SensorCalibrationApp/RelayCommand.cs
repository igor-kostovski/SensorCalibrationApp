using System;
using System.Windows.Input;

namespace SensorCalibrationApp
{
    public class RelayCommand : ICommand
    {
        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;

        public RelayCommand(Action executeMethod)
        {
            _executeMethod = executeMethod;
        }

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteMethod == null)
                return _executeMethod != null;

            return _canExecuteMethod();
        }

        public void Execute(object parameter)
        {
            _executeMethod?.Invoke();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _parameterizedExecuteMethod;
        private readonly Func<T, bool> _parameterizedCanExecuteMethod;

        public RelayCommand(Action<T> executeMethod)
        {
            _parameterizedExecuteMethod = executeMethod;
        }

        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _parameterizedExecuteMethod = executeMethod;
            _parameterizedCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (_parameterizedCanExecuteMethod == null)
                return _parameterizedExecuteMethod != null;

            return _parameterizedCanExecuteMethod((T)parameter);
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter)
        {
            _parameterizedExecuteMethod?.Invoke((T)parameter);
        }
    }
}
