using Client.Data;
using Client.Model;
using Microsoft.EntityFrameworkCore;

namespace Client.Server
{
    public class KhoService : IKhoService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public KhoService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Kho>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Khos.OrderBy(k => k.Ten_Kho).ToListAsync();
        }


        public async Task<Kho?> GetByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Khos.FindAsync(id);
        }

        public async Task AddAsync(Kho entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Khos.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Kho entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Khos.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var entity = await context.Khos.FindAsync(id);
            if (entity != null)
            {
                context.Khos.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsTenKhoUniqueAsync(string tenKho, int? id = null)
        {
            using var context = _contextFactory.CreateDbContext();
            if (id == null)
            {
                return !await context.Khos.AnyAsync(k => k.Ten_Kho == tenKho);
            }
            else
            {
                return !await context.Khos.AnyAsync(k => k.Ten_Kho == tenKho && k.Id != id);
            }
        }
        public async Task<List<Kho>> GetAllKhoAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Khos.OrderBy(k => k.Ten_Kho).ToListAsync();
        }

    }
}
