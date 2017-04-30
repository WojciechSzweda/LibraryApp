namespace Biblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Column("First Name")]
        [Required]
        [StringLength(50)]
        public string First_Name { get; set; }

        [Column("Last Name")]
        [Required]
        [StringLength(50)]
        public string Last_Name { get; set; }

        public bool isAdmin { get; set; }
    }
}
