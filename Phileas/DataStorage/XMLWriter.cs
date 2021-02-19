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
        public static async Task<StorageFile> Write(PlotData simulation)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(PlotData));

            StorageFile file = await DownloadsFolder.CreateFileAsync(simulation.Title + ".xml", Windows.Storage.CreationCollisionOption.GenerateUniqueName);

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                writer.Serialize(stream, simulation);
            }

            return file;
        }
    }
}
