namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAlbumModel1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Albums", "Artist");
            DropColumn("dbo.Albums", "Genre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "Genre", c => c.String());
            AddColumn("dbo.Albums", "Artist", c => c.String());
        }
    }
}
