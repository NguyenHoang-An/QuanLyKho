using Client.Model;

namespace Client.Server
{
    public interface IXuatKhoService
    {
        Task<List<XuatKhoHeader>> GetDanhSachPhieuAsync();
        Task<List<XuatKhoEdit>> GetDanhSachPhieuTuCustomAsync();
        Task<XuatKhoHeader?> GetPhieuAsync(string soPhieu);
        Task<XuatKhoEdit?> GetPhieuFromCustomTableAsync(string soPhieu);
        Task<bool> CreatePhieuAsync(XuatKhoHeader header);
        Task<bool> InsertToCustomTableAsync(XuatKhoEdit header);
        Task<bool> DeletePhieuAsync(string soPhieu);
        Task<bool> DeletePhieuFromCustomTableAsync(string soPhieu);
        Task<bool> UpdateHeaderAsync(XuatKhoHeader header);
        Task<bool> UpdateHeaderToCustomTableAsync(XuatKhoEdit header);
        Task<List<XuatKhoDetail>> GetChiTietPhieuAsync(int xuatKhoId);
        Task<bool> UpdateChiTietAsync(int xuatKhoId, List<XuatKhoDetail> chiTiet);
    }
}
