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
        public string ErrorMessage = string.Empty;

        public async Task<bool> CheckAsync(string mathText)
        {
            return await Task.Run(() =>
            {
                string[] lines = mathText.Split('\r');

                if (lines.Any(l => IsLineSyntaxValid(l) == false)) return false;
                else return true;
            });

        }

        private bool IsLineSyntaxValid(string line)
        {
            MathModelExpression mme = new MathModelExpression(line);

            if (mme.MathText != string.Empty)
            {
                Expression expression = new Expression(mme.MathText);

                if (!expression.checkLexSyntax())
                {
                    this.ErrorMessage = expression.getErrorMessage();
                    return false;
                }
            }

            return true;
        }
    }
}
