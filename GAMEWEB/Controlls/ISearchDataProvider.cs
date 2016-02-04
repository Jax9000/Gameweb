using GAMEWEB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMEWEB.Controlls {
    interface ISearchDataProvider {
        IEnumerable<GameInfo> Result();
        void Hide();
        void Show();
    }
}
