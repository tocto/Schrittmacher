using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phileas.Exceptions;
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
        public void StringRepresentation_SetAndUpdate()
        {
            string validStringExpression = name + " " + "=" + assignment + comment;
            MathModelExpression mme = new MathModelExpression();

            mme.Text = validStringExpression;

            Assert.AreEqual(name, mme.Name, "Name should be extracted correctly.");
            Assert.AreEqual(assignment, mme.Assignment, "Assignment expression should be extracted correctly.");
            Assert.AreEqual(comment.Substring(2), mme.Note, "Comment should be set without the comment indicator '//'.");
        }

        [TestMethod]
        public void StringRepresentation_Exception()
        {
            MathModelExpression mme = new MathModelExpression();

            Assert.ThrowsException<MathModelSyntaxException>(() => mme.Text = "s _t = 4*t");
            Assert.ThrowsException<MathModelSyntaxException>(() => mme.Text = "s_t = 4*t = 4");
        }

        [TestMethod]
        public void MathExpressionString()
        {
            string validStringExpression = name + " " + "=" + assignment + comment;
            MathModelExpression mme = new MathModelExpression(validStringExpression);

            Assert.AreEqual(name + "=" + assignment, mme.MathText, "Only the math expression without the comment should be returned.");

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
