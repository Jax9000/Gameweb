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
using GAMEWEB.Model;

namespace GAMEWEB.Controlls {
    /// <summary>
    /// Interaction logic for AdminTab.xaml
    /// </summary>
    public partial class AdminTab : UserControl {
        public AdminTab() {
            InitializeComponent();
            viewModel = new AdminViewModel();
            DataContext = viewModel;

            var genres = new List<string>();
            genres.AddRange(DatabaseManager.Entities.Gatunki.Select(x => x.Nazwa).AsEnumerable());
            genreComboBox.ItemsSource = genres;
        }

        AdminViewModel viewModel;
    }
}
