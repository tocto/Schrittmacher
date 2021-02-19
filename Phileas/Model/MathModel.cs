using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    /// <summary>
    /// A mathematic equation system holding initial values which represents a real world problem.
    /// </summary>
    [Serializable]
    public class MathModel : MathModelStructureUnit, INotifyPropertyChanged
    {
        private string expressionsTextModel = string.Empty;

        private readonly HashSet<MathModelExpression> expressions = new HashSet<MathModelExpression>();

        /// <summary>
        /// Keeps the user generated math model as text.
        /// </summary>
        public string Text
        {
            get => expressionsTextModel;

            set
            {
                if (value != this.expressionsTextModel)
                {
                    this.expressionsTextModel = value;
                    UpdateExpressions();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Holdes the isolated math model expressions which are in summary the mathematical operations and assignments of the model.
        /// </summary>
        public HashSet<MathModelExpression> Expressions
        {
            get
            {
                return this.expressions;
            }
        }

        private void UpdateExpressions()
        {
            this.expressions.Clear();

            System.IO.StringReader stringReader = new System.IO.StringReader(this.expressionsTextModel);

            string line;
            while ((line = stringReader.ReadLine()) != null)
            {
                var lineFracments = line.Trim().Split("//");

                if (lineFracments[0].Length == 0) continue; // if nothing is before '//'
                else
                {
                    expressions.Add(new MathModelExpression(line));
                }
            }
        }
    }
}
