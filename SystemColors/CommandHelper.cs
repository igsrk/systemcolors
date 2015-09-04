using System;
using System.Windows.Input;

namespace SystemColors
{
    public class CommandHandler : ICommand
    {
        private readonly Action<object> m_Action;
        private readonly bool m_CanExecute;

        public CommandHandler(Action<object> action, bool canExecute)
        {
            m_Action = action;
            m_CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return m_CanExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            m_Action(parameter);
        }
    }
}
