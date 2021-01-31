using Phileas.Controls.Pages;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace Phileas
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly List<(string Tag, Type Page)> pageTypeList = new List<(string Tag, Type Page)>()
        {
            ("home", typeof(LandingPage)),
            ("simulation", typeof(SimulationPage)),
            ("settings", typeof(SettingsPage))
        };

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                Navigate(args.InvokedItemContainer.Tag.ToString(), args.RecommendedNavigationTransitionInfo);
            }
        }

        private void Navigate(string navItemTag, NavigationTransitionInfo navTransitionInfo)
        {
            Type pageType = pageTypeList.FirstOrDefault(item => item.Tag.Equals(navItemTag)).Page;

            if (pageType != null && !pageType.Equals(contentFrame.CurrentSourcePageType))
            {
                contentFrame.Navigate(pageType, null, navTransitionInfo);
            }
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            Navigate("home", new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
        }
    }
}
