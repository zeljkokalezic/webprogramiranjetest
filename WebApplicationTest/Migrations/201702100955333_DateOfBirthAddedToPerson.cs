namespace WebApplicationTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateOfBirthAddedToPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "DateOfBirth", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "DateOfBirth");
        }
    }
}
