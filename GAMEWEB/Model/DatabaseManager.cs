using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMEWEB.Model {
    public static class DatabaseManager {

        public static GAMEWEBEntities3 Entities {
            get { return entities; }
        }

        public static DbSet<Uzytkownicy> Users {
            get { return entities.Uzytkownicy; }
        }


        static GAMEWEBEntities3 entities = new GAMEWEBEntities3();


    }
}
