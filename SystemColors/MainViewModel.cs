using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Media;
using SystemColors.ViewModels;

namespace SystemColors
{
    public interface ICopyService
    {
        void Copy(string name);
    };

    public class MainViewModel : ViewModelBase, ICopyService
    {
        private readonly ObservableCollection<SysColorViewModal> m_Colors =
            new ObservableCollection<SysColorViewModal>();

        public ObservableCollection<SysColorViewModal> Colors
        {
            get { return m_Colors; }
        }

        private string m_XamlText;
        public string XamlText
        {
            get { return m_XamlText; }
            set
            {
                if(m_XamlText != value)
                {
                    m_XamlText = value;
                    OnPropertyChanged("XamlText");
                }
            }
        }

        public MainViewModel()
        {
            var type = typeof(System.Windows.SystemColors);
            var members = type.GetMembers(); 
            foreach (var member in members) 
            {
                var name = member.Name;           
                if(member.MemberType == MemberTypes.Method)
                {
                    if (name.Contains("get_"))
                    {
                        var method = type.GetMethod(name);
                        var result = method.Invoke(null, new object[] {});
                        var col = result as SolidColorBrush;
                        if(col != null)
                        {
                            m_Colors.Add(new SysColorViewModal(name.Substring(4), col, this));
                        }
                    }
                }
            }
        }

        public void Copy(string name)
        {
            XamlText = string.Format("{{DynamicResource {{x:Static SystemColors.{0}Key}}}}", name);

        }
    }
}
