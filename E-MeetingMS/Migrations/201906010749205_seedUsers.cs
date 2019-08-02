namespace E_MeetingMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6ce2c3f5-d75a-45d6-a961-27420cb075b4', N'superuser@admin.com', 0, N'ANKhZZlZ451/nnoabn9/w3TcDgRhPyL5y1pS9aMvOdpKr6g3NJvhTxfGFTZ0S+euFQ==', N'dd751f43-55a1-4250-8370-0958101577fa', NULL, 0, 0, NULL, 1, 0, N'superuser@admin.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ca6b09f0-6ec6-4d60-b10f-65da10156ad3', N'guest@gmail.com', 0, N'ADF3KL15h8l4tRLXJ44Ggq7NxpzmKSNG04JYgw1RPLWvxIplO3M9uLtRhwcl8IvSEw==', N'e0355166-b723-4aea-94c4-695b8955c32d', NULL, 0, 0, NULL, 1, 0, N'guest@gmail.com')
            
           INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'40e0ce4e-fca6-4aff-9657-5dbd224f6a44', N'SuperAdmin')

           INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES ('6ce2c3f5-d75a-45d6-a961-27420cb075b4', '40e0ce4e-fca6-4aff-9657-5dbd224f6a44')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
