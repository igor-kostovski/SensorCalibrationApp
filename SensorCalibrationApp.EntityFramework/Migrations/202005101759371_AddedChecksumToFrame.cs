namespace SensorCalibrationApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedChecksumToFrame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Frames", "Checksum", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Frames", "Checksum");
        }
    }
}
