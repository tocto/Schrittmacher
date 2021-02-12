using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    /// <summary>
    /// Basic math model element
    /// </summary>
    public abstract class MathModelStructureUnit : INotifyPropertyChanged
    {
        private string name;

        private string note;

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

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
