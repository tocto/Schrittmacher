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
            Simulation simToDeserialize = TestFactories.SimulationTestFactory.Make();
            var xmlFile = await XMLWriter.Write(simToDeserialize);

            var simDeserialized = await XMLReader.ReadAsync(xmlFile);

            Assert.AreEqual(simToDeserialize.Plots.First().DataPoints["x"].Count(), simDeserialized.Plots.First().DataPoints["x"].Count());
        }
    }
}
