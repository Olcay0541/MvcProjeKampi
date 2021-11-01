namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_admin1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "AdminName", c => c.String());
            AddColumn("dbo.Admins", "AdminStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "AdminStatus");
            DropColumn("dbo.Admins", "AdminName");
        }
    }
}
