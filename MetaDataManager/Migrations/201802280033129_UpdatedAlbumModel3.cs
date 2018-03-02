namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAlbumModel3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Albums", new[] { "ArtistId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Albums", "ArtistId");
            AddForeignKey("dbo.Albums", "ArtistId", "dbo.Artists", "Id", cascadeDelete: true);
        }
    }
}
