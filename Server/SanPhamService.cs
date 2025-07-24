using Client.Data;
using Client.Model;
using Microsoft.EntityFrameworkCore;

namespace Client.Server
{
    public class SanPhamService : ISanPhamService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public SanPhamService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<SanPham>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.SanPhams.ToListAsync();
        }

        public async Task<SanPham?> GetByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.SanPhams.FindAsync(id);
        }

        public async Task AddAsync(SanPham entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.SanPhams.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SanPham entity)
        {
            using var context = _contextFactory.CreateDbContext();

            var existing = await context.SanPhams.FindAsync(entity.Id);
            if (existing != null)
            {
                // Cập nhật thủ công các thuộc tính
                existing.Ma_SP = entity.Ma_SP;
                existing.Ten_SP = entity.Ten_SP;
                existing.Ghi_Chu = entity.Ghi_Chu;

                // Gán Id thay vì gán object để tránh conflict
                existing.DonViTinhId = entity.DonViTinhId;
                existing.LoaiSanPhamId = entity.LoaiSanPhamId;

                await context.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var entity = await context.SanPhams.FindAsync(id);
            if (entity != null)
            {
                context.SanPhams.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}