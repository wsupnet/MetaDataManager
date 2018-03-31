namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAlbumNameColumnInSongModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "Album_Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "Album_Name");
        }
    }
}
