using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Model
{
    public class HieuChinhThongTinPhieuNhap
    {
        public int Id { get; set; }
        public string So_Phieu_Nhap_Kho { get; set; } = string.Empty;
        public int? Kho_ID { get; set; }
        public int? NCC_ID { get; set; }
        public DateTime Ngay_Nhap_Kho { get; set; }
        public string? Ghi_Chu { get; set; }

        [ForeignKey(nameof(Kho_ID))]
        public Kho? Kho { get; set; }

        [ForeignKey(nameof(NCC_ID))]
        public NhaCungCap? NhaCungCap { get; set; }
    }
}
