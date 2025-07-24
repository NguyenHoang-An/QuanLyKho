using Client.Model;

namespace Client.Server
{
    public interface ISanPhamService
    {
        Task<List<SanPham>> GetAllAsync();
        Task<SanPham?> GetByIdAsync(int id);
        Task AddAsync(SanPham item);
        Task UpdateAsync(SanPham item);
        Task DeleteAsync(int id);
    }
}
