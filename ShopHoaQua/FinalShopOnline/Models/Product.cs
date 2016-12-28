namespace FinalShopOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.Mvc;
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Mã sản phẩm")]
        public string Code { get; set; }

        [Required]
        [DisplayName("Tên sản phẩm")]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Giá tiền")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Số lượng")]
        public decimal Stock { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập thông tin cho dòng này.")]
        [StringLength(10)]
        public string Unit { get; set; }

        [StringLength(1000)]
        [DisplayName("Ảnh")]
        public string ImageUrl { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("Ghi chú")]
        public string Notes { get; set; }

        public decimal Discount { get; set; }

        [Required]
        [DisplayName("Vị trí hiển thị")]
        [StringLength(50)]
        public string DisplayPosition { get; set; }

        [Required]
        [DisplayName("Trạng thái")]
        [StringLength(50)]
        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [DisplayName("Số thứ tự")]
        public int SortOrder { get; set; }

        [DisplayName("Loại sản phẩm")]
        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
