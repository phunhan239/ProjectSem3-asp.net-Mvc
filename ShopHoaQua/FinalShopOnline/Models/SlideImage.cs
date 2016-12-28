using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalShopOnline.Models
{
    public class SlideImage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Ảnh")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Trạng thái")]
        public string Status { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Vị trí hiển thị")]
        public string DisplayPosition { get; set; }

        [Required]
        [DisplayName("Số thứ tự")]
        public int? SortOrder { get; set; }
    }
}