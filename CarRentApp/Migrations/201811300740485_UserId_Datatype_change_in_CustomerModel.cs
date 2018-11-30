namespace CarRentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserId_Datatype_change_in_CustomerModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "UserId", c => c.Int());
        }
    }
}
