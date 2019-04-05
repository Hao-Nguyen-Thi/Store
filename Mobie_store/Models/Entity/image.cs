namespace Mobie_store.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("image")]
    public partial class image
    {
        public int id { get; set; }

        [StringLength(255)]
        public string url { get; set; }

        public int? product_id { get; set; }

        public virtual product product { get; set; }
    }
}
