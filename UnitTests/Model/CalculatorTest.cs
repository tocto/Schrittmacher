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
    public class CalculatorTest
    {
        Calculator calculator = new Calculator() { StepCount = 10, StepRange = 1 };

        [TestMethod]
        public void Calc_LinearEquation()
        {
            //arrange
            MathModel model = new MathModel();

            // s = v * t, v = 1, t = 0
            model.Expressions[0].Name = "s";
            model.Expressions[0].AssignmentExpression = "v * t";
            model.Expressions.Add(new MathModelExpression() { Name = "v", AssignmentExpression = "1" });
            model.Expressions.Add(new MathModelExpression() { Name = "t", AssignmentExpression = "0" });

            var resutls = calculator.Calc(model, "t", "s");

            Assert.AreEqual(10 + 1, resutls.Count, "Each step should result in an entry. Additional to the start value there will be n + 1 entries.");
            Assert.IsTrue(resutls.All(entry => !entry.Item1.Equals(double.NaN) && !entry.Item2.Equals(double.NaN)), "Each result item must be valid.");
        }

        [TestMethod]
        public void Calc_LinearEquationSystem()
        {
            //arrange
            MathModel model = new MathModel();

            // s = v * t, v = 1, t = 0
            model.Expressions[0].Name = "s";
            model.Expressions[0].AssignmentExpression = "v * t";
            model.Expressions.Add(new MathModelExpression() { Name = "v", AssignmentExpression = "t" });
            model.Expressions.Add(new MathModelExpression() { Name = "t", AssignmentExpression = "0" });

            var resutls = calculator.Calc(model, "t", "s");

            Assert.AreEqual(10 + 1, resutls.Count, "Each step should result in an entry. Additional to the start value there will be n + 1 entries.");
            Assert.IsTrue(resutls.All(entry => !entry.Item1.Equals(double.NaN) && !entry.Item2.Equals(double.NaN)), "Each result item must be valid.");
        }
    }
}
