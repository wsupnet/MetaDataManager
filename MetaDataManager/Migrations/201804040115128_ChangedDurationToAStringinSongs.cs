namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDurationToAStringinSongs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Songs", "Duration", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Songs", "Duration", c => c.Int(nullable: false));
        }
    }
}
