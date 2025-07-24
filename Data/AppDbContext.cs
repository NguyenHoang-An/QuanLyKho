using Client.Components.Pages;
using Client.Model;
using Microsoft.EntityFrameworkCore;

namespace Client.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<DonViTinh> DonViTinhs => Set<DonViTinh>();
        public DbSet<LoaiSanPham> LoaiSanPhams => Set<LoaiSanPham>();
        public DbSet<SanPham> SanPhams => Set<SanPham>();
        public DbSet<NhaCungCap> NhaCungCaps => Set<NhaCungCap>();
        public DbSet<Kho> Khos => Set<Kho>();
        public DbSet<KhoUser> KhoUsers => Set<KhoUser>();
        public DbSet<NhapKho> NhapKhos { get; set; }
        public DbSet<NhapKhoRawData> NhapKhoRawDatas { get; set; }
        public DbSet<HieuChinhThongTinPhieuNhap> HieuChinhThongTinPhieuNhaps { get; set; }
        public DbSet<XuatKhoHeader> XuatKho { get; set; }
        public DbSet<XuatKhoDetail> XuatKhoRaw { get; set; }
        public DbSet<XuatKhoEdit> XuatKhoEdit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonViTinh>()
                .HasIndex(x => x.Ten_DonViTinh)
                .IsUnique();

            modelBuilder.Entity<LoaiSanPham>()
                .HasIndex(x => x.Ma_LSP)
                .IsUnique();

            modelBuilder.Entity<LoaiSanPham>()
                .HasIndex(x => x.Ten_LSP)
                .IsUnique();

            modelBuilder.Entity<SanPham>()
                .HasIndex(x => x.Ma_SP)
                .IsUnique();

            modelBuilder.Entity<NhaCungCap>()
                .HasIndex(x => x.Ten_NCC)
                .IsUnique();

            modelBuilder.Entity<Kho>()
                .HasIndex(x => x.Ten_Kho)
                .IsUnique();

            modelBuilder.Entity<KhoUser>()
                .HasIndex(x => new { x.Ma_Dang_Nhap, x.Kho_ID })
                .IsUnique();

            modelBuilder.Entity<NhapKho>()
                .HasIndex(x => x.So_Phieu_Nhap_Kho)
                .IsUnique();

            modelBuilder.Entity<NhapKhoRawData>()
                .HasOne(x => x.NhapKho)
                .WithMany(nk => nk.NhapKhoRawDatas)
                .HasForeignKey(x => x.Nhap_Kho_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NhapKhoRawData>()
                .HasOne(x => x.SanPham)
                .WithMany()
                .HasForeignKey(x => x.San_Pham_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HieuChinhThongTinPhieuNhap>()
                .HasOne(h => h.Kho)
                .WithMany()
                .HasForeignKey(h => h.Kho_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HieuChinhThongTinPhieuNhap>()
                .HasOne(h => h.NhaCungCap)
                .WithMany()
                .HasForeignKey(h => h.NCC_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<XuatKhoHeader>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<XuatKhoHeader>()
                .HasIndex(x => x.So_Phieu_Xuat_Kho)
                .IsUnique();

            modelBuilder.Entity<XuatKhoHeader>()
                .HasMany(x => x.ChiTiet)
                .WithOne(c => c.PhieuXuat)
                .HasForeignKey(c => c.Xuat_Kho_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<XuatKhoEdit>()
                .HasKey(x => x.Id);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(decimal)))
                {
                    property.SetPrecision(18);
                    property.SetScale(2);
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
