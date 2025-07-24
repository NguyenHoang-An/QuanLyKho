using Client.Model;

namespace Client.Server
{
    public interface IThongKeService
    {
        Task<List<BaoCaoChiTietNhap>> GetBaoCaoNhapAsync(DateTime tuNgay, DateTime denNgay);
        Task<List<BaoCaoChiTietXuat>> GetBaoCaoXuatAsync(DateTime tuNgay, DateTime denNgay);
        Task<List<BaoCaoXNT>> GetBaoCaoXNT(DateTime tuNgay, DateTime denNgay);
        
    }
}
