namespace E_MeetingMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "NewMeetingFormViewModel_ID", "dbo.NewMeetingFormViewModels");
            DropIndex("dbo.AspNetUsers", new[] { "NewMeetingFormViewModel_ID" });
            DropColumn("dbo.AspNetUsers", "NewMeetingFormViewModel_ID");
            DropTable("dbo.NewMeetingFormViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NewMeetingFormViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Venue = c.String(nullable: false),
                        Date = c.String(nullable: false),
                        Time = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AspNetUsers", "NewMeetingFormViewModel_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "NewMeetingFormViewModel_ID");
            AddForeignKey("dbo.AspNetUsers", "NewMeetingFormViewModel_ID", "dbo.NewMeetingFormViewModels", "ID");
        }
    }
}
