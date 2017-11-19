namespace Calendar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBV1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "Person_PersonId", "dbo.People");
            DropPrimaryKey("dbo.Attendances");
            DropPrimaryKey("dbo.People");
            AlterColumn("dbo.Attendances", "AttendanceId", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.People", "PersonId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Attendances", "AttendanceId");
            AddPrimaryKey("dbo.People", "PersonId");
            AddForeignKey("dbo.Attendances", "Person_PersonId", "dbo.People", "PersonId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "Person_PersonId", "dbo.People");
            DropPrimaryKey("dbo.People");
            DropPrimaryKey("dbo.Attendances");
            AlterColumn("dbo.People", "PersonId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Attendances", "AttendanceId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.People", "PersonId");
            AddPrimaryKey("dbo.Attendances", "AttendanceId");
            AddForeignKey("dbo.Attendances", "Person_PersonId", "dbo.People", "PersonId");
        }
    }
}
