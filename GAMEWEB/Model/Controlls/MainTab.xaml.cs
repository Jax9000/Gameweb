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

namespace GAMEWEB.Controlls {
    /// <summary>
    /// Interaction logic for MainTab.xaml
    /// </summary>
    public partial class MainTab : UserControl {
        public MainTab() {
            InitializeComponent();

            foreach(var gameID in DatabaseManager.Entities.RankingGier
                .OrderByDescending(x => x.SredniaOcena).Take(5)
                .Select(x => x.GraID)) {
                var game = DatabaseManager.Entities.Gry.First(x => x.GraID == gameID);
                gamesRank.Children.Add(new Presenters.SmallGamePresenter(game));
            }
        }
    }
}
