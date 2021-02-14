using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    /// <summary>
    /// A mathematic equation system holding initial values which represents a real world problem.
    /// </summary>
    public class MathModel : MathModelStructureUnit
    {
        public readonly MathModelExpressionTextBlock MathModelExpressionBlock = new MathModelExpressionTextBlock();

        private readonly ObservableCollection<MathModelExpression> expressions = new ObservableCollection<MathModelExpression>();

        public MathModel()
        {
            this.MathModelExpressionBlock.PropertyChanged += OnMMETextBlockChanges;
        }

        private void OnMMETextBlockChanges(object sender, PropertyChangedEventArgs e)
        {
            this.expressions.Clear();
            var updatedBlocks = MathModelExpressionBlock.GetMathModelExpressions();
            foreach (var expresssion in updatedBlocks) expressions.Add(expresssion);
        }

        public ObservableCollection<MathModelExpression> Expressions
        {
            get
            {
                return this.expressions;
            }
        }
    }
}
