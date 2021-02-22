using Schrittmacher.Model;
using System;
using System.Collections.Generic;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Schrittmacher.Views.UserControls
{
    public sealed partial class MathModelView : UserControl
    {
        SyntaxChecker syntaxChecker = new SyntaxChecker();

        public MathModelView()
        {
            this.InitializeComponent();
        }

        private void TextBox_MathModelExpressions_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_MathModelExpressions.Text != null 
                && TextBox_MathModelExpressions.Text.Trim().Length > 0 
                &&  TextBox_MathModelExpressions.Text.Last().Equals('\r'))
            {
                using (StringReader reader = new StringReader(TextBox_MathModelExpressions.Text))
                {
                    string[] lines = TextBox_MathModelExpressions.Text.Split('\r');

                    foreach (var line in lines)
                    {
                        Debug.WriteLine("Syntax: " + syntaxChecker.Check(line));
                    }
                }
            }
            
        }
    }
}
