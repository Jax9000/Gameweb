﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMEWEB.Model {
    public static class DatabaseManager {

        public static GAMEWEBEntities Entities {
            get { return entities; }
        }

        public static DbSet<Uzytkownicy> Users {
            get { return entities.Uzytkownicy; }
        }

        public static void Save() {
            entities.SaveChanges();
        }

        public static void RemoveUser(int id) {
            Entities.remove_user(id.ToString());
        }

        static GAMEWEBEntities entities = new GAMEWEBEntities();
    }
}
