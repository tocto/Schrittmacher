using Phileas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;

namespace Phileas.DataStorage
{
    public class XMLWriter
    {
        public static async Task<StorageFile> Write(Simulation simulation)
        {
            if (simulation == null) throw new ArgumentNullException();
            if (simulation.Title == null || simulation.Title.Trim().Equals(string.Empty)) throw new ArgumentException("No name given.");

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Simulation));

            StorageFile file = await DownloadsFolder.CreateFileAsync(simulation.Title + ".xml", CreationCollisionOption.GenerateUniqueName);

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                writer.Serialize(stream, simulation);
            }

            return file;
        }
    }
}
