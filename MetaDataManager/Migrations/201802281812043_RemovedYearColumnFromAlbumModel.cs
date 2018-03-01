namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedYearColumnFromAlbumModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Albums", "Year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "Year", c => c.Int(nullable: false));
        }
    }
}
