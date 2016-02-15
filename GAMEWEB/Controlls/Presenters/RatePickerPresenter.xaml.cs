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
    /// Interaction logic for RatePickerPresenter.xaml
    /// </summary>
    public partial class RatePickerPresenter : UserControl {

        public Gry Game {
            set { viewModel.Game = value; }
        }

        public RatePickerPresenter() {
            InitializeComponent();
            viewModel = new RatePickerViewModel();
            DataContext = viewModel;
        }

        private RatePickerViewModel viewModel;
    }
}
