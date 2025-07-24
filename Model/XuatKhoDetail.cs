using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class XuatKhoDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Xuat_Kho_ID { get; set; }
        public string San_Pham_ID { get; set; } = string.Empty;
        public int SL_Xuat { get; set; }
        public decimal Don_Gia_Xuat { get; set; }
        public virtual XuatKhoHeader PhieuXuat { get; set; }
    }
}
