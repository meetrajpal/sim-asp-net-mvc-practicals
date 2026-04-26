namespace Test2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Test2.Designation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Designation = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Test2.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50, unicode: false),
                        MiddleName = c.String(maxLength: 50, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 50, unicode: false),
                        DOB = c.DateTime(nullable: false, storeType: "date"),
                        MobileNumber = c.String(nullable: false, maxLength: 10, unicode: false),
                        Address = c.String(maxLength: 100, unicode: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DesignationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Test2.Designation", t => t.DesignationId)
                .Index(t => t.DesignationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Test2.Employee", "DesignationId", "Test2.Designation");
            DropIndex("Test2.Employee", new[] { "DesignationId" });
            DropTable("Test2.Employee");
            DropTable("Test2.Designation");
        }
    }
}
