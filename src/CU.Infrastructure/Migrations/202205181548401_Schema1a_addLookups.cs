namespace CU.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Schema1a_addLookups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.xLookups2cKey",
                c => new
                    {
                        LookupTypeId = c.Short(nullable: false),
                        Code = c.String(nullable: false, maxLength: 2),
                        Name = c.String(nullable: false, maxLength: 100),
                        _SubType = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.LookupTypeId, t.Code })
                .Index(t => new { t.LookupTypeId, t.Name }, unique: true, name: "LookupTypeAndName");
            
            CreateTable(
                "dbo.xLookupTypes",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        TypeName = c.String(nullable: false, maxLength: 50),
                        BaseTypeName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo._coursesPresentationTypes",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        LookupTypeId = c.Short(nullable: false),
                        CoursePresentationTypeCode = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => new { t.CourseID, t.LookupTypeId, t.CoursePresentationTypeCode })
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.xLookups2cKey", t => new { t.LookupTypeId, t.CoursePresentationTypeCode }, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => new { t.LookupTypeId, t.CoursePresentationTypeCode });
            
            CreateTable(
                "dbo._departmentsFacilityTypes",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false),
                        LookupTypeId = c.Short(nullable: false),
                        DepartmentFacilityTypeCode = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => new { t.DepartmentID, t.LookupTypeId, t.DepartmentFacilityTypeCode })
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.xLookups2cKey", t => new { t.LookupTypeId, t.DepartmentFacilityTypeCode }, cascadeDelete: true)
                .Index(t => t.DepartmentID)
                .Index(t => new { t.LookupTypeId, t.DepartmentFacilityTypeCode });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo._departmentsFacilityTypes", new[] { "LookupTypeId", "DepartmentFacilityTypeCode" }, "dbo.xLookups2cKey");
            DropForeignKey("dbo._departmentsFacilityTypes", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo._coursesPresentationTypes", new[] { "LookupTypeId", "CoursePresentationTypeCode" }, "dbo.xLookups2cKey");
            DropForeignKey("dbo._coursesPresentationTypes", "CourseID", "dbo.Course");
            DropIndex("dbo._departmentsFacilityTypes", new[] { "LookupTypeId", "DepartmentFacilityTypeCode" });
            DropIndex("dbo._departmentsFacilityTypes", new[] { "DepartmentID" });
            DropIndex("dbo._coursesPresentationTypes", new[] { "LookupTypeId", "CoursePresentationTypeCode" });
            DropIndex("dbo._coursesPresentationTypes", new[] { "CourseID" });
            DropIndex("dbo.xLookups2cKey", "LookupTypeAndName");
            DropTable("dbo._departmentsFacilityTypes");
            DropTable("dbo._coursesPresentationTypes");
            DropTable("dbo.xLookupTypes");
            DropTable("dbo.xLookups2cKey");
        }
    }
}
