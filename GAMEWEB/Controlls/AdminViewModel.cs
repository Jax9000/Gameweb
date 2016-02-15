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

        public string NewGameName { get; set; }
        public string NewGameStudioName { get; set; }
        public DateTime? NewGameRelease { get; set; }
        public string GenreSelected { get; set; }
        public string NewGameDescription { get; set; }



        public ICommand DeleteUserCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        public ICommand AddGameCommand { get; private set; }

        public AdminViewModel() {
            UpdateUsers();
            OnPropertyChanged(() => PermissionsTypes);
            DeleteUserCommand = new DelegateCommand(DeleteUser);
            AddUserCommand = new DelegateCommand(AddUser);
            AddGameCommand = new DelegateCommand(AddGame);
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

                Action<string, string> okString =
                    (str, msg) => {
                        if (str == null || str == "")
                            throw new EValidData(msg);
                    };

                
                okString(NewUserName, "Niepoprawna nazwa użytkownika.");
                okString(NewUserPassword, "Niepoprawne hasło.");
                okString(NewUserEmail, "Niepoprawny emial.");
                okString(NewUserPermission, "Podaj pozwolenia.");
                if (!NewUserEmail.Contains("@") || !NewUserEmail.Contains("."))
                    throw new EValidData("Niepoprawna forma email (brak @ / .).");

                if (DatabaseManager.Users.Any(x => x.Nazwa == NewUserName))
                    throw new EValidData("Użytkownik o podanej nazwie już istnieje.");


                var user = new Uzytkownicy() {
                    Nazwa = NewUserName,
                    Plec = NewUserSex.Content.ToString(),
                    BlokadaKonta = false,
                    Haslo = NewUserPassword,
                    Email = NewUserEmail,
                    DataUrodzenia = NewUserBirthdate,
                    UprawnienieID = (NewUserPermission == "Admin") ? 1 : 3
                };

                DatabaseManager.Users.Add(user);
                DatabaseManager.Save();
                UpdateUsers();
            } catch (EValidData e) {
                DialogManager.ShowErrorDialog("Błędne dane", e.Message);
            } catch (Exception) {
                DialogManager.ShowErrorDialog("Błędne dane", "Sprawdź wpisane dane użytkownika");
            }
            
        }

        private void AddGame(object obj)
        {
            try
            {

                Action<string, string> okString =
                    (str, msg) => {
                        if (str == null || str == "")
                            throw new EValidData(msg);
                    };

                Console.WriteLine(NewGameName);
                okString(NewGameName, "Niepoprawna nazwa gry.");
                okString(NewGameStudioName, "Niepoprawna nazwa studio.");

                if (DatabaseManager.Entities.Gry.Any(x=> x.Tytul == NewGameName))
                    throw new EValidData("Gra podanej nazwie już istnieje.");

                if (!(DatabaseManager.Entities.Studia.Any(x => x.Nazwa == NewGameStudioName)))
                {
                    var newStudio  = new Studia();
                    newStudio.Nazwa = NewGameStudioName;
                    DatabaseManager.Entities.Studia.Add(newStudio);
                    DatabaseManager.Save();
                }

                var studio = DatabaseManager.Entities.Studia.First(x => x.Nazwa == NewGameStudioName);

                var game = new Gry()
                {
                    Tytul = NewGameName,
                    StudioID = studio.StudioID,
                    DataWydania = (DateTime)NewGameRelease,
                    Opis = NewGameDescription,
                };

                var gatunek = DatabaseManager.Entities.Gatunki.First(x => x.Nazwa == GenreSelected);
                gatunek.Gry.Add(game);

                DatabaseManager.Entities.Gry.Add(game);
                DatabaseManager.Save();

                DialogManager.ShowInfoDialog("Dodawanie gry", "Gra została dodana pomyślnie!");
            }
            catch (EValidData e)
            {
                DialogManager.ShowErrorDialog("Błędne dane", e.Message);
            }
            catch (Exception)
            {
                DialogManager.ShowErrorDialog("Błędne dane", "Sprawdź wpisane dane");
            }

        }


        private string userToDelete;
        private Uzytkownicy selectedUser;
    }

    public class EValidData : Exception {
        public EValidData(string msg) : base(msg) { }
    }
}
