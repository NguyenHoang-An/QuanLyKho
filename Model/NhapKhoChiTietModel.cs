using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class NhapKhoChiTietModel
    {
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public string Ma_SP { get; set; } = string.Empty;

        public string? Ten_SP { get; set; }

        [Required(ErrorMessage = "ID sản phẩm không được để trống")]
        public int San_Pham_ID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SL_Nhap { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 0")]
        public decimal Don_Gia_Nhap { get; set; }

        public decimal TriGia => SL_Nhap * Don_Gia_Nhap;
    }

}
