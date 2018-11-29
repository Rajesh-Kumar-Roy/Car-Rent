namespace CarRentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RentAssign_Model_VehicleType_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentAssigns", "VehicleTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.RentAssigns", "VehicleTypeId");
            AddForeignKey("dbo.RentAssigns", "VehicleTypeId", "dbo.VehicleTypes", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RentAssigns", "VehicleTypeId", "dbo.VehicleTypes");
            DropIndex("dbo.RentAssigns", new[] { "VehicleTypeId" });
            DropColumn("dbo.RentAssigns", "VehicleTypeId");
        }
    }
}
