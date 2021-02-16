using Phileas.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Phileas.Views.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class SimulationPage : Page
    {
        public Simulation Simulation = App.Simulation;

        public SimulationPage()
        {
            this.InitializeComponent();
        }

        private void AppBarButton_AddPlot_Click(object sender, RoutedEventArgs e)
        {
            Plotter plotter = new Plotter();
            PlotData plotData = new PlotData();

            try
            {
                plotData.DataPoints = plotter.CalcDataPoints(0);
                App.Simulation.Plots.Add(plotData);

                ListView_Plots.ScrollIntoView(ListView_Plots.Items.Last());
            }
            catch(Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
    }
}
