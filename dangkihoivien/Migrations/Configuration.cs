namespace dangkihoivien.Migrations
{
    using dangkihoivien.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<dangkihoivien.Models.DangKiHoiVienDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(dangkihoivien.Models.DangKiHoiVienDBContext context)
        {
            context.Roles.AddOrUpdate(
                r => r.Name,
                new Role { RoleId = 1, Name = "Viewer" },
                new Role { RoleId = 2, Name = "Manager" }
            );
        }
    }
}
