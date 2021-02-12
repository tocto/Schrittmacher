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
    public class MathModelExpressionTest
    {
        string name = "s";
        string assignment = "v * t";
        string comment = "// motion equation for constant speed";

        [TestMethod]
        public void StringExpression_SetAndUpdate()
        {
            string validStringExpression = name + " " + "=" + assignment + comment;
            MathModelExpression mme = new MathModelExpression();

            mme.StringRepresentation = validStringExpression;

            Assert.AreEqual(name, mme.Name, "Name should be extracted correctly.");
            Assert.AreEqual(assignment, mme.AssignmentExpression, "Assignment expression should be extracted correctly.");
            Assert.AreEqual(comment.Substring(2), mme.Note, "Comment should be set without the comment indicator '//'.");
        }

        [TestMethod]
        public void MathExpressionString()
        {
            string validStringExpression = name + " " + "=" + assignment + comment;
            MathModelExpression mme = new MathModelExpression(validStringExpression);

            Assert.AreEqual(name + "=" + assignment, mme.MathExpressionString, "Only the math expression without the comment should be returned.");

        }

        [TestMethod]
        public void toString()
        {
            string stringExpression = name + "=" + assignment;
            MathModelExpression mme = new MathModelExpression(stringExpression);

            Assert.AreEqual(stringExpression, mme.ToString());
        }
    }
}
