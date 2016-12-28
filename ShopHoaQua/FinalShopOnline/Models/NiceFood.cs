namespace FinalShopOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    public partial class NiceFood
    {
        public int Id { get; set; }

        [StringLength(200)]
        [DisplayName("Ảnh")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        [DisplayName("Mở bài")]
        public string HeadContent { get; set; }

        public DateTime? CreateDate { get; set; }

        [Required]
        [AllowHtml]
        [DisplayName("Nội dung")]
        [DataType(DataType.MultilineText)]
        public string BlogContent { get; set; }
    }
}
