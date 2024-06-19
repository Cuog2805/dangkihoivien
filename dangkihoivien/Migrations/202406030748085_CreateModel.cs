namespace dangkihoivien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuanTriViens",
                c => new
                    {
                        QuanTriVienId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuanTriVienId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.HoiViens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThongTinDoanhNghiepId = c.Int(nullable: false),
                        NguoiDaiDienId = c.Int(nullable: false),
                        NguoiLienHeId = c.Int(nullable: false),
                        QuanTriVienId = c.Int(),
                        DongYDieuKhoan = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDenided = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NguoiDaiDiens", t => t.NguoiDaiDienId, cascadeDelete: true)
                .ForeignKey("dbo.QuanTriViens", t => t.QuanTriVienId)
                .ForeignKey("dbo.NguoiLienHes", t => t.NguoiLienHeId, cascadeDelete: true)
                .ForeignKey("dbo.ThongTinDoanhNghieps", t => t.ThongTinDoanhNghiepId, cascadeDelete: true)
                .Index(t => t.ThongTinDoanhNghiepId)
                .Index(t => t.NguoiDaiDienId)
                .Index(t => t.NguoiLienHeId)
                .Index(t => t.QuanTriVienId);
            
            CreateTable(
                "dbo.NguoiDaiDiens",
                c => new
                    {
                        NguoiDaiDienId = c.Int(nullable: false, identity: true),
                        Hoten = c.String(nullable: false),
                        Chucvu = c.String(),
                        Ngaysinh = c.DateTime(nullable: false),
                        Sdt = c.String(nullable: false),
                        Zalo = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NguoiDaiDienId);
            
            CreateTable(
                "dbo.NguoiLienHes",
                c => new
                    {
                        NguoiLienHeId = c.Int(nullable: false, identity: true),
                        Hoten = c.String(nullable: false),
                        Chucvu = c.String(),
                        Ngaysinh = c.DateTime(nullable: false),
                        Sdt = c.String(nullable: false),
                        Zalo = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NguoiLienHeId);
            
            CreateTable(
                "dbo.ThongTinDoanhNghieps",
                c => new
                    {
                        ThongTinDoanhNghiepId = c.Int(nullable: false, identity: true),
                        Tendoanhnghiep = c.String(nullable: false),
                        Masothue = c.String(nullable: false),
                        Diachi = c.String(nullable: false),
                        Dienthoai = c.String(nullable: false),
                        Zalo = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.ThongTinDoanhNghiepId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HoiViens", "ThongTinDoanhNghiepId", "dbo.ThongTinDoanhNghieps");
            DropForeignKey("dbo.HoiViens", "NguoiLienHeId", "dbo.NguoiLienHes");
            DropForeignKey("dbo.HoiViens", "QuanTriVienId", "dbo.QuanTriViens");
            DropForeignKey("dbo.HoiViens", "NguoiDaiDienId", "dbo.NguoiDaiDiens");
            DropForeignKey("dbo.QuanTriViens", "RoleId", "dbo.Roles");
            DropIndex("dbo.HoiViens", new[] { "QuanTriVienId" });
            DropIndex("dbo.HoiViens", new[] { "NguoiLienHeId" });
            DropIndex("dbo.HoiViens", new[] { "NguoiDaiDienId" });
            DropIndex("dbo.HoiViens", new[] { "ThongTinDoanhNghiepId" });
            DropIndex("dbo.QuanTriViens", new[] { "RoleId" });
            DropTable("dbo.ThongTinDoanhNghieps");
            DropTable("dbo.NguoiLienHes");
            DropTable("dbo.NguoiDaiDiens");
            DropTable("dbo.HoiViens");
            DropTable("dbo.Roles");
            DropTable("dbo.QuanTriViens");
        }
    }
}
