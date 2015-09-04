using System;
using System.Windows.Input;
using System.Windows.Media;
using SystemColors.ViewModels;

namespace SystemColors
{
    public class SysColorViewModal : ViewModelBase
    {
        public string Name { get; private set; }
        public string Data { get; private set; }
        public SolidColorBrush ColorItem { get; private set; }
        public ICommand CopyCommand { get; private set; }

        private ICopyService m_CopyService;

        public SysColorViewModal(string name, SolidColorBrush brush, ICopyService copyService)
        {
            if(copyService == null)
            {
                throw new ArgumentNullException("copyService");
            }

            Name = name;
            Data = brush.ToString();
            ColorItem = brush;
            
            CopyCommand = new CommandHandler(CopyAction, true);
            m_CopyService = copyService;
        }

        private void CopyAction(object obj)
        {
            m_CopyService.Copy(Name);
        }
    };
}
