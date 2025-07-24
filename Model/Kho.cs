using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class Kho
    {
        public int Id { get; set; }
        [Required]
        public string? Ten_Kho { get; set; }
        public string? Ghi_Chu { get; set; }
    }
}
