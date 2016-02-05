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
using System.Windows.Shapes;

namespace GAMEWEB {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window {
        public Login() {
            InitializeComponent();
        }


        private void button_Click(object sender, RoutedEventArgs e) {
            if (DatabaseManager.Entities.Uzytkownicy
                .Any(x => x.Nazwa == textBox_login.Text
                && x.Haslo == textBox_password.Password)) {
                User.SetSessionUser(
                    DatabaseManager.Entities.Uzytkownicy
                    .First(x => x.Nazwa == textBox_login.Text && x.Haslo == textBox_password.Password)
                    );

                Close();
            } else {
                Console.WriteLine("Can't login.");
            }
        }
    }
}
