using Schrittmacher.DataStorage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schrittmacher.Model
{
    public static class AutoSaver
    {
        private static bool isSaveOperationOngoing = false;

        private static  Simulation simulation = null;
        /// <summary>
        /// Subscribs the watched simulation. Only one is watch at a time.
        /// </summary>
        /// <param name="simulation">The simulation, which is going to be watched. If <code>null</code> the currently watched will be unsubscribed.</param>
        public static void Subscribe(Simulation simulationToSubscribe)
        {
            if (simulation != null)
            {
                simulation.PropertyChanged -= OnSimulationChanged;
                simulation.MathModel.PropertyChanged -= OnSimulationChanged;
                simulation.Plots.CollectionChanged -= OnSimulationChanged;
            }

            simulation = simulationToSubscribe;

            if (simulation != null)
            {
                simulation.PropertyChanged += OnSimulationChanged;
                simulation.MathModel.PropertyChanged += OnSimulationChanged;
                simulation.Plots.CollectionChanged += OnSimulationChanged;
            }
        }

        /// <summary>
        /// Ensures save operation (as foreground task).
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns></returns>
        private static async Task SaveAsync(Simulation simulation)
        {
            if (simulation == null) throw new ArgumentNullException();
            if (IsSimulationEmpty(simulation)) return;

            while (isSaveOperationOngoing) await Task.Delay(500); // wait until the resources can be handled and than ensure save operation

            isSaveOperationOngoing = true;

            try
            {
                if (simulation.Name == string.Empty) simulation.Name = "Simulation vom " + DateTime.Now.ToString("d", CultureInfo.CurrentCulture);
                await XMLWriter.Write(simulation);
                if (!App.Simulations.Contains(simulation)) App.Simulations.Add(simulation);

                await Task.Delay(3000); // Feedback for the user
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                // handle the problem, an if not possible call the user
            }
            finally
            {
                isSaveOperationOngoing = false;
            }
        }

        /// <summary>
        /// Attempts to save the currently observed simulation. If non is observed, it will do nothing.
        /// </summary>
        /// <returns></returns>
        public static async Task SaveAsync()
        {
            if (simulation != null) await SaveAsync(simulation);
        }

        private static bool IsSimulationEmpty(Simulation simulation)
        {
            return simulation.Name == string.Empty
                && simulation.MathModel.Text == string.Empty
                && simulation.Plots.Count == 0;
        }

        private static async void OnSimulationChanged(object sender, EventArgs e)
        {
            await TrySaveAsync();
        }

        /// <summary>
        /// If any save operation is in process already, the request will be skipped.
        /// </summary>
        /// <returns></returns>
        private static async Task TrySaveAsync()
        {
            if (!isSaveOperationOngoing)
            {
                await SaveAsync(simulation);
            }
        }
    }
}
