namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageColumnToArtist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "Image");
        }
    }
}
