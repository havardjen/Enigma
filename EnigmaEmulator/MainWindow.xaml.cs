using EnigmaComponents.Classes;
using EnigmaResources.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace EnigmaEmulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainWindowViewModel(new Steckerbrett());
            //_vm.AdvancedIsChecked = false; // Default to not showing advanced options
            DataContext = _vm;
            keyboard.DataContext = this;
            textInputLabel.DataContext = this;
            btnCodeDecode.DataContext = this;

            _vm.AdvancedIsCheckedChanged += _vm_AdvancedIsCheckedChanged;
        }

        private MainWindowViewModel _vm;


        public Visibility AdvancedVisibility
        { 
            get 
            { 
                if(_vm.AdvancedIsChecked) 
                    return Visibility.Visible; 
                else 
                    return Visibility.Collapsed; 
            }
        }

        public Visibility KeyboardVisibility
        {
            get
            {
                if (_vm.AdvancedIsChecked)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }

        private void btnCodeDecode_Click(object sender, RoutedEventArgs e)
        {
            _vm.CodeDecodeMessage(); 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void _vm_AdvancedIsCheckedChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(AdvancedVisibility));
            OnPropertyChanged(nameof(KeyboardVisibility));
        }
    }
}
