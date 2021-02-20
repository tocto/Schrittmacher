using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phileas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Model
{
    [TestClass]
    public class PlotDataTest
    {
        static PlotData data;

        static PlotData dataEqual;

        static PlotData dataEqual2;

        static PlotData dataNotEqual;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            data = new PlotData()
            {
                Name = "equal",
                XParameter = "x",
                YParameter = "y",
                XAxisTitle = "x",
                YAxisTitle = "y"
            };

            dataEqual = new PlotData()
            {
                Name = "equal",
                XParameter = "x",
                YParameter = "y",
                XAxisTitle = "x",
                YAxisTitle = "y"
            };

            dataEqual2 = new PlotData()
            {
                Name = "equal",
                XParameter = "x",
                YParameter = "y",
                XAxisTitle = "x",
                YAxisTitle = "y"
            };

            dataNotEqual = new PlotData()
            {
                Name = "equal",
                XParameter = "x",
                YParameter = "y",
                XAxisTitle = "x",
                YAxisTitle = "y"
            };

            List<double> dataPoints = new List<double>();
            for (int i = 0; i < 10; i++) dataPoints.Add(i);

            data.DataPoints.Add("x", dataPoints.ToList());
            data.DataPoints.Add("y", dataPoints.ToList());

            dataEqual.DataPoints.Add("x", dataPoints.ToList());
            dataEqual.DataPoints.Add("y", dataPoints.ToList());

            dataEqual2.DataPoints.Add("x", dataPoints.ToList());
            dataEqual2.DataPoints.Add("y", dataPoints.ToList());

            dataPoints.Add(1); // this is the difference
            dataNotEqual.DataPoints.Add("x", dataPoints.ToList());
            dataNotEqual.DataPoints.Add("y", dataPoints.ToList());
        }

        [TestMethod]
        public void Equals()
        {
            // reflexiv
            Assert.AreEqual(data, data);

            // symmetric
            Assert.AreEqual(data, dataEqual);
            Assert.AreEqual(dataEqual, data);

            // transitive
            //Assert.AreEqual(data, dataEqual); checked above
            Assert.AreEqual(dataEqual, dataEqual2);
            Assert.AreEqual(data, dataEqual2);

            // not equal
            Assert.AreNotEqual(data, dataNotEqual);
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            // same hash code for equal objects
            Assert.AreEqual(data.GetHashCode(), data.GetHashCode());
            Assert.AreEqual(data.GetHashCode(), dataEqual.GetHashCode());

            // different hash code for different objects
            Assert.AreNotEqual(data.GetHashCode(), dataNotEqual.GetHashCode());
        }
    }
}
