using EnigmaComponents.Classes;
using EnigmaResources.ViewModels;
using System.Windows;

namespace EnigmaEmulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainWindowViewModel(new Steckerbrett());
            DataContext = _vm;
        }

        MainWindowViewModel _vm;
    }
}
