using GAMEWEB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GAMEWEB.Controlls {
    class UserViewModel : BaseViewModel {
        public string UserName {
            get { return userName; }
            set {
                userName = value;
                IsDirty = true;
                OnPropertyChanged(() => IsDirty);
            }
        }
        public string UserPassword {
            get { return userPassword; }
            set {
                userPassword = value;
                IsDirty = true;
                OnPropertyChanged(() => IsDirty);
            }
        }
        public string UserEmail {
            get { return userEmail; }
            set {
                userEmail = value;
                IsDirty = true;
                OnPropertyChanged(() => IsDirty);
            }
        }
        public DateTime? UserDate {
            get { return userDate; }
            set {
                userDate = value;
                IsDirty = true;
                OnPropertyChanged(() => IsDirty);
            }
        }
        public string UserDescription {
            get { return userDescription; }
            set {
                userDescription = value;
                IsDirty = true;
                OnPropertyChanged(() => IsDirty);
            }
        }
        public bool IsDirty { get; set; }

        public ICommand SaveCommand { get; private set; }

        public UserViewModel() {
            currentUser = DatabaseManager.Users.First(x => x.UzytkownikID == User.ID);
            UpdateFromDatabase();
            SaveCommand = new DelegateCommand(Save);
        }

        private void Save(object obj) {
            try {
                Action<string, string> okString =
                        (str, msg) => {
                            if (str == null || str == "")
                                throw new EValidData(msg);
                        };


                okString(userName, "Niepoprawna nazwa użytkownika.");
                okString(userPassword, "Niepoprawne hasło.");
                okString(userEmail, "Niepoprawny emial.");
                if (!userEmail.Contains("@") || !userEmail.Contains("."))
                    throw new EValidData("Niepoprawna forma email (brak @ / .).");

                if (DatabaseManager.Users.Where(x => x.Nazwa != currentUser.Nazwa).Any(x => x.Nazwa == userName))
                    throw new EValidData("Użytkownik o podanej nazwie już istnieje.");

                if (!DialogManager.ShowAreYouSureDialog("Aktualizacja", "Czy jesteś pewny, że chcesz zmienić dane?")) {
                    UpdateFromDatabase();
                    return;
                }


                currentUser.Nazwa = userName;
                currentUser.Haslo = userPassword;
                currentUser.Email = userEmail;
                currentUser.Opis = userDescription;

                DatabaseManager.Save();
                DialogManager.ShowInfoDialog("Zapis danych", "Dane zostały pomyślnie zapisane do bazy.");
                UpdateFromDatabase();
            } catch (EValidData e) {
                DialogManager.ShowErrorDialog("Błędne dane", e.Message);
            }
        }

        private void UpdateFromDatabase() {
            UserName = currentUser.Nazwa;
            OnPropertyChanged(() => UserName);

            UserPassword = currentUser.Haslo;
            OnPropertyChanged(() => UserPassword);

            UserEmail = currentUser.Email;
            OnPropertyChanged(() => UserEmail);

            UserDate = currentUser.DataUrodzenia;
            OnPropertyChanged(() => UserDate);

            UserDescription = currentUser.Opis;
            OnPropertyChanged(() => UserDescription);

            IsDirty = false;
            OnPropertyChanged(() => IsDirty);
            Console.WriteLine("Data updated.");
        }

        private Uzytkownicy currentUser;
        private string userName;
        private string userPassword;
        private string userEmail;
        private DateTime? userDate;
        private string userDescription;
    }
}
