using Phileas.Model;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Phileas.Controls.UserControls
{
    public sealed partial class MathModelView : UserControl
    {
        public MathModel MathModel { get; set; } = new MathModel();

        public MathModelView()
        {
            this.InitializeComponent();
        }

        private void AppBarButton_AddExpression_Click(object sender, RoutedEventArgs e)
        {
            MathModel.Expressions.Add(new MathModelExpression());
        }

        private void AppBarButton_Compute_Click(object sender, RoutedEventArgs e)
        {
            Calculator calculator = new Calculator();

            List<(double, double)> result = calculator.Calc(this.MathModel, this.MathModel.Expressions[1].Name, this.MathModel.Expressions[0].Name);

            foreach(var item in result)
            {
                TextBlock_TestOutputX.Text += item.Item1.ToString() + " ";
                TextBlock_TestOutputY.Text += item.Item2.ToString() + " ";
            }
            
        }

        private void AppBarButton_DeleteMathModelExpression_Click(object sender, RoutedEventArgs e)
        {
            var expression = (sender as FrameworkElement).DataContext as MathModelExpression;
            if (expression != null) this.MathModel.Expressions.Remove(expression);
        }
    }
}
