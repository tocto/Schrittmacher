using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.mariuszgromada.math.mxparser;
using Schrittmacher.Exceptions;
using Schrittmacher.Model;
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
        public void Calc_ArgumentNullExceptions()
        {
            MathModel nullModel = null;
            Assert.ThrowsException<ArgumentNullException>(() => calculator.Calc(nullModel, 10));
        }

        [TestMethod]
        public void Calc_IncompleteMathModelException()
        {
            //arrange
            MathModel model = new MathModel();
            uint stepCount = 10;

            model.Expressions.Add(new MathModelExpression("s = t * v"));
            model.Expressions.Add(new MathModelExpression("t = 1"));
           

            Assert.ThrowsException<IncompleteMathModelException>(() => calculator.Calc(model, stepCount));
        }

        [TestMethod]
        public void Calc_LinearEquation()
        {
            //arrange
            MathModel model = new MathModel();
            uint stepCount = 10;

            model.Expressions.Add(new MathModelExpression("s = t * v"));
            model.Expressions.Add(new MathModelExpression("t = t + dt"));
            model.Expressions.Add(new MathModelExpression("v = 1"));
            model.Expressions.Add(new MathModelExpression("t = 0"));
            model.Expressions.Add(new MathModelExpression("dt = 1"));

            //act
            var resutls = calculator.Calc(model, stepCount);

            //assert
            Assert.AreEqual(Convert.ToInt32(stepCount + 1), resutls["s"].Count, "Each step should result in an entry. Additional to the start value there will be n + 1 entries.");
            Assert.IsTrue(resutls["s"].All(entry => !entry.Equals(double.NaN) && !entry.Equals(double.NaN)), "Each result item must be valid.");
        }

        [TestMethod]
        public void Calc_TrivialSolution()
        {
            //arrange
            MathModel model = new MathModel();
            uint stepCount = 10;

            model.Expressions.Add(new MathModelExpression("s = t * v"));
            model.Expressions.Add(new MathModelExpression("t = 1"));
            model.Expressions.Add(new MathModelExpression("v = 0"));

            var resutls = calculator.Calc(model, stepCount);

            Assert.AreEqual(Convert.ToInt32(stepCount + 1), resutls["s"].Count, "Each step should result in an entry. Additional to the start value there will be n + 1 entries.");
            Assert.IsTrue(resutls["s"].All(entry => entry.Equals(0)), "Each result item must be calculated correctly.");
        }

        [TestMethod]
        public void Calc_MultipleUsage()
        {
            //arrange
            MathModel model = new MathModel();
            uint stepCount = 1;

            model.Expressions.Add(new MathModelExpression("s = t * v"));
            model.Expressions.Add(new MathModelExpression("t = 2"));
            model.Expressions.Add(new MathModelExpression("v = 1"));

            var results = calculator.Calc(model, stepCount);

            Assert.AreEqual(Convert.ToInt32(stepCount + 1), results["s"].Count, "Each step should result in an entry. Additional to the start value there will be n + 1 entries.");
            Assert.IsTrue(results["s"].All(entry => entry.Equals(2)), "Each result item must be calculated correctly.");

            MathModel model2 = new MathModel();
            uint stepCount2 = 1;

            model2.Expressions.Add(new MathModelExpression("s = t * v"));
            model2.Expressions.Add(new MathModelExpression("t = 2"));
            model2.Expressions.Add(new MathModelExpression("v = 5"));

            var results2 = calculator.Calc(model2, stepCount2);

            Assert.AreEqual(Convert.ToInt32(stepCount2 + 1), results["s"].Count, "Calling the calculator twice with different step count should led to the use of the new count only.");
            Assert.IsTrue(results2["s"].All(entry => entry.Equals(10)), "Each result item must be calculated correctly, when the calculator got a new math model. (All global paramater should be reset.)");

            MathModel model3 = new MathModel();
            uint stepCount3 = 1;

            model3.Expressions.Add(new MathModelExpression("s = t * v"));
            model3.Expressions.Add(new MathModelExpression("t = 0"));
            model3.Expressions.Add(new MathModelExpression("v = 0"));

            var results3 = calculator.Calc(model3, stepCount3);

            Assert.AreEqual(Convert.ToInt32(stepCount3 + 1), results["s"].Count, "Calling the calculator with new step count should always lead to the usage of the latest counter.");
            Assert.IsTrue(results3["s"].All(entry => entry.Equals(0)), "Each result item must be calculated correctly, when the calculator got another new math model. (All global paramater should be reset.)");
        }
    }
}
