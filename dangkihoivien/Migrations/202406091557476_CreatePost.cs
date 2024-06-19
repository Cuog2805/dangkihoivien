namespace dangkihoivien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        HoiVienId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HoiViens", t => t.HoiVienId, cascadeDelete: true)
                .Index(t => t.HoiVienId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "HoiVienId", "dbo.HoiViens");
            DropIndex("dbo.Posts", new[] { "HoiVienId" });
            DropTable("dbo.Posts");
        }
    }
}
