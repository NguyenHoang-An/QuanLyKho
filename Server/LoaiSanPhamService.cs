using Client.Data;
using Client.Model;
using Microsoft.EntityFrameworkCore;

namespace Client.Server
{
    public class LoaiSanPhamService : ILoaiSanPhamService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public LoaiSanPhamService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<LoaiSanPham>> GetAllAsync()
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.LoaiSanPhams.ToListAsync();
        }

        public async Task AddAsync(LoaiSanPham item)
        {
            using var context = _dbFactory.CreateDbContext();
            context.LoaiSanPhams.Add(item);
            await context.SaveChangesAsync();
        }

        public async Task<LoaiSanPham?> GetByIdAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            return await context.LoaiSanPhams.FindAsync(id);
        }

        public async Task UpdateAsync(LoaiSanPham item)
        {
            using var context = _dbFactory.CreateDbContext();
            context.LoaiSanPhams.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _dbFactory.CreateDbContext();
            var entity = await context.LoaiSanPhams.FindAsync(id);
            if (entity != null)
            {
                context.LoaiSanPhams.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}

