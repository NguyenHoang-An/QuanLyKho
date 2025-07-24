using Client.Data;
using Client.Model;
using Microsoft.EntityFrameworkCore;

namespace Client.Server
{
    public class NhaCungCapService : INhaCungCapService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public NhaCungCapService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<NhaCungCap>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.NhaCungCaps.ToListAsync();
        }

        public async Task<NhaCungCap?> GetByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.NhaCungCaps.FindAsync(id);
        }

        public async Task AddAsync(NhaCungCap entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.NhaCungCaps.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NhaCungCap entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.NhaCungCaps.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var entity = await context.NhaCungCaps.FindAsync(id);
            if (entity != null)
            {
                context.NhaCungCaps.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
