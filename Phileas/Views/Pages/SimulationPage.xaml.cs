using Schrittmacher.DataStorage;
using Schrittmacher.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Schrittmacher.Views.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class SimulationPage : Page
    {
        public Simulation Simulation = new Simulation();

        public SimulationPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Simulation handedSimulation) Simulation = handedSimulation;

            Bindings.Update();
        }

        private void Button_AddDiagramm_Click(object sender, RoutedEventArgs e)
        {
            PlotData plotData = new PlotData();
            uint numberOfSteps = (uint)NumberBox_Steps.Value;

            try
            {
                plotData.DataPoints = new Calculator().Calc(this.Simulation.MathModel, numberOfSteps);
                Simulation.Plots.Add(plotData);

                ListView_Plots.ScrollIntoView(ListView_Plots.Items.Last());
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

        private async void AppBarButton_Save_Click(object sender, RoutedEventArgs e)
        {

            ProgressBar_Saving.Visibility = Visibility.Visible;
            try
            {
                await XMLWriter.Write(Simulation);
                if (!App.Simulations.Contains(this.Simulation)) App.Simulations.Add(this.Simulation);
                
                await Task.Delay(3000); // Feedback for the user
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message); // TODO feedback for the suer
            }
            finally
            {
                ProgressBar_Saving.Visibility = Visibility.Collapsed;
            }

        }

        private void BasicLineChart_DeletionRequested(object sender, EventArgs e)
        {
            if ((sender as FrameworkElement).DataContext is PlotData plotData)
            {
                this.Simulation.Plots.Remove(plotData);
            }
        }

        private void NumberBox_Steps_ValueChanged(MUXC.NumberBox sender, MUXC.NumberBoxValueChangedEventArgs args)
        {
            uint number = (uint)Math.Ceiling(this.NumberBox_Steps.Value);

            NumberBox_Steps.Value = number > 500 || number < 10 ? 10 : number;
        }
    }
}
