namespace Biblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Author")]
    public partial class Author
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Author()
        {
            Authors = new HashSet<Authors>();
        }

        public int ID { get; set; }

        [Column("First Name")]
        [StringLength(50)]
        public string First_Name { get; set; }

        [Column("Last Name")]
        [StringLength(50)]
        public string Last_Name { get; set; }

        [Column("Year of Birth", TypeName = "date")]
        public DateTime? Year_of_Birth { get; set; }

        [StringLength(50)]
        public string Nationality { get; set; }

        [StringLength(50)]
        public string Signature { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Authors> Authors { get; set; }
    }
}
