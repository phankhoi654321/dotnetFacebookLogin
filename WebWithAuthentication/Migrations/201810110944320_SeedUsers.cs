namespace WebWithAuthentication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers]
VALUES
( N'5a223835-d9e6-4816-b1de-8a15c3c55d29', N'admin@admin.com', 0, N'AMnahe9T/ZUT3WcNItNx9LoCZyHQXHxz5sfO0WSY703HPTJbQ5GESo9ZbnswshD7eQ==', N'74709bd5-482d-49a2-8af1-47fde3ca87b0', NULL, 0, 0, NULL, 1, 0, N'admin@admin.com' ), 
( N'c6796c55-1664-43f3-af8b-c8353b120f5d', N'maikhanh@gmail.com', 0, N'AG1C4n4pq4UzwlapzGiktQI1s8fFjG9u7pY+9BFnko4ESMptBovncAbw1l98kOrn/g==', N'd087e362-eb81-4ec9-8280-591f5edf0c7b', NULL, 0, 0, NULL, 1, 0, N'maikhanh@gmail.com' )
");
            Sql(@"INSERT INTO [dbo].[AspNetRoles] VALUES ( N'09087637-64b0-443e-9851-e47530850199', N'CanManageMovies' )");
            Sql(@"INSERT INTO [dbo].[AspNetUserRoles] VALUES ( N'5a223835-d9e6-4816-b1de-8a15c3c55d29', N'09087637-64b0-443e-9851-e47530850199' )
");

        }
        
        public override void Down()
        {
            Sql(@"DELETE FROM [dbo].[AspNetUsers] WHERE Email IN (N'admin@admin.com', N'maikhanh@gmail.com') ");
            Sql(@"DELETE FROM [dbo].[AspNetRoles] WHERE Name = N'CanManageMovies' ");
        }
    }
}
