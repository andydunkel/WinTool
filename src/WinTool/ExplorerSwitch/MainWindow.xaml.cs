using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using WinToolLogic.Registry;

namespace ExplorerSwitch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly RegistryUtil _registryUtil;

        private const string KEY_BASE = "Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}";
        private const string KEY_COMPLETE = "Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\\InprocServer32";

        public MainWindow()
        {
            InitializeComponent();
            _registryUtil = new RegistryUtil();
            SetCurrentMode();
        }

        private string _currentMode = "";

        public void SetCurrentMode() 
        {
            var reg = _registryUtil.GetRegistryKeyUser(KEY_COMPLETE);
            BorderClassic.BorderThickness = new Thickness(0);
            BorderModern.BorderThickness = new Thickness(0);

            if (reg != null)
            {
                _currentMode = Properties.Resources.MainWindow_SetCurrentMode_Classic_context_menu;
                TextBlockCurrentMode.Text = _currentMode;
                BorderClassic.BorderThickness = new Thickness(2);
            }
            else
            {
                _currentMode = Properties.Resources.MainWindow_SetCurrentMode_Modern_context_menu;
                TextBlockCurrentMode.Text = _currentMode;
                BorderModern.BorderThickness = new Thickness(2);
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void ButtonClassicMenu_OnClick(object sender, RoutedEventArgs e)
        {
            //DELETE "HKCU\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}"
            
            var reg = _registryUtil.GetRegistryKeyUser(KEY_COMPLETE);

            if (reg == null)
            {
                _registryUtil.CreateRegistryKeyUser(KEY_COMPLETE);
            }

            SetCurrentMode();
            ShowMessage();
        }

        private void ButtonNewMenu_OnClick(object sender, RoutedEventArgs e)
        {
            //"HKCU\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32"
            
            var reg = _registryUtil.GetRegistryKeyUser(KEY_BASE);

            if (reg != null)
            {
                _registryUtil.DeleteUserKey(KEY_BASE);
            }

            SetCurrentMode();
            ShowMessage();
        }

        private void ShowMessage()
        {
            MessageBox.Show(Properties.Resources.MainWindow_ShowMessage_Please_restart_computer_to_apply_the_changes_, Properties.Resources.MainWindow_ShowMessage_Switched, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
