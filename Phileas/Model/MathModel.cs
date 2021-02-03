using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    public class MathModel : ModelStructureUnit
    {
        private ObservableCollection<MathModelExpression> expressions = new ObservableCollection<MathModelExpression>() { new MathModelExpression() };

        public ObservableCollection<MathModelExpression> Expressions
        {
            get
            {
                return this.expressions;
            }

            set
            {
                if (value != this.expressions)
                {
                    this.expressions = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
