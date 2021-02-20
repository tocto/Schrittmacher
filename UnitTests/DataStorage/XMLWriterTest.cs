using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phileas.DataStorage;
using Phileas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestFactories;

namespace UnitTests.DataStorage
{
    [TestClass]
    public class XMLWriterTest
    {
        [TestMethod]
        public async Task WriteAsync()
        {
            Simulation simulation = SimulationTestFactory.Make();

            var file = await XMLWriter.Write(simulation);

            Assert.IsTrue(file.Name.StartsWith(simulation.Name) && file.Name.EndsWith(Phileas.App.SimulationFileExtension), "Exported file should have the title of the simulation as name, if there are  no conflicts. However existing files with the same name should not be replaced, but a unique name should be generated.");
        }

        [TestMethod]
        public async Task WriteAsync_NoDuplicates()
        {
            Simulation simulation = SimulationTestFactory.Make();

            var file = await XMLWriter.Write(simulation);
            var file2 = await XMLWriter.Write(simulation);

            Assert.AreEqual(file.Path, file2.Path, "Saving the same simulation should overwrite the file and not create duplicates.");
        }

        [TestMethod]
        public async Task WriteAsync_Exception()
        {
            // null
            Simulation simulation = null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await XMLWriter.Write(simulation), "Simulation instace must not be a null refernce");

            // no name
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await XMLWriter.Write(new Simulation()), "Simulation must have a name to be saved");
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await XMLWriter.Write(new Simulation() { Name = " " }), "Simulation must have a non empty name to be saved");
        }
    }
}
