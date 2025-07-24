using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class DonViTinh
    {
        public int Id { get; set; }
        [Required]
        public string? Ten_DonViTinh { get; set; }
        public string? Ghi_Chu { get; set; }
        
    }
}
