namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedArtistModel1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artists", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artists", "Name", c => c.String(nullable: false));
        }
    }
}
