namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.AlbumViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AlbumViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                        Tracks = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                        Artist = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
