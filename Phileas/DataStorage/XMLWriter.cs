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
        public static async Task<StorageFile> Write(Simulation simulation, StorageFile file = null)
        {
            if (simulation == null) throw new ArgumentNullException();
            if (simulation.Name == null || simulation.Name.Trim().Equals(string.Empty)) throw new ArgumentException("No name given.");

            if (file == null)
            {
                StorageFolder simulationFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(App.SimulationsFolderName, CreationCollisionOption.OpenIfExists);
                file = await simulationFolder.CreateFileAsync(simulation.Name + App.SimulationFileExtension, CreationCollisionOption.GenerateUniqueName);
            }
            else if (!file.Name.EndsWith(App.SimulationFileExtension))
            {
                await file.RenameAsync(file.Name + App.SimulationFileExtension);
            }

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Simulation));

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                writer.Serialize(stream, simulation);
            }

            return file;
        }
    }
}
