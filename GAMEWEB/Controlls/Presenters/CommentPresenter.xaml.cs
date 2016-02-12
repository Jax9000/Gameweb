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
        /// <summary>
        /// It's designed to show komentarze and recenzje
        /// </summary>
        /// <param name="wpis"></param>
        public CommentPresenter(IWpis wpis) {
            InitializeComponent();

            viewModel = new EntryViewModel(wpis.Wpisy);
            DataContext = viewModel;



            labelUsername.Content = wpis.Wpisy.Uzytkownicy.Nazwa;
            textComment.Text = wpis.Tresc;
        }

        EntryViewModel viewModel;
    }
}
