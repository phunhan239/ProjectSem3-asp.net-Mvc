namespace FinalShopOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    [Table("NicheInformation")]
    public partial class NicheInformation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Ảnh")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(2000)]
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        [DisplayName("Mở bài")]
        public string HeadContent { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [DisplayName("Nội dung")]
        public string BlogContent { get; set; }
    }
}
