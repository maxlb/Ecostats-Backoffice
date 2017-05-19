using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BackOfficeEcostat.Views
{
    /// <summary>
    /// Logique d'interaction pour resultatsSondage.xaml
    /// </summary>
    public partial class resultatsSondage : Page
    {
        public resultatsSondage()
        {
            InitializeComponent();
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            Accueil page = new Accueil();
            NavigationService.Navigate(page);
        }
    }
}
