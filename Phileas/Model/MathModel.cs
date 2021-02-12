using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    /// <summary>
    /// A mathematic equation system holding initial values which represents a real world problem.
    /// </summary>
    public class MathModel : ModelStructureUnit
    {
        private readonly ObservableCollection<MathModelExpression> expressions = new ObservableCollection<MathModelExpression>();

        public ObservableCollection<MathModelExpression> Expressions
        {
            get
            {
                return this.expressions;
            }
        }
    }
}
