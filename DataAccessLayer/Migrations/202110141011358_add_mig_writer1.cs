namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_mig_writer1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Writers", "WriterPasswordHash", c => c.Binary());
            AddColumn("dbo.Writers", "WriterPasswordSalt", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Writers", "WriterPasswordSalt");
            DropColumn("dbo.Writers", "WriterPasswordHash");
        }
    }
}
