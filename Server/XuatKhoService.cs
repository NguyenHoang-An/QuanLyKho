using Client.Data;
using Client.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Client.Server
{
    public class XuatKhoService : IXuatKhoService
    {
        private readonly AppDbContext _context;

        public XuatKhoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<XuatKhoHeader>> GetDanhSachPhieuAsync()
        {
            return await _context.XuatKho
                .Include(x => x.ChiTiet)
                .OrderByDescending(x => x.Ngay_Xuat_Kho)
                .ToListAsync();
        }

        public async Task<List<XuatKhoEdit>> GetDanhSachPhieuTuCustomAsync()
        {
            return await _context.XuatKhoEdit
                .OrderByDescending(x => x.Ngay_Xuat_Kho)
                .ToListAsync();
        }

        public async Task<XuatKhoHeader?> GetPhieuAsync(string soPhieu)
        {
            return await _context.XuatKho
                .Include(x => x.ChiTiet)
                .FirstOrDefaultAsync(x => x.So_Phieu_Xuat_Kho == soPhieu);
        }

        public async Task<XuatKhoEdit?> GetPhieuFromCustomTableAsync(string soPhieu)
        {
            return await _context.XuatKhoEdit
                .FirstOrDefaultAsync(x => x.So_Phieu_Xuat_Kho == soPhieu);
        }

        public async Task<bool> CreatePhieuAsync(XuatKhoHeader header)
        {
            var isDuplicate = await _context.XuatKho.AnyAsync(x => x.So_Phieu_Xuat_Kho == header.So_Phieu_Xuat_Kho);
            if (isDuplicate)
                throw new Exception($"❌ Số phiếu '{header.So_Phieu_Xuat_Kho}' đã tồn tại. Không thể tạo mới.");

            _context.XuatKho.Add(header);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertToCustomTableAsync(XuatKhoEdit header)
        {
            try
            {
                _context.XuatKhoEdit.Add(header);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Insert tbl_XNK_Xuat_Kho lỗi: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeletePhieuAsync(string soPhieu)
        {
            var phieu = await _context.XuatKho
                .Include(x => x.ChiTiet)
                .FirstOrDefaultAsync(x => x.So_Phieu_Xuat_Kho == soPhieu);

            if (phieu == null) return false;

            _context.XuatKho.Remove(phieu);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePhieuFromCustomTableAsync(string soPhieu)
        {
            try
            {
                var phieu = await _context.XuatKhoEdit.FirstOrDefaultAsync(x => x.So_Phieu_Xuat_Kho == soPhieu);
                if (phieu == null) return false;

                _context.XuatKhoEdit.Remove(phieu);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Lỗi khi xóa phiếu từ custom table: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateHeaderAsync(XuatKhoHeader header)
        {
            _context.XuatKho.Update(header);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateHeaderToCustomTableAsync(XuatKhoEdit header)
        {
            try
            {
                var trung = await _context.XuatKhoEdit
                    .AnyAsync(x => x.So_Phieu_Xuat_Kho == header.So_Phieu_Xuat_Kho && x.Id != header.Id);
                if (trung)
                    throw new Exception($"❌ Số phiếu '{header.So_Phieu_Xuat_Kho}' đã tồn tại. Không thể cập nhật.");

                
                var existing = await _context.XuatKhoEdit.FirstOrDefaultAsync(x => x.Id == header.Id);
                if (existing == null) return false;

                existing.So_Phieu_Xuat_Kho = header.So_Phieu_Xuat_Kho;
                existing.Kho_ID = header.Kho_ID;
                existing.Ngay_Xuat_Kho = header.Ngay_Xuat_Kho;
                existing.Ghi_Chu = header.Ghi_Chu;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ EXCEPTION trong UpdateHeaderToCustomTableAsync:");
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public async Task<List<XuatKhoDetail>> GetChiTietPhieuAsync(int xuatKhoId)
        {
            return await _context.XuatKhoRaw
                .Where(c => c.Xuat_Kho_ID == xuatKhoId)
                .ToListAsync();
        }

        public async Task<bool> UpdateChiTietAsync(int xuatKhoId, List<XuatKhoDetail> chiTiet)
        {
            var exists = await _context.XuatKho.AnyAsync(x => x.Id == xuatKhoId);
            if (!exists)
            {
                throw new Exception($"❌ Không tồn tại phiếu xuất với ID = {xuatKhoId}, không thể cập nhật chi tiết.");
            }

            var existingDetails = await _context.XuatKhoRaw
                .Where(c => c.Xuat_Kho_ID == xuatKhoId)
                .ToListAsync();

            _context.XuatKhoRaw.RemoveRange(existingDetails);

            var newItems = chiTiet
                .Where(x => !string.IsNullOrWhiteSpace(x.San_Pham_ID) && x.SL_Xuat > 0 && x.Don_Gia_Xuat >= 0)
                .Select(x => new XuatKhoDetail
                {
                    Xuat_Kho_ID = xuatKhoId,
                    San_Pham_ID = x.San_Pham_ID,
                    SL_Xuat = x.SL_Xuat,
                    Don_Gia_Xuat = x.Don_Gia_Xuat
                }).ToList();

            _context.XuatKhoRaw.AddRange(newItems);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
