using Phileas.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// Each plot has it's specific math model, which was given, when the plot was created. 
        /// </summary>
        /// TODO: clone math models an decouble them from each other
        ObservableCollection<string> plots = new ObservableCollection<string>();

        public SimulationPage()
        {
            this.InitializeComponent();
        }

        private void AppBarButton_AddPlot_Click(object sender, RoutedEventArgs e)
        {
            plots.Add("neu");
        }
    }
}
