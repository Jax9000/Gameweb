using GAMEWEB.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GAMEWEB.Controlls {
    public class SearchViewModel : BaseViewModel {
        public ObservableCollection<GameInfo> SearchResult { get; private set; }

        public SearchOption CurrentSearchOption {
            get { return searchOption; }
            set {
                Action<ISearchDataProvider> prepareView =
                    (so) => {
                        HideAllSearchProviders();
                        so.Show();
                        searchDataProvider = so;
                    };
                    

                switch (value) {
                    case SearchOption.NAME:
                        prepareView(parent.searchName);
                        break;
                    case SearchOption.GENRE:
                        prepareView(parent.searchGenre);
                        break;
                    case SearchOption.STUDIO:
                        prepareView(parent.searchStudio);
                        break;
                    case SearchOption.RATE:
                        prepareView(parent.searchRate);
                        break;
                }
                searchOption = value;
            }
        }

        public ICommand SearchCommand { get; private set; }
        public ICommand OpenGameCommand { get; private set; }

        public SearchViewModel(Search parent) {
            SearchCommand = new DelegateCommand(_ => NewSearch());
            OpenGameCommand = new DelegateCommand(obj => OpenGame(obj as string));
            this.parent = parent;
            SearchResult = new ObservableCollection<GameInfo>();
            foreach(var game in DatabaseManager.Entities.GameInfo) {
                SearchResult.Add(game);
            }
            searchDataProviders = new List<ISearchDataProvider>() {
                parent.searchName,
                parent.searchGenre,
                parent.searchStudio,
                parent.searchRate
            };
            searchDataProvider = parent.searchName;
            CurrentSearchOption = SearchOption.NAME;
            OnPropertyChanged(() => CurrentSearchOption);
            OnPropertyChanged(() => SearchResult);
        }

        void NewSearch() {
            SearchResult = new ObservableCollection<GameInfo>(searchDataProvider.Result());
            OnPropertyChanged(() => SearchResult);
        }

        void HideAllSearchProviders() {
            foreach(var provider in searchDataProviders) {
                provider.Hide();
            }
        }

        void OpenGame(string title) {
            App.MainWindowManager.AddClosableTab(
                new GameTab(DatabaseManager.Entities.Gry.First(x => x.Tytul == title)),
                title);
        }

        IEnumerable<ISearchDataProvider> searchDataProviders;
        ISearchDataProvider searchDataProvider;
        SearchOption searchOption;
        Search parent;
    }

    public enum SearchOption { NAME, GENRE, STUDIO, RATE}
}
