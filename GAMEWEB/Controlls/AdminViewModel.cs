using GAMEWEB.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GAMEWEB.Controlls {
    public enum Permissions { Admin, User }

    public class AdminViewModel : BaseViewModel {

        public ObservableCollection<Uzytkownicy> Users { get; private set; }
        public ObservableCollection<string> PermissionsTypes {
            get { return new ObservableCollection<string>(Enum.GetNames(typeof(Permissions))); }
        }

        public Uzytkownicy SelectedUser {
            get { return selectedUser; }
            set {
                selectedUser = value;
                if (value != null) {
                    Console.WriteLine(value.Nazwa);
                    userToDelete = value.Nazwa;
                    OnPropertyChanged(() => UserToDeleteName);
                }
            }
        }
        public string UserToDeleteName {
            get { return userToDelete; }
            set { userToDelete = value; }
        }

        public string NewUserName { get; set; }
        public string NewUserPassword { get; set; }
        public string NewUserEmail { get; set; }
        public ComboBoxItem NewUserSex { get; set; }
        public DateTime? NewUserBirthdate { get; set; }
        public string NewUserPermission { get; set; }



        public ICommand DeleteUserCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }

        public AdminViewModel() {
            UpdateUsers();
            OnPropertyChanged(() => PermissionsTypes);
            DeleteUserCommand = new DelegateCommand(DeleteUser);
            AddUserCommand = new DelegateCommand(AddUser);
        }

        private void UpdateUsers() {
            Users = new ObservableCollection<Uzytkownicy>(
                DatabaseManager.Entities.Uzytkownicy.AsEnumerable());
            OnPropertyChanged(() => Users);
        }


        private void DeleteUser(object obj) {
            try {
                var user = DatabaseManager.Users.First(x => x.Nazwa == UserToDeleteName);
                if (!DialogManager.ShowAreYouSureDialog("Usuń użytkownika",
                "Czy na pewno chcesz usunąć użytkownika: " + UserToDeleteName + "?"))
                    return;

                DatabaseManager.RemoveUser(user.UzytkownikID);
                DatabaseManager.Save();

                UpdateUsers();
            } catch (Exception) {
                DialogManager.ShowErrorDialog("Błąd usuwania", "Użytkownik o podanej nazwie nie istnieje.");
                return;
            }   
        }

        private void AddUser(object obj) {

            try {
                var user = new Uzytkownicy() {
                    Nazwa = NewUserName,
                    Plec = NewUserSex.Content.ToString(),
                    BlokadaKonta = false,
                    Haslo = NewUserPassword,
                    Email = NewUserEmail,
                    DataUrodzenia = NewUserBirthdate,
                };
                DatabaseManager.Users.Add(user);
                DatabaseManager.Save();

                UpdateUsers();
            } catch (Exception) {
                DialogManager.ShowErrorDialog("Błędne dane", "Sprawdź wpisane dane użytkownika");
            }
            
        }

        private string userToDelete;
        private Uzytkownicy selectedUser;
    }
}
