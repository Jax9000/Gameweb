﻿using GAMEWEB.Controlls.Presenters;
using GAMEWEB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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

            this.game = game;
            labelTitle.Content = game.Tytul;
            string genres = "";
            foreach(var genre in game.Gatunki.Select(x => x.Nazwa)) {
                genres += genre + " ";
            }
            labelGenre.Content = genres;
            labelRate1.Content = game.OcenyGier.Average(x => x.OcenaOgolna).ToString();
            labelRate2.Content = game.OcenyGier.Average(x => x.Grafika).ToString();
            labelRate3.Content = game.OcenyGier.Average(x => x.Fabula).ToString();
            labelDescription.Content = game.Opis;

            RefreshComments();
        }

        void RefreshComments() {
            Comments.Children.Clear();
            foreach (var comment in DatabaseManager.Entities.Komentarze.Where(x => x.Wpisy.Gry.GraID == game.GraID)) {
                Comments.Children.Add(new CommentPresenter(comment));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (textBoxNewComment.Text == "")
                return;

            var newEntry = new Wpisy() {
                GraID = game.GraID,
                DataDodania = DateTime.Now,
                UzytkownikID = User.ID,
                Tytul = "Super komentarz"
            };

            DatabaseManager.Entities.Wpisy.Add(newEntry);
            DatabaseManager.Save();

            var newComments = new Komentarze() {
                KomentarzID = newEntry.WpisID,
                Tresc = textBoxNewComment.Text,
            };
            DatabaseManager.Entities.Komentarze.Add(newComments);
            DatabaseManager.Save();

            textBoxNewComment.Text = "";
            RefreshComments();
        }

        Gry game;
    }
}
