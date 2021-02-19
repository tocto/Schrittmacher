using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phileas.DataStorage;
using Phileas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.DataStorage
{
    [TestClass]
    public class XMLWriterTest
    {
        [TestMethod]
        public async Task Write_MME()
        {
            var id = "XMLWriterTest";

            PlotData mm = new PlotData()
            {
                Title = id,
                XAxisTitle = "x-Axis",
                YAxisTitle = "y-Axis",
                XParameterKey = "x",
                YParameterKey = "y",
                NumberOfSteps = 100
            };
            mm.DataPoints.Add("s", new List<double>() { 0, 4 });
            mm.DataPoints.Add("t", new List<double>() { 0, 1 });

            var file = await XMLWriter.Write(mm);

            Assert.IsTrue(file.Name.StartsWith(id) && file.Name.EndsWith(".xml"), "Exported file should have the title of the simulation as name, if there are  no conflicts. However existing files with the same name should not be replaced, but a unique name should be generated.");
        }

        ////[TestMethod]
        ////public async Task Write()
        ////{
        ////    var id = DateTime.Now.ToString();

        ////    Simulation simulation = new Simulation();
        ////    simulation.Title = id;

        ////    var file = await XMLWriter.Write(simulation);

        ////    Assert.AreEqual(id, file.Name, "Exported file should have the title of the simulation as name, if there are  no conflicts.");
        ////}
    }
}
