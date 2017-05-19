using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BackOfficeEcostat.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginPage1.xaml
    /// </summary>
    public partial class Accueil : Page
    {
        public Accueil()
        {
            InitializeComponent();
        }

        private void modifier_S_Click(object sender, RoutedEventArgs e)
        {
            modifSondage page = new modifSondage();
            NavigationService.Navigate(page);
        }

        private void ajouter_S_Click(object sender, RoutedEventArgs e)
        {
            ajouterSondage page = new ajouterSondage();
            NavigationService.Navigate(page);
        }

        private void analyses_S_Click(object sender, RoutedEventArgs e)
        {
            resultatsSondage page = new resultatsSondage();
            NavigationService.Navigate(page);
        }

        private void modifier_E_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ajouter_E_Click(object sender, RoutedEventArgs e)
        {
            ajouterEnquete page = new ajouterEnquete();
            NavigationService.Navigate(page);
        }
    }
}
