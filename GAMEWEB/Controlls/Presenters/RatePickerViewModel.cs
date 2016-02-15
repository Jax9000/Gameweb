using GAMEWEB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GAMEWEB.Controlls.Presenters {
    public class RatePickerViewModel : BaseViewModel {
        public bool? R1 {
            get { return r1; }
            set {
                r1 = value;
                if (value == false) {
                    R2 = false;
                    OnPropertyChanged(() => R2);
                    OnPropertyChanged(() => LabelValue);
                }
            }
        }
        public bool? R2 {
            get { return r2; }
            set {
                r2 = value;
                if (value ?? false) {
                    R1 = true;
                    OnPropertyChanged(() => R1);
                    OnPropertyChanged(() => LabelValue);
                } else {
                    R3 = false;
                    OnPropertyChanged(() => R3);
                    OnPropertyChanged(() => LabelValue);
                }
            }
        }
        public bool? R3 {
            get { return r3; }
            set {
                r3 = value;
                if (value ?? false) {
                    R2 = true;
                    OnPropertyChanged(() => R2);
                    OnPropertyChanged(() => LabelValue);
                } else {
                    R4 = false;
                    OnPropertyChanged(() => R4);
                    OnPropertyChanged(() => LabelValue);
                }
            }
        }
        public bool? R4 {
            get { return r4; }
            set {
                r4 = value;
                if (value ?? false) {
                    R3 = true;
                    OnPropertyChanged(() => R3);
                    OnPropertyChanged(() => LabelValue);
                } else {
                    R5 = false;
                    OnPropertyChanged(() => R5);
                    OnPropertyChanged(() => LabelValue);
                }
            }
        }
        public bool? R5 {
            get { return r5; }
            set {
                r5 = value;
                if (value ?? false) {
                    R4 = true;
                    OnPropertyChanged(() => R4);
                    OnPropertyChanged(() => LabelValue);
                }
            }
        }

        public string LabelValue {
            get { return rValue + "/5"; }
        }

        public bool AlreadyVoted {
            get { return !DatabaseManager.Entities.OcenyGier
                    .Any(x => x.GraID == Game.GraID && x.UzytkownikID == User.ID); }
        }

        public Gry Game;

        int rValue {
            get {
                if (R5 ?? false)
                    return 5;
                if (R4 ?? false)
                    return 4;
                if (R3 ?? false)
                    return 3;
                if (R2 ?? false)
                    return 2;
                if (R1 ?? false)
                    return 1;
                return 0;
            }
        }

        public ICommand RateCommand { get; private set; }

        public RatePickerViewModel() {
            R1 = false;
            R2 = false;
            R3 = false;
            R4 = false;
            R5 = false;
            OnPropertyChanged(() => LabelValue);
            RateCommand = new DelegateCommand(_ => Rate());
        }

        private void Rate() {
            DatabaseManager.Entities.OcenyGier
                .Add(new OcenyGier() {
                    GraID = Game.GraID,
                    UzytkownikID = User.ID,
                    OcenaOgolna = rValue
                });
            DatabaseManager.Save();
            OnPropertyChanged(() => AlreadyVoted);
        }

        private bool? r1;
        private bool? r2;
        private bool? r3;
        private bool? r4;
        private bool? r5;
    }
}
