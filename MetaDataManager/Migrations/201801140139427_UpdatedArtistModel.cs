namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedArtistModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Artist_Id", c => c.Int());
            CreateIndex("dbo.Albums", "Artist_Id");
            AddForeignKey("dbo.Albums", "Artist_Id", "dbo.Artists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.Albums", new[] { "Artist_Id" });
            DropColumn("dbo.Albums", "Artist_Id");
        }
    }
}
