using org.mariuszgromada.math.mxparser;
using Phileas.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    /// <summary>
    /// Solves the equation of a given math model.
    /// </summary>
    public class Calculator
    {
        Dictionary<string, List<double>> outputDic = new Dictionary<string, List<double>>();

        Dictionary<string, double> constants = new Dictionary<string, double>(); // depricated

        Dictionary<string, Argument> argumentDic = new Dictionary<string, Argument>();

        MathModel model = null;

        /// <summary>
        /// Attempts to solve the given math model equations.
        /// </summary>
        /// <param name="model">the model of concern</param>
        /// <param name="numberOfSteps">The number of steps following the intial values given in the math model.</param>
        /// <returns>A Dictionary where the key is the name of the parameter an the value is a list with a value for each calculated step.</returns>
        public Dictionary<string, List<double>> Calc(MathModel model, uint numberOfSteps)
        {
            if (model == null) throw new ArgumentNullException("Model must not be null.");

            Reset();

            this.model = model;

            FillArgumentDic();

            PrepareOutputDic();

            IdentifyConstants();

            for (uint i = 0; i <= numberOfSteps; i++)
            {
                CompleteValueSet(i);
            }

            return outputDic;
        }

        private void Reset()
        {
            outputDic = new Dictionary<string, List<double>>(); // so refernces to the old object can still be used by the client
            argumentDic.Clear();
            constants.Clear();
        }

        private void FillArgumentDic()
        {
            foreach(var expression in model.Expressions)
            {
                if (!argumentDic.Keys.Contains(expression.Name))
                {
                    argumentDic.Add(expression.Name, new Argument(expression.MathExpressionString));
                }
            }
        }

        private void PrepareOutputDic()
        {
            foreach (var expression in model.Expressions)
            {
                if(!outputDic.Keys.Contains(expression.Name)) outputDic.Add(expression.Name, new List<double>());
            }
        }

        private void IdentifyConstants()
        {
            foreach (var expression in this.model.Expressions)
            {
                double value = new Expression(expression.AssignmentExpression).calculate();

                if (!value.Equals(double.NaN))
                {
                    constants.Add(expression.Name, value);
                    argumentDic[expression.Name].setArgumentValue(value);
                }
            }
        }

        private void CompleteValueSet(uint stepCounter)
        {
            int maxLoopsIfSolvable = outputDic.Count;
            int loopCounter = 0;

            do
            {
                if (loopCounter == maxLoopsIfSolvable) throw new IncompleteMathModelException("The equation system is not complete.");
                loopCounter++;

                foreach (var id in outputDic.Keys)
                {
                    if (outputDic[id].Count > stepCounter) continue; // skip if already added

                    // attempt to find solution
                    string assignmentString = model.Expressions.First(s => s.Name.Equals(id)).AssignmentExpression;
                    
                    Expression expression = new Expression(assignmentString, argumentDic.Values.ToArray());
                    double value = expression.calculate();

                    // check if valid 
                    if (!value.Equals(double.NaN)) 
                    {
                        argumentDic[id].setArgumentValue(value);
                        outputDic[id].Add(value);
                    }
                }
            }
            while (outputDic.Values.Any(v => v.Count < stepCounter + 1));
        }


    }
}
