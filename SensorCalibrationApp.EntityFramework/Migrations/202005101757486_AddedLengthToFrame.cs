namespace SensorCalibrationApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLengthToFrame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Frames", "Length", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Frames", "Length");
        }
    }
}
