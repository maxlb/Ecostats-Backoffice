using BackOfficeEcostat.Model;
using BackOfficeEcostat.Views;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace BackOfficeEcostat
{


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // hook on error before app really starts
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            base.OnStartup(e);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // put your tracing or logging code here (I put a message box as an example)
            MessageBox.Show(e.ExceptionObject.ToString());
        }
    }



    /// <summary>
    /// Logique d'interaction pour LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private Controller Control;

        public LoginPage()
        {
            InitializeComponent();
            Control = new Controller();
        }

        private void connexion_Click(object sender, RoutedEventArgs e)
        {
            if (Control.CanLogin(loginTextBox.Text, passwordBox.Password))
            {
                NavigationWindow nw = new NavigationWindow();
                nw.ShowsNavigationUI = false;
                nw.Width = 900;
                nw.Height = 600;
                nw.Title = "SYSTEME DE GESTION ECOSTAT";
                nw.Navigate(new Accueil());
                nw.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Identifiants incorrects");
            }
        }
    }
}
