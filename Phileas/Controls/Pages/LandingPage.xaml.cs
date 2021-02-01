using System;
using System.Collections.Generic;
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

namespace Phileas.Controls.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class LandingPage : Page
    {
        public LandingPage()
        {
            this.InitializeComponent();
        }

        private void ListView_LastUsedSimulations_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListView_Recommendations.SelectedIndex = -1; // ensure selection in only one list   
        }

        private void ListView_Recommendations_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListView_LastUsedSimulations.SelectedIndex = -1; // ensure selection in only one list
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AppBarButton_OpenSelected.IsEnabled = e.AddedItems.Count > 0 ? true : false;
        }
    }
}
