namespace CarRentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserId_and_IsDeleted_property_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "UserId", c => c.Int());
            AddColumn("dbo.Customers", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.RentRequests", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.VehicleTypes", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.RentAssigns", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentAssigns", "Comment");
            DropColumn("dbo.VehicleTypes", "IsDelete");
            DropColumn("dbo.RentRequests", "IsDelete");
            DropColumn("dbo.Customers", "IsDelete");
            DropColumn("dbo.Customers", "UserId");
        }
    }
}
