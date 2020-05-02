namespace SensorCalibrationApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ecus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Frames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FrameId = c.Byte(nullable: false),
                        Direction = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Signals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EcuDevices",
                c => new
                    {
                        EcuId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EcuId, t.DeviceId })
                .ForeignKey("dbo.Ecus", t => t.EcuId, cascadeDelete: true)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.EcuId)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.FrameSignals",
                c => new
                    {
                        Frame_Id = c.Int(nullable: false),
                        SignalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Frame_Id, t.SignalId })
                .ForeignKey("dbo.Frames", t => t.Frame_Id, cascadeDelete: true)
                .ForeignKey("dbo.Signals", t => t.SignalId, cascadeDelete: true)
                .Index(t => t.Frame_Id)
                .Index(t => t.SignalId);
            
            CreateTable(
                "dbo.DeviceFrames",
                c => new
                    {
                        DeviceId = c.Int(nullable: false),
                        Frame_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeviceId, t.Frame_Id })
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.Frames", t => t.Frame_Id, cascadeDelete: true)
                .Index(t => t.DeviceId)
                .Index(t => t.Frame_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceFrames", "Frame_Id", "dbo.Frames");
            DropForeignKey("dbo.DeviceFrames", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.FrameSignals", "SignalId", "dbo.Signals");
            DropForeignKey("dbo.FrameSignals", "Frame_Id", "dbo.Frames");
            DropForeignKey("dbo.EcuDevices", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.EcuDevices", "EcuId", "dbo.Ecus");
            DropIndex("dbo.DeviceFrames", new[] { "Frame_Id" });
            DropIndex("dbo.DeviceFrames", new[] { "DeviceId" });
            DropIndex("dbo.FrameSignals", new[] { "SignalId" });
            DropIndex("dbo.FrameSignals", new[] { "Frame_Id" });
            DropIndex("dbo.EcuDevices", new[] { "DeviceId" });
            DropIndex("dbo.EcuDevices", new[] { "EcuId" });
            DropTable("dbo.DeviceFrames");
            DropTable("dbo.FrameSignals");
            DropTable("dbo.EcuDevices");
            DropTable("dbo.Signals");
            DropTable("dbo.Frames");
            DropTable("dbo.Ecus");
            DropTable("dbo.Devices");
        }
    }
}
