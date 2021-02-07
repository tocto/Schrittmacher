using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    public class Calculator
    {
        Dictionary<string, Argument> expressionsDic = new Dictionary<string, Argument>();

        HashSet<string> initialValues = new HashSet<string>();

        /// <summary>
        /// Dic for results <fName, List<(x, f(x))>>
        /// </summary>
        Dictionary<string, List<(double xValue, double yValue)>> results = new Dictionary<string, List<(double, double)>>();

        string steppingVariableName = null;

        public double StepCount { get; set; } = 100;

        public double StepRange { get; set; } = 1;

        public List<(double, double)> Calc(MathModel mathModel, string steppingVariableName, params string[] dependedVariableName)
        {
            ResetCalculator();

            this.steppingVariableName = steppingVariableName;

            // set initial value of stepping variable
            if (!mathModel.Expressions.Any(e => e.Name.Equals(steppingVariableName)))
                throw new ArgumentException("Stepping variable is not given.");

            var mmeInitial = mathModel.Expressions.Single(i => i.Name.Equals(steppingVariableName));
            Expression expressionInitial = new Expression(mmeInitial.AssignmentExpression);
            var valueOfSteppingVariable = expressionInitial.calculate();
            if (valueOfSteppingVariable.Equals(double.NaN)) throw new ArgumentException("Stepping variable is not given with a valid initial value.");

            //prepare results dictionary
            foreach (var v in dependedVariableName) results.Add(v, new List<(double, double)>());

            //Prepare given math model for calculation
            foreach (MathModelExpression mme in mathModel.Expressions)
            {
                Argument argument = new Argument(mme.Name + " = " + mme.AssignmentExpression);
                expressionsDic.Add(mme.Name, argument);

                // identify inital values by checking if expression returns a valid result
                Expression expessionHypotheses = new Expression();
                expessionHypotheses.setExpressionString(argument.getArgumentExpressionString());
                //var temp = expessionHypotheses.getArgumentsNumber();
                if (expessionHypotheses.calculate() != double.NaN) initialValues.Add(mme.Name);
            }

            // add start value to results
            foreach (var key in results.Keys)
            {
                Expression expression = new Expression(expressionsDic[key].getArgumentExpressionString(), expressionsDic.Values.ToArray());
                var startFunctionValue = expression.calculate();
                results[key].Add((valueOfSteppingVariable, startFunctionValue));
            }


            for (int i = 0; i < StepCount; i++)
            {
                CalcStep();
            }

            //Argument a2 = new Argument(e2.Name + " = " + e2.AssignmentExpression);
            //Argument a3 = new Argument("y = 3/4 + s");
            //Expression ex = new Expression(e.AssignmentExpression, a2, a3);
            //ex.calculate();

            return results[dependedVariableName[0]];
        }

        private void CalcStep()
        {
            double newX = GoOneStep();

            HashSet<string> checklistForStep = expressionsDic.Keys.ToHashSet();

            SetupExpressionSetForStepCalculation();

            CalcAllMappingValues(newX, checklistForStep);
        }

        private void SetupExpressionSetForStepCalculation()
        {
            
        }

        private void CalcAllMappingValues(double newX, HashSet<string> checklistForStep)
        {
            foreach (var key in expressionsDic.Keys)
            {
                double newY = new Expression(expressionsDic[key].getArgumentExpressionString(), expressionsDic.Values.ToArray()).calculate();

                if (!newY.Equals(double.NaN))
                {
                    checklistForStep.ExceptWith(new string[] { key });

                    if (results.ContainsKey(key)) results[key].Add((newX, newY));
                }
            }

            if (checklistForStep.Count > 0) CalcAllMappingValues(newX, checklistForStep);
        }

        private double GoOneStep()
        {
            var steppingVariableExpression = expressionsDic[steppingVariableName];
            var nextBaseValue = steppingVariableExpression.getArgumentValue() + StepRange;
            steppingVariableExpression.setArgumentValue(nextBaseValue);

            return nextBaseValue;
        }

        /// <summary>
        /// Resets object association of cache variable.
        /// </summary>
        /// <remarks>
        /// New Objects ensures, that old results can still be used elsewhere.
        /// </remarks>
        private void ResetCalculator()
        {
            expressionsDic = new Dictionary<string, Argument>();
            initialValues = new HashSet<string>();
            results = new Dictionary<string, List<(double, double)>>();
        }
    }
}
