using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.mariuszgromada.math.mxparser;
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
        Calculator calculator = new Calculator();

        [TestMethod]
        public void Calc_LinearEquation()
        {
            //arrange
            MathModel model = new MathModel();
            int stepCount = 10;

            // s = v * t, v = 1, t = 0
            model.Expressions.Add(new MathModelExpression() { Name = "s", AssignmentExpression = "t * v" });
            model.Expressions.Add(new MathModelExpression() { Name = "t", AssignmentExpression = "t + dt" });
            model.Expressions.Add(new MathModelExpression() { Name = "v", AssignmentExpression = "1" });
            model.Expressions.Add(new MathModelExpression() { Name = "t", AssignmentExpression = "0" });
            model.Expressions.Add(new MathModelExpression() { Name = "dt", AssignmentExpression = "1" });

            var resutls = calculator.Calc(model, stepCount);

            Assert.AreEqual(stepCount + 1, resutls["s"].Count, "Each step should result in an entry. Additional to the start value there will be n + 1 entries.");
            Assert.IsTrue(resutls["s"].All(entry => !entry.Equals(double.NaN) && !entry.Equals(double.NaN)), "Each result item must be valid.");
        }

        [TestMethod]
        public void Calc_LinearEquationSystem()
        {
            throw new NotImplementedException();
            ////arrange
            //MathModel model = new MathModel();

            //// s = v * t, v = 1, t = 0
            //model.Expressions[0].Name = "s";
            //model.Expressions[0].AssignmentExpression = "v * t";
            //model.Expressions.Add(new MathModelExpression() { Name = "v", AssignmentExpression = "t" });
            //model.Expressions.Add(new MathModelExpression() { Name = "t", AssignmentExpression = "0" });

            //var resutls = calculator.Calc(model, "t", "s");

            //Assert.AreEqual(10 + 1, resutls.Count, "Each step should result in an entry. Additional to the start value there will be n + 1 entries.");
            //Assert.IsTrue(resutls.All(entry => !entry.Item1.Equals(double.NaN) && !entry.Item2.Equals(double.NaN)), "Each result item must be valid.");
        }
    }
}
