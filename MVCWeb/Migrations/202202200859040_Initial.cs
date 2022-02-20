namespace MVCWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Title", c => c.String());
            AlterColumn("dbo.Movies", "Genre", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Genre", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Movies", "Title", c => c.String(maxLength: 60));
        }
    }
}
