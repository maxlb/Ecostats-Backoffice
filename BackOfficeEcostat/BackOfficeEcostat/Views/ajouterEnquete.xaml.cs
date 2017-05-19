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
    /// Logique d'interaction pour ajouterEnquete.xaml
    /// </summary>
    public partial class ajouterEnquete : Page
    {
        bool nvTheme;
        Controller ct;
        enquete newEnquete;
        enquete enquete;
        bool enqueteExistante = false;

        public ajouterEnquete()
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

        public ajouterEnquete(int e)
        {
            InitializeComponent();
            ct = new Controller();
            enquete = ct.getEnqueteById(e);

            themesParents.Visibility = Visibility.Visible;
            themeParent.Visibility = Visibility.Visible;
            erreur.Visibility = Visibility.Hidden;

            nvTheme = true;

            themes.ItemsSource = ct.getAllNomsThemes();
            themesParents.ItemsSource = ct.getAllNomsThemes();

            titreChoisi.Text = newEnquete.titre;
            inputDescription.Text = newEnquete.description;
            themes.Text = newEnquete.theme.nom;
            themesParents.Text = newEnquete.theme.theme2.nom;
            inputNbQ.Text = newEnquete.questionnaires.Count.ToString();
            if (newEnquete.disponible)
            {
                disponibilite.IsChecked = true;
            }
            enqueteExistante = true;
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
                    if (themesParents.SelectedIndex == -1)
                    {
                        theme newTheme = ct.AddThemeAlone(themes.Text);
                        if (enqueteExistante)
                        {
                            newEnquete = ct.UpdateEnquete(enquete.Id, titreChoisi.Text, inputDescription.Text, newTheme.nom, int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value);
                        }
                        else
                        {
                            newEnquete = ct.AddEnquete(titreChoisi.Text, inputDescription.Text, newTheme.nom, int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value);
                        }
                    }
                    else
                    {
                        if (enqueteExistante)
                        {
                            newEnquete = ct.UpdateEnqueteWithThemeWithThemeParent(enquete.Id, titreChoisi.Text, inputDescription.Text, themes.Text, themesParents.Text, int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value);
                        }
                        else
                        {
                            newEnquete = ct.AddEnqueteWithThemeWithThemeParent(titreChoisi.Text, inputDescription.Text, themes.Text, themesParents.Text, int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value);
                        }
                    }
                }
                else
                {
                    newEnquete = ct.AddEnquete(titreChoisi.Text, inputDescription.Text, themes.SelectedItem.ToString(), int.Parse(inputNbQ.Text), disponibilite.IsChecked.Value);
                }

                ajouterSE page = new ajouterSE(newEnquete, int.Parse(inputNbQ.Text), 1);
                NavigationService.Navigate(page);
            }
            catch (System.FormatException)
            {
                erreur.Visibility = Visibility.Visible;
            }
        }
    }
}
