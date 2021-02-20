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
    public class XMLReaderTest
    {
        [TestMethod]
        public async Task ReadAsync()
        {
            Simulation simToXML = TestFactories.SimulationTestFactory.Make();
            var xmlFile = await XMLWriter.Write(simToXML);

            var simFromXML = await XMLReader.ReadAsync(xmlFile);

            Assert.AreEqual(simToXML.Title, simFromXML.Title, "Name should be equal.");
            Assert.AreEqual(simToXML.MathModel.Text, simFromXML.MathModel.Text, "Math Model text expression should be equal.");
            Assert.IsTrue(simToXML.Plots.SequenceEqual(simFromXML.Plots), "Plot Data should be equal.");
            // todo implement equals/getHashcode for simulation class + tests
        }
    }
}
