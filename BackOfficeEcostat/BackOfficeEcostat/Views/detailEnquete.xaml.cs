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
    /// Logique d'interaction pour detailEnquete.xaml
    /// </summary>
    public partial class detailEnquete : Page
    {
        Controller ct;
        int enq;

        public detailEnquete(enquete e)
        {
            InitializeComponent();
            ct = new Controller();
            enq = e.Id;
            soustitre.Content += e.titre;
            titreEnq.Content += e.titre;
            descriptionEnq.Content += e.description;
            theme.Content += e.theme.nom;

            foreach (questionnaire seq in ct.getAllQuestionnaireOfEnquete(enq))
            {
                sequences.Items.Add(seq);
            }

            // Affichage de la DataGrid
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Titre de la séquence";
            col1.IsReadOnly = true;
            col1.Binding = new Binding("Titre");
            sequences.Columns.Insert(0, col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Description de la séquence";
            col2.IsReadOnly = true;
            col2.Binding = new Binding("Description");
            sequences.Columns.Insert(1, col2);
        }


        private void retour_Click(object sender, RoutedEventArgs e)
        {
            modifEnquete page = new modifEnquete();
            NavigationService.Navigate(page);
        }

        private void modifier_Click(object sender, RoutedEventArgs e)
        {
            ajouterEnquete page = new ajouterEnquete(enq);
            NavigationService.Navigate(page);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            detailsSondage page = new detailsSondage(((FrameworkElement)sender).DataContext as questionnaire);
            NavigationService.Navigate(page);
        }

        private void seqSup_Click(object sender, RoutedEventArgs e)
        {
            questionnaire newSeq = ct.AddSequence("Nouvelle séquence", "Description ...", ct.getEnqueteById(enq).theme.nom, 1, false, ct.getEnqueteById(enq));
            sequences.Items.Add(newSeq);
        }
    }
}
