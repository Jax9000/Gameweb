using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAMEWEB.Model;

namespace GAMEWEB {

    public static class User {
        public static string Username {
            get;
            private set;
        }
        public static int ID {
            get;
            private set;
        }
        public static bool CanVote {
            get { return privileges.DodawanieOcen ?? false; }
        }
        public static bool CanPost {
            get { return privileges.DodawanieWpisow ?? false; }
        }
        public static bool CanDeleteSelfPosts {
            get { return privileges.UsuwanieWlasnychWpisowOcen ?? false; }
        }
        public static bool CanDeleteAllPosts {
            get { return privileges.UsuwanieWpisowOcen ?? false; }
        }

        public static void SetSessionUser(Uzytkownicy user) {
            User.user = user;
            Username = user.Nazwa;
            ID = user.UzytkownikID;
            privileges = DatabaseManager.Entities.Uprawnienia
                .First(x => x.UprawnienieID == user.UprawnienieID);
        }

        private static Uzytkownicy user;
        private static Uprawnienia privileges;
    }
}
