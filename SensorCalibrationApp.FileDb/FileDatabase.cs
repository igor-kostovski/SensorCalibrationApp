using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.FileDb
{
    public class FileDatabase
    {
        private const string filePath = "database.txt";
        public Stream Connection { get; set; }
        public List<EcuModel> Collection { get;set; }

        public FileDatabase()
        {
            Collection = new List<EcuModel>();
        }

        public Task Save()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<EcuModel>));

            return Task.Run(() =>
            {
                OpenFor(FileAccess.Write);
                xmlFormat.Serialize(Connection, Collection);

                Connection.Close();
            });
        }

        public Task Load()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<EcuModel>));

            return Task.Run(async () =>
            {
                await SeedDbIfEmpty();

                OpenFor(FileAccess.Read);
                Collection = (List<EcuModel>) xmlFormat.Deserialize(Connection);

                Connection.Close();
            });
        }

        private async Task SeedDbIfEmpty()
        {
            OpenFor(FileAccess.Read);
            if (Connection.Length == 0)
                await SeedDb();
        }

        private async Task SeedDb()
        {
            Collection = Seeder.DataCollection;
            await Save();
        }

        private void OpenFor(FileAccess action)
        {
            if (Connection != null)
                Connection.Close();

            Connection = new FileStream(filePath, FileMode.OpenOrCreate, action);
        }
    }
}
