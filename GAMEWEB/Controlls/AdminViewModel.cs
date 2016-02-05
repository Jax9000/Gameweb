using GAMEWEB.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMEWEB.Controlls {
    public class AdminViewModel : BaseViewModel {
        public ObservableCollection<Uzytkownicy> Users { get; private set; }

        public AdminViewModel() {
            Users = new ObservableCollection<Uzytkownicy>(
                DatabaseManager.Entities.Uzytkownicy.AsEnumerable());
            OnPropertyChanged(() => Users);
        }
    }
}
