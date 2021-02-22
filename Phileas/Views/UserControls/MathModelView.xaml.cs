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
using MUXC = Microsoft.UI.Xaml.Controls;


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
            if (InfoBar_Validation.Message == "Modell unvollständig") InfoBar_Validation.IsOpen = false;

            CheckSyntax();
            
        }

        /// <summary>
        /// Checks the syntax. By testing there were no performance issues found, even for great equation systems.
        /// </summary>
        private async void CheckSyntax()
        {
            using (StringReader reader = new StringReader(TextBox_MathModelExpressions.Text))
            {
                InfoBar_Validation.IsOpen = !await syntaxChecker.CheckAsync(TextBox_MathModelExpressions.Text);
                InfoBar_Validation.Message = "Syntax-Fehler festegestellt";
                ToolTipService.SetToolTip(InfoBar_Validation, syntaxChecker.ErrorMessage);
            }
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MathModel mathModel)
            {
                Calculator calculator = new Calculator();


                try
                {
                    if (mathModel.Expressions.Any(m => m.MathText != string.Empty))
                    {
                        calculator.Calc(mathModel, 1);
                    }
                }
                catch
                {
                    InfoBar_Validation.Message = "Model unvollständig";
                    InfoBar_Validation.IsOpen = true;
                    ToolTipService.SetToolTip(InfoBar_Validation, "Eine Berechnung auf Grundlage dieses Systems ist nicht möglich. Haben Sie ggf. eine Zuordnung oder Anfangswerte vergessen?");
                }
            }
        }
    }
}
