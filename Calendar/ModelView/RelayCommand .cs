using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calendar.ModelView
{
    public class RelayCommand : ICommand
    {
        private Action<object> _action;
        private Func<bool> _func;
        public RelayCommand(Action<object> action, Func<bool> func)
        {
            _action = action;
            _func = func;
        }

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            if (_func != null)
                return _func();
            return true;
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                _action(parameter);
            }
            else
            {
                _action("NTR");
            }
        }
        #endregion
    }
}
