//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GAMEWEB.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Studia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Studia()
        {
            this.Gry = new HashSet<Gry>();
        }
    
        public int StudioID { get; set; }
        public string Nazwa { get; set; }
        public Nullable<System.DateTime> DataZalozenia { get; set; }
        public Nullable<int> IloscPracownikow { get; set; }
        public string Zalozyciel { get; set; }
        public string Opis { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gry> Gry { get; set; }
    }
}
