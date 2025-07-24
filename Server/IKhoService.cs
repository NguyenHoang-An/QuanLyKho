using Client.Model;

namespace Client.Server
{
    public interface IKhoService
    {
        Task<List<Kho>> GetAllAsync();
        Task<Kho?> GetByIdAsync(int id);
        Task<bool> IsTenKhoUniqueAsync(string tenKho, int? id = null);
        Task AddAsync(Kho kho);
        Task UpdateAsync(Kho kho);
        Task DeleteAsync(int id);
        Task<List<Kho>> GetAllKhoAsync();

    }
}
