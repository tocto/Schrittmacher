using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    public class MathModel : MathModelStructureUnit
    {
        private ObservableCollection<Expression> expressions = new ObservableCollection<Expression>() { new Expression() };

        public ObservableCollection<Expression> Expressions
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
