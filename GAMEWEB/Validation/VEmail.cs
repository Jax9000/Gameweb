using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GAMEWEB.Validation {
    public class VEmail : ValidationRule {

        public VEmail() {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            if ((value as string).Contains("@"))
                return new ValidationResult(true, null);
            return new ValidationResult(false, "Błędna wartość!");
        }
    }
}
