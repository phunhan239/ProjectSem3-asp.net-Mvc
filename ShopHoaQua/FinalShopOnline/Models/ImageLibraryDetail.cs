namespace FinalShopOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImageLibraryDetail
    {
        public int Id { get; set; }

        public int ImageLibraryId { get; set; }

        [Required]
        [StringLength(200)]
        public string ImageUrl { get; set; }
    }
}
