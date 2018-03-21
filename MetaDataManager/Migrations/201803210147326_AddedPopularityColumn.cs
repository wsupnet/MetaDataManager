namespace MetaDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPopularityColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "Popularity", c => c.Int(nullable: false));
            DropColumn("dbo.Artists", "YearFormed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artists", "YearFormed", c => c.Int(nullable: false));
            DropColumn("dbo.Artists", "Popularity");
        }
    }
}
