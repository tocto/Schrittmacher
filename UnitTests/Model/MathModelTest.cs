using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schrittmacher.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Model
{
    [TestClass]
    public class MathModelTest
    {
        string model1 = "y = m*x +n // linear function";

        string model2 = "// linear function \n y = m*x +n";

        string model3 = "// linear function y = m*x +n";

        string model4 = "";

        string model5 = "\n // a comment in nowhere \n";

        string model6 = " y = mx + n \n s = a/2 * t^2 \n //comment \n m = 4 // rise ";

        MathModel mm = new MathModel();

        [TestMethod]
        public void UpdateExpressions()
        {
            mm.Text = model1;
            int numberOfExpressionsInModel1 = mm.Expressions.Count;
            Assert.AreEqual(1, numberOfExpressionsInModel1, "An expression before a comment should be found.");

            mm.Text = model2;
            int numberOfExpressionsInModel2 = mm.Expressions.Count;
            Assert.AreEqual(1, numberOfExpressionsInModel2, "An expression in a new line after a comment should be found.");

            mm.Text = model3;
            int numberOfExpressionsInModel3 = mm.Expressions.Count;
            Assert.AreEqual(0, numberOfExpressionsInModel3, "An expression behind a comment indicator should not be found.");

            mm.Text = model4;
            int numberOfExpressionsInModel4 = mm.Expressions.Count;
            Assert.AreEqual(0, numberOfExpressionsInModel4, "Nothing should be found for an empty string.");

            mm.Text = model5;
            int numberOfExpressionsInModel5 = mm.Expressions.Count;
            Assert.AreEqual(0, numberOfExpressionsInModel5, "A comment between some empty lines should not be interpreted as an expression.");

            mm.Text = model6;
            int numberOfExpressionsInModel6 = mm.Expressions.Count;
            Assert.AreEqual(3, numberOfExpressionsInModel6, "If there are multiple expression spread over multiple lines, all should be found.");
        }
    }
}
