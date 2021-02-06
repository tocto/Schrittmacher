using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    public class Simulation
    {
        private readonly MathModel mathModel = new MathModel();

        public MathModel MathModel { get => this.mathModel; }
    }
}
