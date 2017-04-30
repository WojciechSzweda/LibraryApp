namespace Biblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book_Category
    {
        public int ID { get; set; }

        public int ID_Book { get; set; }

        public int ID_Category { get; set; }

        public virtual Book Book { get; set; }

        public virtual Category Category { get; set; }
    }
}
