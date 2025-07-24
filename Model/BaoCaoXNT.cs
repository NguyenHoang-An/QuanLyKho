namespace Client.Model
{
    public class BaoCaoXNT
    {
        public string Ma_SP { get; set; } = string.Empty;
        public string Ten_SP { get; set; } = string.Empty;
        public int So_Luong_Dau_Ky { get; set; }
        public int So_Luong_Nhap { get; set; }
        public int So_Luong_Xuat { get; set; }
        public int So_Luong_Cuoi_Ky => So_Luong_Dau_Ky + So_Luong_Nhap - So_Luong_Xuat;
    }
}
