using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class NhaCungCap
    {
        public int Id { get; set; }
        [Required]
        public string? Ma_NCC { get; set; }
        [Required]
        public string? Ten_NCC { get; set; }
        public string? Ghi_Chu { get; set; }

    }
}
