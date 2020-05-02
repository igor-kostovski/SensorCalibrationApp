using SensorCalibrationApp.EntityFramework.Data;

namespace SensorCalibrationApp.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override async void Seed(DataContext context)
        {
            var seeder = new Seeder(context);
            await seeder.Seed();
        }
    }
}
