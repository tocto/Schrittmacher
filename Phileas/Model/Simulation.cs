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
    public class Simulation : INotifyPropertyChanged
    {
        private string title = string.Empty;

        private string note = string.Empty;

        private readonly MathModel mathModel = new MathModel();

        private readonly ObservableCollection<PlotData> plots = new ObservableCollection<PlotData>();

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

        public ObservableCollection<PlotData> Plots
        {
            get => this.plots;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Plot(uint numberOfSteps, string xParamterKey, string yParameterKey)
        {
            Calculator calculator = new Calculator();

            Dictionary<string, List<double>> results = null;
            
            results = calculator.Calc(App.Simulation.MathModel, numberOfSteps); // exception might thrown

            PlotData plotData = new PlotData()
            {
                DataPoints = results,
                xParameterKey = xParamterKey,
                yParameterKey = yParameterKey,
                Title = "Neues Diagramm ohne Titel" // temp
            };

            this.plots.Add(plotData);
        }


    }
}
