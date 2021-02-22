using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schrittmacher.Model
{
    public class SyntaxChecker
    {

        public bool Check(string expressionString)
        {
            bool isValid = true;

            MathModelExpression mme = new MathModelExpression(expressionString);
            if (mme.MathText.Length == 0) return true; // no math string to validate

            if (mme.MathText != string.Empty)
            {
                Expression expression = new Expression(expressionString);
                isValid = expression.checkLexSyntax();

                Debug.WriteLine(expression.getErrorMessage());
            }

            return isValid;
        }
    }
}
