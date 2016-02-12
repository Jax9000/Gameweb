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
using System.Windows.Shapes;
using GAMEWEB.Model;

namespace GAMEWEB.Controlls
{
    /// <summary>
    /// Interaction logic for NewReviewWindow.xaml
    /// </summary>
    public partial class NewReviewWindow : Window
    {
        public NewReviewWindow(Gry game)
        {
            InitializeComponent();
            Title += "(" + game.Tytul + ")";
            this.game = game;
        }

        Gry game;
        bool isSavedInDBS = false;

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            string myText = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            var newEntry = new Wpisy()
            {
                GraID = game.GraID,
                DataDodania = DateTime.Now,
                UzytkownikID = User.ID,
                Tytul = "Recenzja"
            };
            DatabaseManager.Entities.Wpisy.Add(newEntry);
            DatabaseManager.Entities.SaveChanges();

            var newReview = new Recenzje()
            {
                RecenzjaID = newEntry.WpisID,
                DlugaTresc = myText
            };
            DatabaseManager.Entities.Recenzje.Add(newReview);
            DatabaseManager.Entities.SaveChanges();

            isSavedInDBS = true;
            ShowConfirmDialog();
            this.Close();
        }

        private void ShowConfirmDialog()
        {
            string messageBoxText = "Recenzja została wprowadzona pomyślnie";
            string caption = "Zatwierdzenie";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        private bool IsSureForExitDialog()
        {
            string messageBoxText = "Niezatwierdzone zmiany zostaną utracone, kontynuować?";
            string caption = "Zmiany";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult rsltMessageBox = MessageBox.Show(messageBoxText, caption, button, icon);
           if(rsltMessageBox == MessageBoxResult.Yes)
            {
                return true;
            }
           else
            {
                return false;
            }
            return false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string myText = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            if(myText.Length > 10 && !isSavedInDBS)
            {
                e.Cancel = !(IsSureForExitDialog());
            }
        }
    }
}
