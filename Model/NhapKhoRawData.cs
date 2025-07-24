using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class NhapKhoRawData
    {
        public int Id { get; set; }

        [Required]
        public int Nhap_Kho_ID { get; set; }

        [Required]
        public int San_Pham_ID { get; set; }

        [Required]
        public int SL_Nhap { get; set; }

        [Required]
        public decimal Don_Gia_Nhap { get; set; }

        public string? Ghi_Chu { get; set; }

        public NhapKho? NhapKho { get; set; }
        public SanPham? SanPham { get; set; }
    }

}
