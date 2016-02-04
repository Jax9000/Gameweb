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
    /// Interaction logic for SearchNamePanel.xaml
    /// </summary>
    public partial class SearchNamePanel : UserControl, ISearchDataProvider {
        public SearchNamePanel() {
            InitializeComponent();
        }

        public IEnumerable<GameInfo> Result() {
            return DatabaseManager.Entities.GameInfo
                .Where(x => x.Tytul.Contains(labelName.Text));
        }

        public void Hide() {
            Visibility = Visibility.Collapsed;
        }

        public void Show() {
            Visibility = Visibility.Visible;
        }

    }
}
