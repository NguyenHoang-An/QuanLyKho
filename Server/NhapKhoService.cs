using Client.Data;
using Client.Model;
using Microsoft.EntityFrameworkCore;

namespace Client.Server
{
    public class NhapKhoService : INhapKhoService
    {
        private readonly AppDbContext _context;

        public NhapKhoService(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<List<NhapKho>> GetAllAsync()
        {
            return await _context.HieuChinhThongTinPhieuNhaps
                .Include(x => x.Kho)
                .Include(x => x.NhaCungCap)
                .Select(x => new NhapKho
                {
                    Id = x.Id,
                    So_Phieu_Nhap_Kho = x.So_Phieu_Nhap_Kho,
                    Kho_ID = x.Kho_ID,
                    NCC_ID = x.NCC_ID,
                    Ngay_Nhap_Kho = x.Ngay_Nhap_Kho,
                    Ghi_Chu = x.Ghi_Chu,
                    Kho = x.Kho,
                    NhaCungCap = x.NhaCungCap
                })
                .OrderByDescending(x => x.Ngay_Nhap_Kho)
                .ToListAsync();
        }

        public async Task<List<Kho>> GetAllKhosAsync() => await _context.Khos.OrderBy(x => x.Ten_Kho).ToListAsync();
        public async Task<List<NhaCungCap>> GetAllNCCsAsync() => await _context.NhaCungCaps.OrderBy(x => x.Ten_NCC).ToListAsync();

        
        public async Task<bool> IsSoPhieuNhapUniqueAsync(string soPhieuNhap)
        {
            var exists = await _context.NhapKhos.AnyAsync(x => x.So_Phieu_Nhap_Kho == soPhieuNhap)
                       || await _context.HieuChinhThongTinPhieuNhaps.AnyAsync(x => x.So_Phieu_Nhap_Kho == soPhieuNhap);
            return !exists;
        }

        public async Task<bool> IsSoPhieuNhapUniqueExceptIdAsync(string soPhieu, int excludeId)
        {
            var exists = await _context.NhapKhos.AnyAsync(x => x.So_Phieu_Nhap_Kho == soPhieu && x.Id != excludeId)
                       || await _context.HieuChinhThongTinPhieuNhaps.AnyAsync(x => x.So_Phieu_Nhap_Kho == soPhieu && x.Id != excludeId);
            return !exists;
        }

        
        public async Task<int> CreateOriginalAsync(NhapKho model, List<NhapKhoRawData> chiTiet)
        {
            await _context.NhapKhos.AddAsync(model);
            await _context.SaveChangesAsync();

            foreach (var item in chiTiet)
                item.Nhap_Kho_ID = model.Id;

            await _context.NhapKhoRawDatas.AddRangeAsync(chiTiet);
            await _context.SaveChangesAsync();

            var sync = new HieuChinhThongTinPhieuNhap
            {
                So_Phieu_Nhap_Kho = model.So_Phieu_Nhap_Kho ?? string.Empty,
                Kho_ID = model.Kho_ID,
                NCC_ID = model.NCC_ID,
                Ngay_Nhap_Kho = model.Ngay_Nhap_Kho ?? DateTime.Now,
                Ghi_Chu = model.Ghi_Chu
            };

            await _context.HieuChinhThongTinPhieuNhaps.AddAsync(sync);
            await _context.SaveChangesAsync();

            return model.Id;
        }

        
        public async Task<List<NhapKhoRawData>> GetDetailsAsync(int nhapKhoId)
        {
            return await _context.NhapKhoRawDatas
                .Where(x => x.Nhap_Kho_ID == nhapKhoId)
                .Include(x => x.SanPham)
                .ToListAsync();
        }

        public async Task<List<NhapKhoChiTietModel>> GetChiTietModelsAsync(int nhapKhoId)
        {
            return await _context.NhapKhoRawDatas
                .Where(x => x.Nhap_Kho_ID == nhapKhoId)
                .Join(_context.SanPhams, x => x.San_Pham_ID, s => s.Id, (x, s) => new NhapKhoChiTietModel
                {
                    Ma_SP = s.Ma_SP,
                    Ten_SP = s.Ten_SP,
                    San_Pham_ID = s.Id,
                    SL_Nhap = x.SL_Nhap,
                    Don_Gia_Nhap = x.Don_Gia_Nhap
                })
                .ToListAsync();
        }

        public async Task AddDetailAsync(int nhapKhoId, NhapKhoChiTietModel detail)
        {
            var sp = await _context.SanPhams.FirstOrDefaultAsync(x => x.Ma_SP == detail.Ma_SP);
            if (sp == null) throw new Exception("Không tìm thấy sản phẩm.");

            await _context.NhapKhoRawDatas.AddAsync(new NhapKhoRawData
            {
                Nhap_Kho_ID = nhapKhoId,
                San_Pham_ID = sp.Id,
                SL_Nhap = detail.SL_Nhap,
                Don_Gia_Nhap = detail.Don_Gia_Nhap
            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDetailAsync(int nhapKhoId, NhapKhoChiTietModel detail)
        {
            var entity = await _context.NhapKhoRawDatas.FirstOrDefaultAsync(x => x.Nhap_Kho_ID == nhapKhoId && x.San_Pham_ID == detail.San_Pham_ID);
            if (entity == null) throw new Exception("Chi tiết không tồn tại.");

            entity.SL_Nhap = detail.SL_Nhap;
            entity.Don_Gia_Nhap = detail.Don_Gia_Nhap;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDetailAsync(int nhapKhoId, int sanPhamId)
        {
            var entity = await _context.NhapKhoRawDatas.FirstOrDefaultAsync(x => x.Nhap_Kho_ID == nhapKhoId && x.San_Pham_ID == sanPhamId);
            if (entity != null)
            {
                _context.NhapKhoRawDatas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddChiTietNhapAsync(NhapKhoRawData item)
        {
            _context.NhapKhoRawDatas.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChiTietNhapAsync(int id)
        {
            var entity = await _context.NhapKhoRawDatas.FindAsync(id);
            if (entity != null)
            {
                _context.NhapKhoRawDatas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // ===== Truy xuất =====
        public async Task<Kho?> GetKhoByIdAsync(int id) => await _context.Khos.FindAsync(id);
        public async Task<NhaCungCap?> GetNCCByIdAsync(int id) => await _context.NhaCungCaps.FindAsync(id);
        public async Task<SanPham?> GetProductByMaSPAsync(string maSP) => await _context.SanPhams.FirstOrDefaultAsync(sp => sp.Ma_SP == maSP);

        public async Task<int?> GetOriginalNhapKhoIdBySoPhieuAsync(string soPhieu)
        {
            var record = await _context.NhapKhos.FirstOrDefaultAsync(x => x.So_Phieu_Nhap_Kho == soPhieu);
            return record?.Id;
        }

        public async Task<HieuChinhThongTinPhieuNhap?> GetHeaderAsync(string soPhieu)
        {
            soPhieu = soPhieu.Trim().ToLower();
            return await _context.HieuChinhThongTinPhieuNhaps
                .Include(p => p.Kho)
                .Include(p => p.NhaCungCap)
                .FirstOrDefaultAsync(p => p.So_Phieu_Nhap_Kho.ToLower() == soPhieu);
        }


        public async Task<HieuChinhThongTinPhieuNhap?> GetHeaderAsync(int id)
        {
            return await _context.HieuChinhThongTinPhieuNhaps
                .Include(p => p.Kho)
                .Include(p => p.NhaCungCap)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<HieuChinhThongTinPhieuNhap?> GetHeaderAsyncById(int id)
        {
            var entity = await _context.NhapKhos.FindAsync(id);
            if (entity == null) return null;

            return new HieuChinhThongTinPhieuNhap
            {
                Id = entity.Id,
                So_Phieu_Nhap_Kho = entity.So_Phieu_Nhap_Kho,
                Kho_ID = entity.Kho_ID,
                NCC_ID = entity.NCC_ID,
                Ngay_Nhap_Kho = (DateTime)entity.Ngay_Nhap_Kho,
                Ghi_Chu = entity.Ghi_Chu
            };
        }

        public async Task<NhapKho?> GetByIdAsync(int id)
        {
            return await _context.NhapKhos.FindAsync(id);
        }

        
        public async Task UpdateHeaderAsync(HieuChinhThongTinPhieuNhap model)
        {
            var entity = await _context.HieuChinhThongTinPhieuNhaps.FindAsync(model.Id);

            if (entity == null)
            {
                
                entity = new HieuChinhThongTinPhieuNhap
                {
                    Id = model.Id
                };
                _context.HieuChinhThongTinPhieuNhaps.Add(entity);
            }

            
            entity.So_Phieu_Nhap_Kho = model.So_Phieu_Nhap_Kho;
            entity.Kho_ID = model.Kho_ID;
            entity.NCC_ID = model.NCC_ID;
            entity.Ngay_Nhap_Kho = model.Ngay_Nhap_Kho;
            entity.Ghi_Chu = model.Ghi_Chu;

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var nhapKho = await _context.NhapKhos.FindAsync(id);
            if (nhapKho != null)
            {
                var raw = _context.NhapKhoRawDatas.Where(x => x.Nhap_Kho_ID == id);
                _context.NhapKhoRawDatas.RemoveRange(raw);
                _context.NhapKhos.Remove(nhapKho);
            }

            var edit = await _context.HieuChinhThongTinPhieuNhaps.FindAsync(id);
            if (edit != null)
            {
                _context.HieuChinhThongTinPhieuNhaps.Remove(edit);
            }

            await _context.SaveChangesAsync();
        }
    }
}