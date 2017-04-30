namespace Biblioteka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Authors
    {
        public int ID { get; set; }

        public int ID_Book { get; set; }

        public int ID_Author { get; set; }

        public virtual Author Author { get; set; }

        public virtual Book Book { get; set; }
    }
}
