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
    /// Interaction logic for SmallGamePresenter.xaml
    /// </summary>
    public partial class SmallGamePresenter : UserControl {

        public SmallGamePresenter(Model.Gry game) {
            InitializeComponent();
            this.game = game;
            labelName.Content = game.Tytul;
            labelRank.Content = Model.DatabaseManager.Entities.RankingGier
                .First(x => x.GraID == game.GraID).SredniaOcena;
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            App.MainWindowManager.AddTab(new GameTab(game), game.Tytul);
        }

        Model.Gry game;
    }
}
