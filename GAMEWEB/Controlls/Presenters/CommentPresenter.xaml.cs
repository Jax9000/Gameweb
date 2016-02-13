using GAMEWEB.Model;
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

namespace GAMEWEB.Controlls.Presenters {
    /// <summary>
    /// Interaction logic for CommentPresenter.xaml
    /// </summary>
    public partial class CommentPresenter : UserControl {

        public CommentPresenter(Komentarze comment) {
            InitializeComponent();

            viewModel = new EntryViewModel(comment.Wpisy);
            DataContext = viewModel;



            labelUsername.Content = comment.Wpisy.Uzytkownicy.Nazwa;
            textComment.Text = comment.Tresc;
        }
        
        // chcicalem uzyc interfejsu do tego ale trzeba by edytowac klasy od entity :c
        public CommentPresenter(Recenzje review)
        {
            InitializeComponent();

            viewModel = new EntryViewModel(review.Wpisy);
            DataContext = viewModel;



            labelUsername.Content = review.Wpisy.Uzytkownicy.Nazwa;
            textComment.Text = review.DlugaTresc;
        }
            EntryViewModel viewModel;
    }
}
