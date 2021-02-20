using Phileas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.Xml;

namespace Phileas.DataStorage
{
    public class XMLReader
    {
        public Simulation Read(StorageFile file)
        {
            Simulation simulation = null;

            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Simulation));

            using (StreamReader stream = new StreamReader(file.Path))
            {
                simulation = (Simulation)reader.Deserialize(stream);
            }

            return simulation;
        }
    }
}
