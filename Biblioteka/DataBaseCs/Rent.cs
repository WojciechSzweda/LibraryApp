namespace Biblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rent")]
    public partial class Rent
    {
        public int ID { get; set; }

        public int ID_Copy { get; set; }

        public DateTime Rent_Date { get; set; }

        public int ID_Card { get; set; }

        [Column(TypeName = "date")]
        public DateTime Expected_Return_Date { get; set; }

        public virtual Card Card { get; set; }

        public virtual Copy Copy { get; set; }

        public virtual Return Return { get; set; }
    }
}
