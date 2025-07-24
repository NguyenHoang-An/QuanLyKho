using Client.Data;
using Client.Model;
using Microsoft.EntityFrameworkCore;

namespace Client.Server
{
    public class ThongKeService : IThongKeService
    {
        private readonly AppDbContext _context;

        public ThongKeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BaoCaoChiTietNhap>> GetBaoCaoNhapAsync(DateTime tuNgay, DateTime denNgay)
        {
            return await (from pn in _context.NhapKhos
                          join ct in _context.NhapKhoRawDatas on pn.Id equals ct.Nhap_Kho_ID
                          join sp in _context.SanPhams on Convert.ToInt32(ct.San_Pham_ID) equals sp.Id
                          join ncc in _context.NhaCungCaps on pn.NCC_ID equals ncc.Id
                          where pn.Ngay_Nhap_Kho >= tuNgay && pn.Ngay_Nhap_Kho <= denNgay
                          select new BaoCaoChiTietNhap
                          {
                              NgayNhap = (DateTime)pn.Ngay_Nhap_Kho,
                              SoPhieuNhap = pn.So_Phieu_Nhap_Kho,
                              NhaCungCap = ncc.Ten_NCC,
                              MaSanPham = sp.Ma_SP,
                              TenSanPham = sp.Ten_SP,
                              SoLuongNhap = ct.SL_Nhap,
                              DonGia = ct.Don_Gia_Nhap
                          }).ToListAsync();
        }

        public async Task<List<BaoCaoChiTietXuat>> GetBaoCaoXuatAsync(DateTime tuNgay, DateTime denNgay)
        {
            return await (from hdr in _context.XuatKho
                          join ct in _context.XuatKhoRaw on hdr.Id equals ct.Xuat_Kho_ID
                          join sp in _context.SanPhams on ct.San_Pham_ID equals sp.Ma_SP
                          where hdr.Ngay_Xuat_Kho >= tuNgay && hdr.Ngay_Xuat_Kho <= denNgay
                          orderby hdr.Ngay_Xuat_Kho descending
                          select new BaoCaoChiTietXuat
                          {
                              Ngay_Xuat = hdr.Ngay_Xuat_Kho,
                              So_Phieu_Xuat_Kho = hdr.So_Phieu_Xuat_Kho,
                              Ma_SP = ct.San_Pham_ID,
                              Ten_SP = sp.Ten_SP,
                              SL_Xuat = ct.SL_Xuat,
                              Don_Gia = ct.Don_Gia_Xuat,
                              Tri_Gia = ct.SL_Xuat * ct.Don_Gia_Xuat
                          }).ToListAsync();
        }


        public async Task<List<BaoCaoXNT>> GetBaoCaoXNT(DateTime tuNgay, DateTime denNgay)
        {
            var sanPhams = await _context.SanPhams.ToListAsync();

            var nhapTheoSp = await _context.NhapKhoRawDatas
                .Where(x => x.NhapKho != null &&
                            x.NhapKho.Ngay_Nhap_Kho >= tuNgay &&
                            x.NhapKho.Ngay_Nhap_Kho <= denNgay)
                .GroupBy(x => x.San_Pham_ID)
                .Select(g => new { San_Pham_ID = g.Key, TongNhap = g.Sum(x => x.SL_Nhap) })
                .ToListAsync();

            var xuatTheoSp = await _context.XuatKhoRaw
                .Where(x => x.PhieuXuat != null &&
                            x.PhieuXuat.Ngay_Xuat_Kho >= tuNgay &&
                            x.PhieuXuat.Ngay_Xuat_Kho <= denNgay)
                .GroupBy(x => x.San_Pham_ID)
                .Select(g => new { San_Pham_ID = g.Key, TongXuat = g.Sum(x => x.SL_Xuat) })
                .ToListAsync();

            var list = sanPhams.Select(sp =>
            {
                var tongNhap = nhapTheoSp.FirstOrDefault(n => Convert.ToInt32(n.San_Pham_ID) == sp.Id)?.TongNhap ?? 0;
                var tongXuat = xuatTheoSp.FirstOrDefault(x => Convert.ToInt32(x.San_Pham_ID) == sp.Id)?.TongXuat ?? 0;

                return new BaoCaoXNT
                {
                    Ma_SP = sp.Ma_SP,
                    Ten_SP = sp.Ten_SP,
                    So_Luong_Dau_Ky = 0,
                    So_Luong_Nhap = tongNhap,
                    So_Luong_Xuat = tongXuat
                };
            }).ToList();

            return list;
        }
    }
}
