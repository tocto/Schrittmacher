using Phileas.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    /// <summary>
    /// A mathematical expression which assignes a value or formular to a target variable.
    /// </summary>
    public class MathModelExpression : MathModelStructureUnit
    {
        private string stringExpression = string.Empty;

        private string assignmentExpression = string.Empty;

        /// <summary>
        /// The complete  string representation of this object including comments, e.g. "s = v * t" or "x = 1.5 // this is a comment".
        /// </summary>
        /// <remarks>
        /// All related properties will be updated automatically setting this one.
        /// </remarks>
        public string StringRepresentation
        {
            get => this.stringExpression;

            set
            {
                if (value != this.stringExpression)
                {
                    this.stringExpression = value;
                    UpdateNameAndAssignment();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The math expression of this object excluding comments. Only assigned variable an the calculation term will be return, e.g. "x = 1.5".
        /// </summary>
        public string MathExpressionString
        {
            get => this.Name + "=" + this.AssignmentExpression;
        }

        /// <summary>
        /// The math expression assigned to the named target variable of this expression, e.g. this property returns "x + 2" if this object is "y(x) = x + 2". 
        /// </summary>
        public string AssignmentExpression
        {
            get => this.assignmentExpression;

            set
            {
                if (value != this.assignmentExpression)
                {
                    this.assignmentExpression = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public MathModelExpression() : base()
        {

        }

        public MathModelExpression(string stringExpression) : base()
        {
            this.StringRepresentation = stringExpression;
        }

        private void UpdateNameAndAssignment()
        {
            string[] array = this.stringExpression.Split("=");

            if (array.Count() > 2) throw new MathModelSyntaxException("To many assignments ('=') used.");
            if (array.Count() < 2) return; 

            // update name
            string nameCanidate = array[0].Trim();
            if (nameCanidate.Contains(" ")) throw new MathModelSyntaxException("Target variable contains whitespaces.");
            else this.Name = nameCanidate;


            // update assigment
            string assignmentCandidate = array[1].Trim();

            // extract notes if existing
            if (assignmentCandidate.Contains("//"))
            {
                string[] assignExpAndNote = assignmentCandidate.Split("//", 2);
                assignmentCandidate = assignExpAndNote[0];
                this.Note = assignExpAndNote[1];
            }

            this.AssignmentExpression = assignmentCandidate;
        }

        public override string ToString()
        {
            return this.StringRepresentation;
        }
    }
}
