namespace E_MeetingMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewAdminUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'bcac2efe-b638-47c1-bc95-eef41d45b453', N'admin1@gmail.com', 0, N'AK/6zD8hGUFyCOI3c/CkbVbsZQNG61gBzOZcJ6MWfZ6Chxurwe90thR8kJEQLCwFxQ==', N'649104d9-377f-4dfe-9680-31fca6dd0f51', NULL, 0, 0, NULL, 1, 0, N'admin1@gmail.com')");
            Sql(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bcac2efe-b638-47c1-bc95-eef41d45b453', N'40e0ce4e-fca6-4aff-9657-5dbd224f6a44')");
        }
        
        public override void Down()
        {
        }
    }
}
