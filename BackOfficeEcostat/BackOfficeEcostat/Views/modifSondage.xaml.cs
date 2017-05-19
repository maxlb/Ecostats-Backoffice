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
using BackOfficeEcostat.Model;

namespace BackOfficeEcostat.Views
{
    /// <summary>
    /// Logique d'interaction pour modifSondage.xaml
    /// </summary>
    public partial class modifSondage : Page
    {
        Controller ct;

        public modifSondage()
        {
            InitializeComponent();

            ct = new Controller();

            // Affichage des courses dans la DataGrid
            foreach (questionnaire son in ct.getAllSondages())
            {
                sondages.Items.Add(son);
            }

            // Affichage de la DataGrid
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Titre du sondage";
            col1.IsReadOnly = true;
            col1.Binding = new Binding("Titre");
            sondages.Columns.Insert(0, col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Description du sondage";
            col2.IsReadOnly = true;
            col2.Binding = new Binding("Description");
            sondages.Columns.Insert(1, col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "Thème du sondage";
            col3.IsReadOnly = true;
            col3.Binding = new Binding("theme.nom");
            col3.CanUserSort = true;
            sondages.Columns.Insert(2, col3);

        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            Accueil page = new Accueil();
            NavigationService.Navigate(page);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            detailsSondage page = new detailsSondage(((FrameworkElement)sender).DataContext as questionnaire);
            NavigationService.Navigate(page);
        }
    }
}
