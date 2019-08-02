namespace E_MeetingMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustCommentTable2 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'003cffa0-e034-4f25-a320-30315a5662c9', N'Ordinary')");
        }
        
        public override void Down()
        {
        }
    }
}
