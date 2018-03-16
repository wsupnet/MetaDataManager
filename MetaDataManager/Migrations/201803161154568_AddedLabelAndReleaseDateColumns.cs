namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLabelAndReleaseDateColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Label", c => c.String());
            AddColumn("dbo.Albums", "Release_Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Release_Date");
            DropColumn("dbo.Albums", "Label");
        }
    }
}
