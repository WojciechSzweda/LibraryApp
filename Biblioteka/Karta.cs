//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biblioteka
{
    using System;
    using System.Collections.Generic;
    
    public partial class Karta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Karta()
        {
            this.Wypożyczenie = new HashSet<Wypożyczenie>();
        }
    
        public int ID { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string Miejscowość { get; set; }
        public string Kod_pocztowy { get; set; }
        public string Ulica { get; set; }
        public string Nr_domu { get; set; }
        public string Nr_mieszkania { get; set; }
        public string Nr_kontaktowy { get; set; }
        public System.DateTime Data_wydania { get; set; }
        public int Stan { get; set; }
    
        public virtual Stan_Karty Stan_Karty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wypożyczenie> Wypożyczenie { get; set; }
    }
}