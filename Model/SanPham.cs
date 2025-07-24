using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class SanPham
    {
        public int Id { get; set; }
        [Required]
        public string? Ma_SP { get; set; }
        [Required]
        public string? Ten_SP { get; set; }      
        public int DonViTinhId { get; set; }
        public DonViTinh? DonViTinh { get; set; }
        public int LoaiSanPhamId { get; set; }
        public LoaiSanPham? LoaiSanPham { get; set; }
        public string? Ghi_Chu { get; set; }
    }
}
