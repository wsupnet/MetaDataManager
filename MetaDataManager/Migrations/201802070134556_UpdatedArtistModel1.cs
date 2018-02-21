namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedArtistModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "Description");
        }
    }
}
