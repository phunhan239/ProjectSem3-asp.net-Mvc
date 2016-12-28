using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalShopOnline.Models
{
    public class UserComment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Ảnh")]
        public string ImageUrl { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [DisplayName("Comment")]
        public string Comment { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Trạng thái")]
        public string Status { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Tên khách hàng")]
        public string Name {get;set;}

        [Required]
        [StringLength(200)]
        [DisplayName("Địa chỉ khách hàng")]
        public string Address { get; set; }


    }
}