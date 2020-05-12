namespace SensorCalibrationApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBytesToFrame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Frames", "Bytes", c => c.Binary(maxLength: 8));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Frames", "Bytes");
        }
    }
}
