using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class KhoUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã đăng nhập không được để trống")]
        public string Ma_Dang_Nhap { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kho không được để trống")]
        public int Kho_ID { get; set; }
    }
}
