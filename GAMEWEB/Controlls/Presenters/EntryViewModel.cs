using GAMEWEB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GAMEWEB.Controlls.Presenters {
    public class EntryViewModel : BaseViewModel {
        public ICommand VotePlusCommand { get; private set; }
        public ICommand VoteMinusCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        
        public int BinaryRate { get; private set; }
        public bool CanVote {
            get { return !entry.OcenyWpisow.Any(x => x.UzytkownikID == User.ID); }
        }
        public bool CanDelete {
            get {
                if (entry.UzytkownikID == User.ID && User.CanDeleteSelfPosts)
                    return true;
                else if (User.CanDeleteAllPosts)
                    return true;
                else
                    return false;
            }
        }

        public bool IsOk {
            get;
            private set;
        }

        public EntryViewModel(Wpisy entry) {
            this.entry = entry;
            IsOk = true;

            VotePlusCommand = new DelegateCommand(_ => UpdateRate(true));
            VoteMinusCommand = new DelegateCommand(_ => UpdateRate(false));
            DeleteCommand = new DelegateCommand(_ => Delete());
            Referesh();
        }

        void UpdateRate(bool rate) {
            if (!CanVote)
                return;

            entry.OcenyWpisow.Add(
                new OcenyWpisow() {
                    BinarnaOcena = rate,
                    UzytkownikID = User.ID,
                    WpisID = entry.WpisID
                });
            DatabaseManager.Entities.SaveChanges();

            OnPropertyChanged(() => CanVote);
            Referesh();
        }

        void Referesh() {
            BinaryRate = entry.OcenyWpisow.Sum(x => x.BinarnaOcena ? 1 : -1);
            OnPropertyChanged(() => BinaryRate);
        }

        void Delete() {
            DatabaseManager.Entities.Wpisy.Remove(entry);
            DatabaseManager.Save();
            IsOk = false;
            OnPropertyChanged(() => IsOk);
        }

        Wpisy entry;
    }
}
