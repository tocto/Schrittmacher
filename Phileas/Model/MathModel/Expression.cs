using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model.MathModel
{
    public class Expression
    {
        /// <summary>
        /// The variable that is defined through this class.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// The constant or operation which allocates a value to the target variable.
        /// </summary>
        public string Assignment { get; set; }
    }
}
