namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSongModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "Name", c => c.String());
            DropColumn("dbo.Songs", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Songs", "Name");
        }
    }
}
