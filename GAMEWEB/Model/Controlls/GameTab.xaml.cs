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
    /// Interaction logic for GameTab.xaml
    /// </summary>
    public partial class GameTab : UserControl {
        public GameTab(Gry game) {
            InitializeComponent();
            DataContext = new ViewModels.GameTabViewModel();

            labelTitle.Content = game.Tytul;
            labelRate1.Content = game.OcenyGier.Average(x => x.OcenaOgolna).ToString();
            labelRate2.Content = game.OcenyGier.Average(x => x.Grafika).ToString();
            labelRate3.Content = game.OcenyGier.Average(x => x.Fabula).ToString();
        }
    }
}
