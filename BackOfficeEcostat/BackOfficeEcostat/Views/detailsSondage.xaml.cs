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
    /// Logique d'interaction pour detailsSondage.xaml
    /// </summary>
    public partial class detailsSondage : Page
    {
        Controller ct;
        int qu;

        public detailsSondage(questionnaire q)
        {
            InitializeComponent();
            ct = new Controller();
            qu = q.Id;
            soustitre.Content += q.Titre;
            titreSon.Content += q.Titre;
            descriptionSon.Content += q.Description;

            if (q.Disponible)
            {
                disponible.Content = "Ce sondage est actuellement disponible en ligne.";
                disponible.Foreground = Brushes.Green;
                disponibilite.Content = "Rendre le sondage immédiatement indisponible";
            }
            else
            {
                disponible.Content = "Ce sondage est actuellement indisponible en ligne.";
                disponible.Foreground = Brushes.Red;
                disponibilite.Content = "Rendre le sondage immédiatement disponible";
            }

            List<question> QuestionOfSondage = ct.getAllQuestionOfQuestionnaire(q.Id);
            foreach (question quest in QuestionOfSondage)
            {
                StackPanel stack = new StackPanel() { Orientation = Orientation.Vertical, Margin = new Thickness(20, 0, 0, 0) };

                TextBlock question = new TextBlock() { Style = (Style)Application.Current.Resources["Questions"], Text = quest.contenu };
                stack.Children.Add(question);

                List<choix> ChoixOfQuestion = ct.getAllChoixOfQuestion(quest.Id);
                foreach (choix ch in ChoixOfQuestion)
                {
                    TextBlock choix = new TextBlock() { Style = (Style)Application.Current.Resources["Reponses"], Text = "    - " + ch.contenu };
                    stack.Children.Add(choix);
                }

                listeQuestionsReponses.Children.Add(stack);
            }
            
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            modifSondage page = new modifSondage();
            NavigationService.Navigate(page);
        }

        private void modifier_Click(object sender, RoutedEventArgs e)
        {
            ajouterSondage page = new ajouterSondage(qu);
            NavigationService.Navigate(page);
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            if (ct.getQuestionnaireById(qu).Disponible)
            {
                ct.UpdateDiponibiliteQuestionnaire(qu, false);
                disponible.Content = "Ce sondage est actuellement indisponible en ligne.";
                disponible.Foreground = Brushes.Red;
                (sender as CheckBox).IsChecked = false;
                (sender as CheckBox).Content = "Rendre le sondage immédiatement disponible";
            }
            else
            {
                ct.UpdateDiponibiliteQuestionnaire(qu, true);
                disponible.Content = "Ce sondage est actuellement disponible en ligne.";
                disponible.Foreground = Brushes.Green;
                (sender as CheckBox).IsChecked = false;
                (sender as CheckBox).Content = "Rendre le sondage immédiatement indisponible";
            }
        }
    }
}
