using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    public class Expression : MathModelStructureUnit
    {
        public Value Value { get; set; }

        public string AssignmentExpression { get; set; }
    }
}
