namespace Biblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Card")]
    public partial class Card
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card()
        {
            Rent = new HashSet<Rent>();
        }

        public int ID { get; set; }

        [Column("First Name")]
        [Required]
        [StringLength(50)]
        public string First_Name { get; set; }

        [Column("Last Name")]
        [Required]
        [StringLength(50)]
        public string Last_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Column("Postal Code")]
        [Required]
        [StringLength(6)]
        public string Postal_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Street { get; set; }

        [Column("House Number")]
        [Required]
        [StringLength(5)]
        public string House_Number { get; set; }

        [Column("Apartment Nuber")]
        [StringLength(5)]
        public string Apartment_Nuber { get; set; }

        [Column("Phone Number")]
        [StringLength(9)]
        public string Phone_Number { get; set; }

        [Column("Create Date", TypeName = "date")]
        public DateTime Create_Date { get; set; }

        public int State { get; set; }

        public virtual Card_State Card_State { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rent> Rent { get; set; }
    }
}
