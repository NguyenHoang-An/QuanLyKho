using Client.Model;

namespace Client.Server
{
    public interface INhapKhoService
    {
        // ===== Lấy danh sách =====
        Task<List<NhapKho>> GetAllAsync();
        Task<List<Kho>> GetAllKhosAsync();
        Task<List<NhaCungCap>> GetAllNCCsAsync();
        Task<List<NhapKhoRawData>> GetDetailsAsync(int nhapKhoId);
        Task<List<NhapKhoChiTietModel>> GetChiTietModelsAsync(int nhapKhoId);

        // ===== Kiểm tra trùng số phiếu nhập =====
        Task<bool> IsSoPhieuNhapUniqueAsync(string soPhieuNhap);
        Task<bool> IsSoPhieuNhapUniqueExceptIdAsync(string soPhieu, int excludeId);

        // ===== Tạo phiếu nhập =====
        Task<int> CreateOriginalAsync(NhapKho model, List<NhapKhoRawData> chiTiet);

        // ===== Chi tiết nhập =====
        Task AddDetailAsync(int nhapKhoId, NhapKhoChiTietModel detail);
        Task UpdateDetailAsync(int nhapKhoId, NhapKhoChiTietModel detail);
        Task DeleteDetailAsync(int nhapKhoId, int sanPhamId);
        Task AddChiTietNhapAsync(NhapKhoRawData item);
        Task DeleteChiTietNhapAsync(int id);

        // ===== Truy xuất theo ID hoặc Số phiếu =====
        Task<Kho?> GetKhoByIdAsync(int id);
        Task<NhaCungCap?> GetNCCByIdAsync(int id);
        Task<SanPham?> GetProductByMaSPAsync(string maSP);
        Task<int?> GetOriginalNhapKhoIdBySoPhieuAsync(string soPhieu);
        Task<HieuChinhThongTinPhieuNhap?> GetHeaderAsync(string soPhieu);
        Task<HieuChinhThongTinPhieuNhap?> GetHeaderAsync(int id);
        Task<HieuChinhThongTinPhieuNhap?> GetHeaderAsyncById(int id);
        Task<NhapKho?> GetByIdAsync(int id);

        // ===== Cập nhật và xoá phiếu nhập =====
        Task UpdateHeaderAsync(HieuChinhThongTinPhieuNhap model);
        Task DeleteAsync(int id);
    }
}
