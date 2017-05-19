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
    /// Logique d'interaction pour ajouterSE.xaml
    /// </summary>
    public partial class ajouterSE : Page
    {
        Controller ct;
        questionnaire newSequence;
        questionnaire sequence;
        enquete enq;
        public static int nbrSeq;
        public static int seqActuelle;
        bool sequenceExistante = false;

        public ajouterSE(enquete e, int nb, int i)
        {
            InitializeComponent();
            erreur.Visibility = Visibility.Hidden;
            ct = new Controller();
            enq = e;
            nbrSeq = nb;
            seqActuelle = i;
            soustitre.Content += i.ToString();
        }

        public ajouterSE(int id, enquete e, int nb, int i)
        {
            InitializeComponent();
            ct = new Controller();
            sequence = ct.getQuestionnaireById(id);
            erreur.Visibility = Visibility.Hidden;
            titreChoisi.Text = sequence.Titre;
            inputDescription.Text = sequence.Description;
            inputNbQ.Text = sequence.questions.Count.ToString();
            if (sequence.Disponible)
            {
                disponibilite.IsChecked = true;
            }
            sequenceExistante = true;
            enq = e;
            nbrSeq = nb;
            seqActuelle = i;
            soustitre.Content += i.ToString();
        }


        private void questions_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                if (sequenceExistante)
                {
                    newSequence = ct.UpdateSequence(sequence.Id, titreChoisi.Text, inputDescription.Text, enq.theme.nom, int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value, enq);
                }
                else
                {
                    newSequence = ct.AddSequence(titreChoisi.Text, inputDescription.Text, enq.theme.nom, int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value, enq);
                }

                ajouterQS page = new ajouterQS(newSequence, int.Parse(inputNbQ.Text), 1);
                NavigationService.Navigate(page);
            }
            catch (System.FormatException)
            {
                erreur.Visibility = Visibility.Visible;
            }
        }
    }
}
