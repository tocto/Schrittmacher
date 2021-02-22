using Schrittmacher.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.Xml;

namespace Schrittmacher.DataStorage
{
    public class XMLReader
    {
        public static async Task<Simulation> ReadAsync(StorageFile file)
        {
            if (file == null) throw new ArgumentNullException();

            Simulation simulation = null;

            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Simulation));

            using (var stream = await file.OpenStreamForReadAsync())
            {
                simulation = (Simulation)reader.Deserialize(stream);
            }

            return simulation;
        }
    }
}
