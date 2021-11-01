namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_talent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TalentCards",
                c => new
                    {
                        TalentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        About = c.String(),
                        TalentName = c.String(),
                        Percent = c.String(),
                    })
                .PrimaryKey(t => t.TalentID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TalentCards");
        }
    }
}
