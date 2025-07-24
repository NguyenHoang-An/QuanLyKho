using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class NhapKho
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Số phiếu nhập không được để trống")]
        public string? So_Phieu_Nhap_Kho { get; set; }

        [Required(ErrorMessage = "Kho không được để trống")]
        public int? Kho_ID { get; set; }

        [Required(ErrorMessage = "Nhà cung cấp không được để trống")]
        public int? NCC_ID { get; set; }

        [Required(ErrorMessage = "Ngày nhập không được để trống")]
        public DateTime? Ngay_Nhap_Kho { get; set; }

        public string? Ghi_Chu { get; set; }

        public Kho? Kho { get; set; }
        public NhaCungCap? NhaCungCap { get; set; }
        public List<NhapKhoRawData> NhapKhoRawDatas { get; set; } = new();
        public string? Ten_Kho { get; set; }
        public string? Ten_NCC { get; set; }
    }
}
