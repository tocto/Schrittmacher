using Schrittmacher.Model;
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

namespace Schrittmacher.Views.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class LandingPage : Page
    {
        private ObservableCollection<Simulation> simulations = App.Simulations;

        public LandingPage()
        {
            this.InitializeComponent();
        }

        private void AppBarButton_AddSimulation_Click(object sender, RoutedEventArgs e)
        {
            
            MainPage.Navigate(typeof(SimulationPage));
        }

        private void ListView_Simulations_ItemClick(object sender, ItemClickEventArgs e)
        {
            MainPage.Navigate(typeof(SimulationPage), e.ClickedItem);
        }
    }
}
