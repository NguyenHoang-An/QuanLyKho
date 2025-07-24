using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class LoaiSanPham
    {
        public int Id { get; set; }
        [Required]
        public string? Ma_LSP { get; set; }
        [Required]
        public string? Ten_LSP { get; set; }
        public string? Ghi_Chu { get; set; }
    }
}
