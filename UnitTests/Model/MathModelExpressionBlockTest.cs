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
    public class MathModelExpressionBlockTest
    {
        string model1 = "y = m*x +n // linear function";

        string model2 = "// linear function \n y = m*x +n";

        string model3 = "// linear function y = m*x +n";

        string model4 = "";

        string model5 = "\n // a comment in nowhere \n";

        string model6 = " y = mx + n \n s = a/2 * t^2 \n //comment \n m = 4 // rise ";

        MathModelExpressionBlock mmeBlock = new MathModelExpressionBlock();

        [TestMethod]
        public void GetMathModelExpressions()
        {
            mmeBlock.ExpressionsTextModel = model1;
            int numberOfExpressionsInModel1 = mmeBlock.GetMathModelExpressions().Count;
            Assert.AreEqual(1, numberOfExpressionsInModel1, "An expression before a comment should be found.");

            mmeBlock.ExpressionsTextModel = model2;
            int numberOfExpressionsInModel2 = mmeBlock.GetMathModelExpressions().Count;
            Assert.AreEqual(1, numberOfExpressionsInModel2, "An expression in a new line after a comment should be found.");

            mmeBlock.ExpressionsTextModel = model3;
            int numberOfExpressionsInModel3 = mmeBlock.GetMathModelExpressions().Count;
            Assert.AreEqual(0, numberOfExpressionsInModel3, "An expression behind a comment indicator should not be found.");

            mmeBlock.ExpressionsTextModel = model4;
            int numberOfExpressionsInModel4 = mmeBlock.GetMathModelExpressions().Count;
            Assert.AreEqual(0, numberOfExpressionsInModel4, "Nothing should be found for an empty string.");

            mmeBlock.ExpressionsTextModel = model5;
            int numberOfExpressionsInModel5 = mmeBlock.GetMathModelExpressions().Count;
            Assert.AreEqual(0, numberOfExpressionsInModel5, "A comment between some empty lines should not be interpreted as an expression.");

            mmeBlock.ExpressionsTextModel = model6;
            int numberOfExpressionsInModel6 = mmeBlock.GetMathModelExpressions().Count;
            Assert.AreEqual(3, numberOfExpressionsInModel6, "If there are multiple expression spread over multiple lines, all should be found.");
        }
    }
}
