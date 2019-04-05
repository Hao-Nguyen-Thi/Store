namespace Mobie_store.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class comment
    {
        public int id { get; set; }

        public int? product_id { get; set; }

        public int? users_id { get; set; }

        [StringLength(4000)]
        public string comments { get; set; }

        public virtual product product { get; set; }

        public virtual user user { get; set; }
    }
}
