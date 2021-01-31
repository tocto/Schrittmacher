using Phileas.Model.MathModel;
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

namespace Phileas.Controls.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class SimulationPage : Page
    {
        ObservableCollection<Expression> expressionList = new ObservableCollection<Expression>() { new Expression() };

        ObservableCollection<string> plots = new ObservableCollection<string>();

        public SimulationPage()
        {
            this.InitializeComponent();
        }

        private void AppBarButton_AddExpression_Click(object sender, RoutedEventArgs e)
        {
            expressionList.Add(new Expression());
        }

        private void AppBarButton_AddPlot_Click(object sender, RoutedEventArgs e)
        {
            plots.Add("neu");
        }
    }
}
