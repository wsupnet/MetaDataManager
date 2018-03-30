namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnsToSongModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "Track_Number", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "Duration", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "Preview_Url", c => c.String());
            AddColumn("dbo.Songs", "Disc_number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "Disc_number");
            DropColumn("dbo.Songs", "Preview_Url");
            DropColumn("dbo.Songs", "Duration");
            DropColumn("dbo.Songs", "Track_Number");
        }
    }
}
