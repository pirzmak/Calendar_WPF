namespace Calendar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppoitmentVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Version", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Version");
        }
    }
}
