using Client.Model;

namespace Client.Server
{
    public interface IDonViTinhService
    {
        Task<List<DonViTinh>> GetAllAsync();
        Task AddAsync(DonViTinh model);
        Task UpdateAsync(DonViTinh model);
        Task DeleteAsync(int id);
    }
}
