using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    public class MathModelExpressionTextBlock : INotifyPropertyChanged
    {
        private string expressionsTextModel = string.Empty;

        private List<MathModelExpression> mathModelExpressions;

        public string ExpressionsTextModel
        {
            get => expressionsTextModel;

            set
            {
                if (value != this.expressionsTextModel)
                {
                    this.expressionsTextModel = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Expressions"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<MathModelExpression> GetMathModelExpressions()
        {
            mathModelExpressions = new List<MathModelExpression>();

            System.IO.StringReader stringReader = new System.IO.StringReader(this.expressionsTextModel);

            MakeLinesToMMExpressions(stringReader);

            return mathModelExpressions;
        }

        private void MakeLinesToMMExpressions(StringReader stringReader)
        {
            string line;
            while ((line = stringReader.ReadLine()) != null)
            {
                var lineFracments = line.Trim().Split("//");

                if (lineFracments[0].Length == 0) continue; // if nothing is before '//'
                else
                {
                    mathModelExpressions.Add(new MathModelExpression(line));
                }
            }
        }
    }
}
