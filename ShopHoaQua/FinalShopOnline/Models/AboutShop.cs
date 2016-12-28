namespace FinalShopOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    [Table("AboutShop")]
    public partial class AboutShop
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [DisplayName("Nội dung")]
        public string Text { get; set; }
    }
}
