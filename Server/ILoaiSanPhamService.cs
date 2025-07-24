using Client.Model;

namespace Client.Server
{
    public interface ILoaiSanPhamService
    {
        Task<List<LoaiSanPham>> GetAllAsync();
        Task AddAsync(LoaiSanPham loai);
        Task UpdateAsync(LoaiSanPham loai);
        Task DeleteAsync(int id);
    }
}
