using Client.Model;

namespace Client.Server
{
    public interface INhaCungCapService
    {
        Task<List<NhaCungCap>> GetAllAsync();
        Task<NhaCungCap?> GetByIdAsync(int id);
        Task AddAsync(NhaCungCap ncc);
        Task UpdateAsync(NhaCungCap ncc);
        Task DeleteAsync(int id);
    }
}
