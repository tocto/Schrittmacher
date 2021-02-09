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
        Dictionary<string, List<double>> outputDic = new Dictionary<string, List<double>>();

        Dictionary<string, double> constants = new Dictionary<string, double>(); // depricated

        Dictionary<string, Argument> argumentDic = new Dictionary<string, Argument>();

        MathModel model = null;

        public Dictionary<string, List<double>> Calc(MathModel model, int numberOfSteps)
        {
            Reset();

            this.model = model;

            FillArgumentDic();

            PrepareResultDic();

            IdentifyConstants();

            for (uint i = 0; i <= numberOfSteps; i++)
            {
                CompleteValueSet(i);
            }

            return outputDic;
        }

        private void Reset()
        {
            outputDic.Clear();
            argumentDic.Clear();
            constants.Clear();
        }

        private void FillArgumentDic()
        {
            foreach(var expression in model.Expressions)
            {
                if (!argumentDic.Keys.Contains(expression.Name))
                {
                    argumentDic.Add(expression.Name, new Argument( expression.Name + "=" + expression.AssignmentExpression));
                }
            }
        }

        private void PrepareResultDic()
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
            do
            {
                foreach (var id in outputDic.Keys)
                {
                    
                    string expressionString = model.Expressions.First(s => s.Name.Equals(id)).AssignmentExpression; // change to single
                    
                    Expression expression = new Expression(expressionString, argumentDic.Values.ToArray());
                    double value = expression.calculate();

                    if (!value.Equals(double.NaN) && outputDic[id].Count < stepCounter + 1) // valid and not yet added
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
