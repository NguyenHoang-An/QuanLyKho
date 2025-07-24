using Client.Data;
using Client.Model;
using Microsoft.EntityFrameworkCore;

namespace Client.Server
{
    public class KhoUserService : IKhoUserService
    {
        private readonly AppDbContext _context;

        public KhoUserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<KhoUser>> GetAllAsync()
        {
            return await _context.KhoUsers.ToListAsync();
        }

        public async Task<KhoUser?> GetByIdAsync(int id)
        {
            return await _context.KhoUsers.FindAsync(id);
        }

        public async Task AddAsync(KhoUser item)
        {
            _context.KhoUsers.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(KhoUser item)
        {
            _context.KhoUsers.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KhoUsers.FindAsync(id);
            if (entity != null)
            {
                _context.KhoUsers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Kho>> GetAllKhosAsync()
        {
            return await _context.Khos.ToListAsync();
        }

        public async Task<bool> IsUniqueAsync(string maDangNhap, int khoId, int? excludeId = null)
        {
            return !await _context.KhoUsers
                .AnyAsync(x => x.Ma_Dang_Nhap == maDangNhap && x.Kho_ID == khoId && (excludeId == null || x.Id != excludeId));
        }

    }
}
