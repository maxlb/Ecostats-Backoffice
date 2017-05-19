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
    /// Logique d'interaction pour ajouterSondage.xaml
    /// </summary>
    public partial class ajouterSondage : Page
    {
        bool nvTheme;
        Controller ct;
        sondage newSondage;
        questionnaire questionnaire;
        bool sondageExistant = false;

        public ajouterSondage()
        {
            InitializeComponent();
            themesParents.Visibility = Visibility.Hidden;
            themeParent.Visibility = Visibility.Hidden;
            erreur.Visibility = Visibility.Hidden;
            ct = new Controller();
            nvTheme = false;

            themes.ItemsSource = ct.getAllNomsThemes();
            themesParents.ItemsSource = ct.getAllNomsThemes();
        }

        public ajouterSondage(int q)
        {
            InitializeComponent();
            ct = new Controller();
            questionnaire = ct.getQuestionnaireById(q);

            themesParents.Visibility = Visibility.Visible;
            themeParent.Visibility = Visibility.Visible;
            erreur.Visibility = Visibility.Hidden;
            
            nvTheme = true;
            
            themes.ItemsSource = ct.getAllNomsThemes();
            themesParents.ItemsSource = ct.getAllNomsThemes();

            titreChoisi.Text = questionnaire.Titre;
            inputDescription.Text = questionnaire.Description;
            themes.Text = questionnaire.theme.nom;
            themesParents.Text = questionnaire.theme.theme2.nom;
            inputNbQ.Text = questionnaire.questions.Count.ToString();
            if (questionnaire.Disponible)
            {
                disponibilite.IsChecked = true;
            }
            sondageExistant = true;
        }

        private void themes_LostMouseCapture(object sender, MouseEventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex == -1)
            {
                themesParents.Visibility = Visibility.Visible;
                themeParent.Visibility = Visibility.Visible;
                nvTheme = true;
            }
        }

        private void questions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (nvTheme)
                {
                    if(themesParents.SelectedIndex == -1)
                    {
                        theme newTheme = ct.AddThemeAlone(themes.Text);
                        if (sondageExistant)
                        {
                            newSondage = ct.UpdateSondage(questionnaire.Id, titreChoisi.Text, inputDescription.Text, newTheme.nom, int.Parse(inputRemuneration.Text), int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value);
                        }
                        else
                        {
                            newSondage = ct.AddSondage(titreChoisi.Text, inputDescription.Text, newTheme.nom, int.Parse(inputRemuneration.Text), int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value);
                        }
                    }
                    else
                    {
                        if (sondageExistant)
                        {
                            newSondage = ct.UpdateSondageWithThemeWithThemeParent(questionnaire.Id, titreChoisi.Text, inputDescription.Text, themes.Text, themesParents.Text, int.Parse(inputRemuneration.Text), int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value);
                        }
                        else
                        {
                            newSondage = ct.AddSondageWithThemeWithThemeParent(titreChoisi.Text, inputDescription.Text, themes.Text, themesParents.Text, int.Parse(inputRemuneration.Text), int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value);
                        }
                    }
                }
                else
                {
                    newSondage = ct.AddSondage(titreChoisi.Text, inputDescription.Text, themes.SelectedItem.ToString(), int.Parse(inputRemuneration.Text), int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value);
                }

                ajouterQS page = new ajouterQS(newSondage, int.Parse(inputNbQ.Text), 1);
                NavigationService.Navigate(page);
            }
            catch (System.FormatException)
            {
                erreur.Visibility = Visibility.Visible;
            }            
        }
    }

    
}
