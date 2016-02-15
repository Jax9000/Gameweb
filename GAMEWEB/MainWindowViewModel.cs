using GAMEWEB.Controlls.Presenters;
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
            AddTab(new Controlls.Search(), "Wyszukiwanie");
            if(User.CanDeleteAllPosts)
                AddTab(new Controlls.AdminTab(), "Administracja");
            AddTab(new Controlls.UserTab(), "Panel użytkownika");
            OnPropertyChanged(() => TabCollection);
        }

        /// <summary>
        /// Add Tab.
        /// </summary>
        /// <param name="controll">User control.</param>
        /// <param name="title">Tab name.</param>
        public void AddTab(UserControl control, string title) {
            if (TabCollection.Any(x => x.Header as string == title))
                return;
            var newTab = new TabItem();
            newTab.Content = control;
            newTab.Header = title;
            TabCollection.Add(newTab);
        }

        /// <summary>
        /// Add Tab with close button.
        /// </summary>
        /// <param name="control">User control.</param>
        /// <param name="title">Tab name.</param>
        public void AddClosableTab(UserControl control, string title) {
            foreach(var existingTab in TabCollection) {
                var header = existingTab.Header as ClosableTabHeader;
                if (header != null)
                    if (header.labelName.Content as string == title)
                        return;
            }
            var newTab = new TabItem();
            newTab.Content = control;
            newTab.Header = new ClosableTabHeader(title, control);
            TabCollection.Add(newTab);
        }

        /// <summary>
        /// Close tab with specific UserControl.
        /// </summary>
        /// <param name="tab">Tab's user control.</param>
        public void RemoveTab(UserControl tab) {
            TabCollection.Remove(TabCollection.First(x => x.Content == tab));
        }

        TabControl tabControl;
    }
}
