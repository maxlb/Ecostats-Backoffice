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
    /// Logique d'interaction pour ajouterQS.xaml
    /// </summary>
    public partial class ajouterQS : Page
    {
        Controller ct;
        questionnaire q;
        sondage son;
        int nbQ;
        List<StackPanel> ch = new List<StackPanel>();
        int j = 2;
        int k;
        int idQ = 2000;

        public ajouterQS(sondage s, int nb, int i)
        {
            InitializeComponent();
            ct = new Controller();
            son = s;
            nbQ = nb;
            k = i;
            erreur.Visibility = Visibility.Hidden;
            q = ct.getQuestionnaireBySondage(s);
            soustitre.Content = "Ajout des questions : question " + i;
            if (i == nb)
            {
                valider.Visibility = Visibility.Visible;
                questions.Visibility = Visibility.Hidden;
            }
            else
            {
                questions.Visibility = Visibility.Visible;
                valider.Visibility = Visibility.Hidden;
            }
            ch.Add(choix1);
            ch.Add(choix2);
            try
            {
                List<question> AllQuestions = ct.getAllQuestionOfQuestionnaire(q.Id);
                inputQ.Text = AllQuestions.ElementAt(i - 1).contenu;
                idQ = AllQuestions.ElementAt(i - 1).Id;
                List<choix> AllChoix = ct.getAllChoixOfQuestion(idQ);
                int a = 0;
                foreach (choix choix in AllChoix)
                {
                    if(a > 1)
                    {
                        j++;
                        StackPanel sp = new StackPanel() { Name = "choix" + j, Orientation = Orientation.Horizontal, Margin = new Thickness(38, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                        Label l = new Label() { Margin = new Thickness(5), Content = j + " :" };
                        TextBox tb = new TextBox() { Width = 348, Height = 34, Margin = new Thickness(5) };
                        sp.Children.Add(l);
                        sp.Children.Add(tb);
                        PanelQuestion.Children.Add(sp);
                        ch.Add(sp);
                    }
                    (ch.ElementAt(a).Children[1] as TextBox).Text = choix.contenu;
                    a++;
                }
            }
            catch (Exception) {  }

        }

        private void questions_Click(object sender, RoutedEventArgs e)
        {
            if(idQ != 2000)
            {
                question Updatedquestion = ct.UpdateQuestionOfSondage(idQ, inputQ.Text);
                int b = 0;
                foreach (var choix in ch)
                {
                    b++;
                    string unChoix = (choix.Children[1] as TextBox).Text;
                    if( b <= Updatedquestion.choixes.Count)
                    {
                        ct.UpdateChoix(Updatedquestion.choixes.ElementAt(b - 1).Id, unChoix);
                    }
                    else
                    {
                        ct.AddChoix(Updatedquestion, unChoix);
                    }
                }
            }
            else
            {
                question Newquestion = ct.AddQuestionOfSondage(q, inputQ.Text);
                foreach (var choix in ch)
                {
                    string unChoix = (choix.Children[1] as TextBox).Text;
                    ct.AddChoix(Newquestion, unChoix);
                }
            }
            ajouterQS page = new ajouterQS(son, nbQ, k+1);
            NavigationService.Navigate(page);
        }

        private void choixSup_Click(object sender, RoutedEventArgs e)
        {
            j++;
            StackPanel sp = new StackPanel() { Name = "choix" + j, Orientation = Orientation.Horizontal, Margin = new Thickness(38, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            Label l = new Label() { Margin = new Thickness(5), Content = j + " :" };
            TextBox tb = new TextBox() { Width = 348, Height = 34, Margin = new Thickness(5) };

            sp.Children.Add(l);
            sp.Children.Add(tb);

            PanelQuestion.Children.Add(sp);
            ch.Add(sp);
        }

        private void valider_Click(object sender, RoutedEventArgs e)
        {
            if (idQ != 2000)
            {
                question Updatedquestion = ct.UpdateQuestionOfSondage(idQ, inputQ.Text);
                int b = 0;
                foreach (var choix in ch)
                {
                    b++;
                    string unChoix = (choix.Children[1] as TextBox).Text;
                    if (b <= Updatedquestion.choixes.Count)
                    {
                        ct.UpdateChoix(Updatedquestion.choixes.ElementAt(b - 1).Id, unChoix);
                    }
                    else
                    {
                        ct.AddChoix(Updatedquestion, unChoix);
                    }
                }
            }
            else
            {
                question Newquestion = ct.AddQuestionOfSondage(q, inputQ.Text);
                foreach (var choix in ch)
                {
                    string unChoix = (choix.Children[1] as TextBox).Text;
                    ct.AddChoix(Newquestion, unChoix);
                }
            }

            Accueil page = new Accueil();
            NavigationService.Navigate(page);
        }
    }
}
