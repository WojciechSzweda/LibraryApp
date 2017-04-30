namespace Biblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Return")]
    public partial class Return
    {
        public DateTime Return_Date { get; set; }

        [Column("State_Pre-Return")]
        public int? State_Pre_Return { get; set; }

        [Column("State_After-Return")]
        public int? State_After_Return { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Rent { get; set; }

        public virtual Book_State Book_State { get; set; }

        public virtual Book_State Book_State1 { get; set; }

        public virtual Rent Rent { get; set; }
    }
}
