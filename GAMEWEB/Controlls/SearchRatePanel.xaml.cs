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
    /// Interaction logic for SearchRatePanel.xaml
    /// </summary>
    public partial class SearchRatePanel : UserControl, ISearchDataProvider {
        public SearchRatePanel() {
            InitializeComponent();
        }

        public void Hide() {
            Visibility = Visibility.Collapsed;
        }

        public IEnumerable<GameInfo> Result() {
            var value = textBoxValue.Text;
            


            double doubleValue;
            if (Double.TryParse(value, out doubleValue)) {
                if(comboBox.Text == "-")
                    return DatabaseManager.Entities.GameInfo.Where(x => x.SredniaOcena <= doubleValue);
                else
                    return DatabaseManager.Entities.GameInfo.Where(x => x.SredniaOcena >= doubleValue);
            }
            return new List<GameInfo>();
        }

        public void Show() {
            Visibility = Visibility.Visible;
        }
    }
}
