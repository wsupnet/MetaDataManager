namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArtist_NameToAlbumTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Artist_Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Artist_Name");
        }
    }
}
