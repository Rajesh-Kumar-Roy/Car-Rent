namespace CarRentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Car_Rent_Model_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                        ContactNo = c.String(nullable: false, maxLength: 14),
                        Address = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 100),
                        Details = c.String(),
                        NotificatinDateTime = c.DateTime(nullable: false),
                        RentRequestId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.RentRequests", t => t.RentRequestId, cascadeDelete: false)
                .Index(t => t.RentRequestId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.RentRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromPlace = c.String(nullable: false, maxLength: 500),
                        ToPlace = c.String(nullable: false, maxLength: 500),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        AirCondition = c.String(nullable: false),
                        VehicleQty = c.Int(nullable: false),
                        Description = c.String(),
                        CustomerId = c.Int(nullable: false),
                        VehicleTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeId, cascadeDelete: false)
                .Index(t => t.CustomerId)
                .Index(t => t.VehicleTypeId);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RentAssigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RentPrice = c.Double(nullable: false),
                        Status = c.String(nullable: false, maxLength: 100),
                        RentAssignDateTime = c.DateTime(nullable: false),
                        RentRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RentRequests", t => t.RentRequestId, cascadeDelete: false)
                .Index(t => t.RentRequestId);
            
            CreateTable(
                "dbo.RentRequestHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Status = c.String(nullable: false, maxLength: 100),
                        HistoryDateTime = c.DateTime(nullable: false),
                        RentRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RentRequests", t => t.RentRequestId, cascadeDelete: false)
                .Index(t => t.RentRequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RentRequestHistories", "RentRequestId", "dbo.RentRequests");
            DropForeignKey("dbo.RentAssigns", "RentRequestId", "dbo.RentRequests");
            DropForeignKey("dbo.Notifications", "RentRequestId", "dbo.RentRequests");
            DropForeignKey("dbo.RentRequests", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.RentRequests", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Notifications", "CustomerId", "dbo.Customers");
            DropIndex("dbo.RentRequestHistories", new[] { "RentRequestId" });
            DropIndex("dbo.RentAssigns", new[] { "RentRequestId" });
            DropIndex("dbo.RentRequests", new[] { "VehicleTypeId" });
            DropIndex("dbo.RentRequests", new[] { "CustomerId" });
            DropIndex("dbo.Notifications", new[] { "CustomerId" });
            DropIndex("dbo.Notifications", new[] { "RentRequestId" });
            DropTable("dbo.RentRequestHistories");
            DropTable("dbo.RentAssigns");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.RentRequests");
            DropTable("dbo.Notifications");
            DropTable("dbo.Customers");
        }
    }
}
