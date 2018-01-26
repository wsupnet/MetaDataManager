namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAlbumModel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Albums", "ArtistId");
            AddForeignKey("dbo.Albums", "ArtistId", "dbo.Artists", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Albums", new[] { "ArtistId" });
        }
    }
}
