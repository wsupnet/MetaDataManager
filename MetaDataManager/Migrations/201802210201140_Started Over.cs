namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartedOver : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Songs", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Songs", new[] { "AlbumId" });
            AddColumn("dbo.Songs", "Album", c => c.String());
            DropColumn("dbo.Songs", "AlbumName");
            DropColumn("dbo.Songs", "AlbumId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "AlbumId", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "AlbumName", c => c.String());
            DropColumn("dbo.Songs", "Album");
            CreateIndex("dbo.Songs", "AlbumId");
            AddForeignKey("dbo.Songs", "AlbumId", "dbo.Albums", "Id", cascadeDelete: true);
        }
    }
}
