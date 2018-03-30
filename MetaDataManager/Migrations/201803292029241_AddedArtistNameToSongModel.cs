namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArtistNameToSongModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "Artist_Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "Artist_Name");
        }
    }
}
