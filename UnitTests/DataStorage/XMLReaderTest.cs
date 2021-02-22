using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schrittmacher.DataStorage;
using Schrittmacher.Model;
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

            Assert.AreEqual(simToXML.Name, simFromXML.Name, "Name should be equal.");
            Assert.AreEqual(simToXML.MathModel.Text, simFromXML.MathModel.Text, "Math Model text expression should be equal.");
            Assert.AreEqual(simToXML.MathModel.Expressions.Count, simFromXML.MathModel.Expressions.Count, "Math model expresions should be updated when math model has been deserialized.");
            Assert.AreEqual(simToXML.Plots.Count, simFromXML.Plots.Count, "Number of saved and read plot data must be equal.");
            Assert.IsTrue(simToXML.Plots.SequenceEqual(simFromXML.Plots), "Plot Data should be equal.");
            // todo implement equals/getHashcode for simulation class + tests
        }

        [TestMethod]
        public async Task ReadAsync_Exception()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await XMLReader.ReadAsync(null));
        }
    }
}
