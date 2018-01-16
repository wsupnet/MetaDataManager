namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWebsiteToArtistModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "Website", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "Website");
        }
    }
}
