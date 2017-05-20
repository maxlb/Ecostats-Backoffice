using BackOfficeEcostat.Model;
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
    /// Logique d'interaction pour modifEnquete.xaml
    /// </summary>
    public partial class modifEnquete : Page
    {
        Controller ct;

        public modifEnquete()
        {
            InitializeComponent();

            ct = new Controller();


            foreach (enquete enq in ct.getAllEnquetes())
            {
                enquetes.Items.Add(enq);
            }

            // Affichage de la DataGrid
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Titre de l'enquête";
            col1.IsReadOnly = true;
            col1.Binding = new Binding("titre");
            enquetes.Columns.Insert(0, col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Description de l'enquête";
            col2.IsReadOnly = true;
            col2.Binding = new Binding("description");
            enquetes.Columns.Insert(1, col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "Thème de l'enquête";
            col3.IsReadOnly = true;
            col3.Binding = new Binding("theme.nom");
            col3.CanUserSort = true;
            enquetes.Columns.Insert(2, col3);
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            Accueil page = new Accueil();
            NavigationService.Navigate(page);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            detailEnquete page = new detailEnquete(((FrameworkElement)sender).DataContext as enquete);
            NavigationService.Navigate(page);
        }
    }
}
