namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Songs", "Artist");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "Artist", c => c.String());
        }
    }
}
