namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyHandsDbContext : DbContext
    {
        public MyHandsDbContext()
            : base("name=MyHandsDbContext")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tbl_BaiViet> tbl_BaiViet { get; set; }
        public virtual DbSet<tbl_ChiTietHoaDon> tbl_ChiTietHoaDon { get; set; }
        public virtual DbSet<tbl_ChuDe> tbl_ChuDe { get; set; }
        public virtual DbSet<tbl_DanhMucSanPham> tbl_DanhMucSanPham { get; set; }
        public virtual DbSet<tbl_Feedback> tbl_Feedback { get; set; }
        public virtual DbSet<tbl_Footer> tbl_Footer { get; set; }
        public virtual DbSet<tbl_GioiThieu> tbl_GioiThieu { get; set; }
        public virtual DbSet<tbl_HoaDon> tbl_HoaDon { get; set; }
        public virtual DbSet<tbl_LienHe> tbl_LienHe { get; set; }
        public virtual DbSet<tbl_Menu> tbl_Menu { get; set; }
        public virtual DbSet<tbl_MenuType> tbl_MenuType { get; set; }
        public virtual DbSet<tbl_SanPham> tbl_SanPham { get; set; }
        public virtual DbSet<tbl_Slice> tbl_Slice { get; set; }
        public virtual DbSet<tbl_SystemConfig> tbl_SystemConfig { get; set; }
        public virtual DbSet<tbl_Tag> tbl_Tag { get; set; }
        public virtual DbSet<tbl_TagBaiViet> tbl_TagBaiViet { get; set; }
        public virtual DbSet<tbl_TaiKhoan> tbl_TaiKhoan { get; set; }
        public virtual DbSet<tbl_Roles> tbl_Roles { get; set; }
        public virtual DbSet<tbl_Permission> tbl_Permissions { get; set; }
        public virtual DbSet<tbl_PhanQuyen> tbl_PhanQuyens { get; set; }
        public virtual DbSet<tbl_TrangThaiHoaDon> tbl_TrangThaiHoaDon { get; set; }
        public virtual DbSet<tbl_NCC> tbl_NCC { get; set; }
        public virtual DbSet<tbl_PhieuYeuCauNhap> tbl_PhieuYeuCauNhap { get; set; }
        public virtual DbSet<tbl_DonNhapHang> tbl_DonNhapHang { get; set; }
        public virtual DbSet<tbl_ChiTietPhieuYC> tbl_ChiTietPhieuYC { get; set; }
        public virtual DbSet<tbl_ChiTietDonNhapHang> tbl_ChiTietDonNhapHang { get; set; }
        public virtual DbSet<tbl_Notification> tbl_Notification { get; set; }
        public virtual DbSet<tbl_HinhAnhSP> tbl_HinhAnhSP { get; set; }
        public virtual DbSet<tbl_SanPhamComment> tbl_SanPhamComment { get; set; }
        public virtual DbSet<tbl_BaiVietComment> tbl_BaiVietComment { get; set; }
        public virtual DbSet<tbl_DanhMucMauSanPham> tbl_DanhMucMauSanPham { get; set; }
        public virtual DbSet<tbl_SPmauOption> tbl_SPmauOption { get; set; }
        public virtual DbSet<tbl_OptionSPMau> tbl_OptionSPMau { get; set; }
        public virtual DbSet<tbl_OptionValue> tbl_OptionValue { get; set; }
        public virtual DbSet<tbl_SanPhamMau> tbl_SanPhamMau { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_BaiViet>()
                .Property(e => e.sTenTieuDecMeta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_BaiViet>()
                .Property(e => e.sNguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_BaiViet>()
                .Property(e => e.sNguoiSua)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_ChiTietHoaDon>()
                .Property(e => e.dDonGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_ChuDe>()
                .Property(e => e.sTenChuDeMeta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_ChuDe>()
                .Property(e => e.sNguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_ChuDe>()
                .Property(e => e.sNguoiSua)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DanhMucSanPham>()
                .Property(e => e.sTenDanhMucMeta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DanhMucSanPham>()
                .Property(e => e.sNguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DanhMucSanPham>()
                .Property(e => e.sNguoiSua)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Footer>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_GioiThieu>()
                .Property(e => e.sTenGioiThieuMeta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_GioiThieu>()
                .Property(e => e.sNguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_GioiThieu>()
                .Property(e => e.sNguoiSua)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_HoaDon>()
                .Property(e => e.sSDTnguoiNhan)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_HoaDon>()
                .Property(e => e.sEmailNguoiNhan)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SanPham>()
                .Property(e => e.sMaSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SanPham>()
                .Property(e => e.sTenSanPhamMeta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SanPham>()
                .Property(e => e.dGiaBan)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_SanPham>()
                .Property(e => e.dGiaKhuyenMai)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_SanPham>()
                .Property(e => e.sNguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SanPham>()
                .Property(e => e.sNguoiSua)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Slice>()
                .Property(e => e.sNguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Slice>()
                .Property(e => e.sNguoiSua)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SystemConfig>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Tag>()
                .Property(e => e.TagID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_TagBaiViet>()
                .Property(e => e.TagID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_TaiKhoan>()
                .Property(e => e.sTenTaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_TaiKhoan>()
                .Property(e => e.sMatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_TaiKhoan>()
                .Property(e => e.sNguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_TaiKhoan>()
                .Property(e => e.sNguoiSua)
                .IsUnicode(false);
        }

        //public System.Data.Entity.DbSet<MyHandsDoAnTotNghiep.Models.LoginModel> LoginModels { get; set; }

        //public System.Data.Entity.DbSet<MyHandsDoAnTotNghiep.Models.ChangePasswordModel> ChangePasswordModels { get; set; }

        //public System.Data.Entity.DbSet<Model.ViewModel.PhieuYeuCauViewModel> PhieuYeuCauViewModels { get; set; }

        //public System.Data.Entity.DbSet<Model.ViewModel.HoaDonViewModel> HoaDonViewModels { get; set; }

        //public System.Data.Entity.DbSet<MyHandsDoAnTotNghiep.Models.ChangePasswordModel> ChangePasswordModels { get; set; }

        //public System.Data.Entity.DbSet<Model.ViewModel.HoaDonViewModel> HoaDonViewModels { get; set; }

        // public System.Data.Entity.DbSet<Model.ViewModel.ProductViewModel> ProductViewModels { get; set; }

        //public System.Data.Entity.DbSet<MyHandsDoAnTotNghiep.Models.LoginModel> LoginModels { get; set; }

        //public System.Data.Entity.DbSet<MyHandsDoAnTotNghiep.Models.RegisterModel> RegisterModels { get; set; }
    }
}
