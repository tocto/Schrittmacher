using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    /// <summary>
    /// Base class for any numeric approximation.
    /// </summary>
    [Serializable]
    public class Simulation : INotifyPropertyChanged
    {
        private string name = string.Empty;

        private string note = string.Empty;

        private MathModel mathModel = new MathModel(); // not readonly because serialization is simpler this way (readonly does not serialize using the default serializer)

        private readonly ObservableCollection<PlotData> plots = new ObservableCollection<PlotData>();

        public string Name
        {
            get => this.name;

            set
            {
                if (value != name)
                {
                    this.name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
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

        public MathModel MathModel
        {
            get => this.mathModel;

            set
            {
                if (this.mathModel != value)
                {
                    this.mathModel = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MathModel"));
                }
            }
        }

        public ObservableCollection<PlotData> Plots
        {
            get => this.plots;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
