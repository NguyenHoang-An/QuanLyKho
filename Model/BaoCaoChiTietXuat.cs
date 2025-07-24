namespace Client.Model
{
    public class BaoCaoChiTietXuat
    {
        public DateTime Ngay_Xuat { get; set; }
        public string So_Phieu_Xuat_Kho { get; set; } = string.Empty;
        public string Ma_SP { get; set; } = string.Empty;
        public string Ten_SP { get; set; } = string.Empty;
        public int SL_Xuat { get; set; }
        public decimal Don_Gia { get; set; }
        public decimal Tri_Gia { get; set; }
    }
}
