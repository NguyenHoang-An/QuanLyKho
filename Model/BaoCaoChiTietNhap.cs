namespace Client.Model
{
    public class BaoCaoChiTietNhap
    {
        public DateTime NgayNhap { get; set; }
        public string SoPhieuNhap { get; set; } = "";
        public string NhaCungCap { get; set; } = "";
        public string MaSanPham { get; set; } = "";
        public string TenSanPham { get; set; } = "";
        public int SoLuongNhap { get; set; }
        public decimal DonGia { get; set; }

        public decimal TriGia => SoLuongNhap * DonGia;
    }
}
