using Schrittmacher.DataStorage;
using Schrittmacher.Model;
using Schrittmacher.Views.Dialogs;
using Schrittmacher.Views.Plots;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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

namespace Schrittmacher.Views.Pages
{
    public sealed partial class SimulationPage : Page
    {
        public Simulation Simulation = new Simulation();

        public SimulationPage()
        {
            this.InitializeComponent();

            AutoSaver.Subscribe(this.Simulation);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Simulation handedSimulation) Simulation = handedSimulation;

            Bindings.Update();

            AutoSaver.Subscribe(this.Simulation);
        }

        private async void AppBarButton_AddDiagramm_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton_AddDiagramm.IsEnabled = false;

            EnsureMathModelBinding();

            PlotData plotData = new PlotData();
            uint numberOfSteps = (uint)NumberBox_Steps.Value;            

            try
            {
                var calcPlotDataTask = Task.Run<Dictionary<string,List<double>>>(() => new Calculator().Calc(this.Simulation.MathModel, numberOfSteps));

                // show dialog with phantom data
                plotData.DataPoints = await Task.Run(() => new Calculator().Calc(this.Simulation.MathModel, 0));

                PlotEditingDialog dialog = new PlotEditingDialog(plotData);
                var dialogResult = await dialog.ShowAsync();

                if (dialogResult == ContentDialogResult.Primary)
                {
                    // add complete data points
                    ProgressBar_Plotting.Visibility = Visibility.Visible;
                    plotData.DataPoints = await calcPlotDataTask;

                    Simulation.Plots.Add(plotData);

                    ListView_Plots.ScrollIntoView(plotData);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            finally
            {
                AppBarButton_AddDiagramm.IsEnabled = true;
                ProgressBar_Plotting.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// AppBarButton focus behaviour doesn't ensure a timely binding update of the math model text property.
        /// This Method fixes this unwanted behaviour.
        /// </summary>
        private void EnsureMathModelBinding()
        {
            var focusedObject = FocusManager.GetFocusedElement();

            if (focusedObject is TextBox textBox && textBox.Tag != null && textBox.Tag.Equals("MathModel"))
            {
                this.Simulation.MathModel.Text = textBox.Text;
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

        private async void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            await AutoSaver.SaveAsync();

            AutoSaver.Subscribe(null);
        }

        private void AppBarToggleButton_AutoSave_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)AppBarToggleButton_AutoSave.IsChecked)
            {
                InfoBar_DraftModus.IsOpen = false;
                AutoSaver.Subscribe(this.Simulation);
            }
            else
            {
                InfoBar_DraftModus.IsOpen = true;
                AutoSaver.Subscribe(null);
            }
        }
    }
}
