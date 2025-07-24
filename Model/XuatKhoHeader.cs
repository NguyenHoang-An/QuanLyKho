using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Model
{
    public class XuatKhoHeader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string So_Phieu_Xuat_Kho { get; set; } = string.Empty;

        [Required]
        public int? Kho_ID { get; set; }

        public DateTime Ngay_Xuat_Kho { get; set; }

        public string? Ghi_Chu { get; set; }

        public virtual ICollection<XuatKhoDetail> ChiTiet { get; set; } = new List<XuatKhoDetail>();
    }
}