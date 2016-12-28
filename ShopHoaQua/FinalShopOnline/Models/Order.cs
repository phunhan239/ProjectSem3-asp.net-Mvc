namespace FinalShopOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }

        [Required]
        [DisplayName("Mã số")]
        [StringLength(50)]
        public string Code { get; set; }

        [DisplayName("Ngày tháng")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập thông tin cho dòng này.")]
        [DisplayName("Họ và tên")]
        [StringLength(250)]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập thông tin cho dòng này.")]
        [DisplayName("Địa chỉ")]
        [StringLength(250)]
        public string ContactAddress { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập thông tin cho dòng này.")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Entered phone format is not valid.")]
        [DisplayName("Điện thoại")]
        [StringLength(250)]
        public string ContactPhone { get; set; }

        [StringLength(250)]
        [DisplayName("Email")]
        public string ContactEmail { get; set; }

        [Required]
        [DisplayName("Trạng thái")]
        [StringLength(50)]
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
