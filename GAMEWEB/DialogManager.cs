using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GAMEWEB {
    public static class DialogManager {

        public static void ShowInfoDialog(string title, string content) {
            ShowDialog(title, content, MessageBoxImage.Information);
        }

        public static void ShowErrorDialog(string title, string content) {
            ShowDialog(title, content, MessageBoxImage.Error);
        }

        public static bool ShowAreYouSureDialog(string title, string content) {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult rsltMessageBox = MessageBox.Show(content, title, button, icon);
            if (rsltMessageBox == MessageBoxResult.Yes) {
                return true;
            }
            return false;
        }

        private static void ShowDialog(string title, string content, MessageBoxImage icon) {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBox.Show(content, title, button, icon);
        }

    }
}
