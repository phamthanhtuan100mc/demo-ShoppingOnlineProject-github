namespace OnlineShoppingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrdersDetails",
                c => new
                    {
                        MaDonHang = c.String(nullable: false, maxLength: 128),
                        SoLuong = c.Int(nullable: false),
                        DonGia = c.Double(nullable: false),
                        orderId = c.String(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.MaDonHang)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdersDetails", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrdersDetails", new[] { "Order_Id" });
            DropTable("dbo.OrdersDetails");
        }
    }
}
