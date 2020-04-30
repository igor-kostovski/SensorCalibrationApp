using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SensorCalibrationApp.Domain.Models;

namespace SensorCalibrationApp.FileDb
{
    public partial class FileDatabase
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

            return Task.Run(() =>
            {
                OpenFor(FileAccess.Read);
                Collection = (List<EcuModel>) xmlFormat.Deserialize(Connection);

                Connection.Close();
            });
        }

        private async Task SeedDb(List<EcuModel> seed)
        {
            OpenFor(FileAccess.Read);
            if (Connection.Length != 0)
                return;

            Collection = seed;
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
