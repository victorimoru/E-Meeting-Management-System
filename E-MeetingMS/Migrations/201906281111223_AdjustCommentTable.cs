namespace E_MeetingMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustCommentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "IsCancelled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "IsCancelled");
        }
    }
}
