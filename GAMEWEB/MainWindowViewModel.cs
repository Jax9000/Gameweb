using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GAMEWEB {
    public class MainWindowViewModel : BaseViewModel {

        public ObservableCollection<TabItem> TabCollection { get; private set; }

        public MainWindowViewModel(TabControl tabControl) {
            this.tabControl = tabControl;
            TabCollection = new ObservableCollection<TabItem>();
            AddTab(new Controlls.MainTab(), "Strona główna");
            OnPropertyChanged(() => TabCollection);
        }

        public void AddTab(UserControl controll, string title) {
            var newTab = new TabItem();
            newTab.Content = controll;
            newTab.Header = title;
            TabCollection.Add(newTab);
        }

        public void AddClosableTab(UserControl controll, string title) {
            // TODO
        }

        TabControl tabControl;
    }
}
