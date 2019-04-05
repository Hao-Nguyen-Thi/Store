namespace Mobie_store.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class post
    {
        public int id { get; set; }

        public int? product_id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(4000)]
        public string descripton { get; set; }

        public virtual product product { get; set; }
    }
}
