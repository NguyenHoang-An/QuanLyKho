using Client.Model;

namespace Client.Server
{
    public interface IKhoUserService
    {
        Task<List<KhoUser>> GetAllAsync();
        Task<List<Kho>> GetAllKhosAsync();
        Task<bool> IsUniqueAsync(string maDangNhap, int khoId, int? excludeId = null);
        Task AddAsync(KhoUser khoUser);
        Task DeleteAsync(int id);

       
    }
}
