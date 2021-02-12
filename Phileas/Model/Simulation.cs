using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    /// <summary>
    /// Base class for any numeric approximation.
    /// </summary>
    public class Simulation : INotifyPropertyChanged
    {
        private string title = string.Empty;

        private string note = string.Empty;

        private readonly MathModel mathModel = new MathModel();


        public string Title
        {
            get => this.title;

            set
            {
                if (value != title)
                {
                    this.title = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }

        public string Note
        {
            get => this.note;

            set
            {
                if (value != note)
                {
                    this.note = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Note"));
                }
            }
        }

        public MathModel MathModel { get => this.mathModel; }

        public event PropertyChangedEventHandler PropertyChanged;


    }
}
