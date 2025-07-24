using Client.Data;
using Client.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Client.Server
{
    public class DonViTinhService : IDonViTinhService
    {
        private readonly AppDbContext _context;

        public DonViTinhService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DonViTinh>> GetAllAsync()
        {
            return await _context.DonViTinhs.ToListAsync();
        }

        public async Task<DonViTinh?> GetByIdAsync(int id)
        {
            return await _context.DonViTinhs.FindAsync(id);
        }

        public async Task AddAsync(DonViTinh item)
        {
            _context.DonViTinhs.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DonViTinh item)
        {
            // Gắn bản ghi với trạng thái Unchanged (nếu cần)
            var local = _context.DonViTinhs.Local.FirstOrDefault(entry => entry.Id == item.Id);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached; // Loại bỏ bản đang track
            }

            _context.Attach(item); // Gắn bản cần cập nhật
            _context.Entry(item).State = EntityState.Modified; // Đánh dấu là Modified
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DonViTinhs.FindAsync(id);
            if (entity != null)
            {
                _context.DonViTinhs.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}