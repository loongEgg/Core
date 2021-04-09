using System.Windows;

namespace LoongEgg.Core.Demo
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = IoC.Container.Get<MainViewModel>();
        }
    }
}
