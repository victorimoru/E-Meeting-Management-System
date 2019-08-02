namespace E_MeetingMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCommentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Content = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Document_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Documents", t => t.Document_ID)
                .Index(t => t.Document_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Document_ID", "dbo.Documents");
            DropIndex("dbo.Comments", new[] { "Document_ID" });
            DropTable("dbo.Comments");
        }
    }
}
