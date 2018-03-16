namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnForAlbumArt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Image");
        }
    }
}
