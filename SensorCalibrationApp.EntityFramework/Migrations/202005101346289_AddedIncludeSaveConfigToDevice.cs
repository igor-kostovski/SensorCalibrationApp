namespace SensorCalibrationApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIncludeSaveConfigToDevice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "IncludeSaveConfig", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "IncludeSaveConfig");
        }
    }
}
