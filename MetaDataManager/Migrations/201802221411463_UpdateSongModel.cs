namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSongModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Songs", "AlbumName");
            DropColumn("dbo.Songs", "Year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "AlbumName", c => c.String());
        }
    }
}
