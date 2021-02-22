using Schrittmacher.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;

namespace Schrittmacher.DataStorage
{
    public class XMLWriter
    {
        public static async Task<StorageFile> Write(Simulation simulation, StorageFile file = null)
        {
            if (simulation == null) throw new ArgumentNullException();
            if (simulation.Name == null || simulation.Name.Trim().Equals(string.Empty)) throw new ArgumentException("No name given.");

            file = await PrepareFileAsync(simulation, file);

            simulation.Path = file.Path; //update data storage related properties, keep that in main thread

            await WriteToXMLAsync(simulation, file);

            return file;
        }

        private static async Task WriteToXMLAsync(Simulation simulation, StorageFile file)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Simulation));

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                writer.Serialize(stream, simulation);
            }
        }

        private static async Task<StorageFile> PrepareFileAsync(Simulation simulation, StorageFile file)
        {
            if (file == null)
            {
                if (simulation.Path != string.Empty)
                {
                    file = await StorageFile.GetFileFromPathAsync(simulation.Path);
                    await file.DeleteAsync();
                }

                StorageFolder simulationFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(App.SimulationsFolderName, CreationCollisionOption.OpenIfExists);
                file = await simulationFolder.CreateFileAsync(simulation.Name + App.SimulationFileExtension, CreationCollisionOption.GenerateUniqueName);
            }
            else if (!file.Name.EndsWith(App.SimulationFileExtension))
            {
                await file.RenameAsync(file.Name + App.SimulationFileExtension);
            }

            return file;
        }
    }
}
