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

namespace Phileas.Views.UserControls
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

        private void AppBarButton_DeleteMathModelExpression_Click(object sender, RoutedEventArgs e)
        {
            var expression = (sender as FrameworkElement).DataContext as MathModelExpression;
            if (expression != null) this.MathModel.Expressions.Remove(expression);
        }

        private void TextBox_MathModelExpressions_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_MathModelExpressions_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
