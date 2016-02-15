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
    /// Interaction logic for SearchStudioPanel.xaml
    /// </summary>
    public partial class SearchStudioPanel : UserControl, ISearchDataProvider {
        public SearchStudioPanel() {
            InitializeComponent();
        }

        public void Hide() {
            Visibility = Visibility.Collapsed;
        }

        public IEnumerable<GameInfo> Result() {
            var studio = DatabaseManager.Entities.Studia
                .First(x => x.Nazwa == comboBox.Text);
            var titles = studio.Gry.Select(x => x.Tytul);
            return DatabaseManager.Entities.GameInfo.Where(x => titles.Contains(x.Tytul));
        }

        public void Show() {
            Visibility = Visibility.Visible;
            var studies = new List<string>();
            studies.AddRange(DatabaseManager.Entities.Studia.Select(x => x.Nazwa).AsEnumerable());
            comboBox.ItemsSource = studies;
            comboBox.SelectedIndex = 0;
        }
    }
}
