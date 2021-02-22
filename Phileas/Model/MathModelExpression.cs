using Schrittmacher.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Schrittmacher.Model
{
    /// <summary>
    /// A mathematical expression which assignes a value or formular to a target variable.
    /// </summary>
    [Serializable]
    public class MathModelExpression : INotifyPropertyChanged
    {
        private string name = string.Empty;

        private string note = string.Empty;

        private string text = string.Empty;

        private string assignment = string.Empty;

        /// <summary>
        /// This is an identifactor of the object in the math model, e.g. "x" for "x = 2".
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Keeps additional information about the object.
        /// </summary>
        public string Note
        {
            get
            {
                return this.note;
            }

            set
            {
                if (value != this.note)
                {
                    this.note = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The complete  string representation of this object including comments, e.g. "s = v * t" or "x = 1.5 // this is a comment".
        /// </summary>
        /// <remarks>
        /// All related properties will be updated automatically by updating this property.
        /// </remarks>
        public string Text
        {
            get => this.text;

            set
            {
                if (value != this.text)
                {
                    this.text = value;
                    UpdateNameAndAssignment();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The math expression of this object excluding comments. Only assigned variable an the calculation term will be return, e.g. "x = 1.5".
        /// </summary>
        public string MathText
        {
            get => this.Name + "=" + this.Assignment;
        }

        /// <summary>
        /// The math expression assigned to the named target variable of this expression, e.g. this property returns "x + 2" if this object is "y(x) = x + 2". 
        /// </summary>
        public string Assignment
        {
            get => this.assignment;

            set
            {
                if (value != this.assignment)
                {
                    this.assignment = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.
        public event PropertyChangedEventHandler PropertyChanged;

        public MathModelExpression() : base()
        {

        }

        public MathModelExpression(string stringExpression) : base()
        {
            this.Text = stringExpression;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateNameAndAssignment()
        {
            string[] array = this.text.Split("=");

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

            this.Assignment = assignmentCandidate;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
