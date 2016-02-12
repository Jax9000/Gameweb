using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GAMEWEB.Model;

namespace GAMEWEB.Controlls {
    /// <summary>
    /// Interaction logic for SearchGenrePanel.xaml
    /// </summary>
    public partial class SearchGenrePanel : UserControl, ISearchDataProvider {
        public SearchGenrePanel() {
            InitializeComponent();
        }

        public void Hide() {
            Visibility = Visibility.Collapsed;
        }

        public IEnumerable<GameInfo> Result() {
            var genre = DatabaseManager.Entities.Gatunki
                .First(x => x.Nazwa == comboBox.Text);
            var titles = genre.Gry.Select(x => x.Tytul);
            return DatabaseManager.Entities.GameInfo.Where(x => titles.Contains(x.Tytul));
        }

        public void Show() {
            Visibility = Visibility.Visible;
            var genres = new List<string>();
            genres.AddRange(DatabaseManager.Entities.Gatunki.Select(x => x.Nazwa).AsEnumerable());
            comboBox.ItemsSource = genres;
        }
    }
}
