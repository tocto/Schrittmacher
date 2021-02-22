using Schrittmacher.Views.Pages;
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

namespace Schrittmacher
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Used to handle navigation requests from other sources than the navigation view.
        /// </summary>
        private static Frame staticContentFrame = null;

        public static readonly Dictionary<string, Type> pageDic = new Dictionary<string, Type>()
        {
            { "home", typeof(LandingPage) },
            { "simulation", typeof(SimulationPage) },
            { "settings", typeof(SettingsPage) } 
        };

        public MainPage()
        {
            this.InitializeComponent();

            staticContentFrame = contentFrame;
            contentFrame.Navigated += OnContentFrameNavigated;
        }

        private void OnContentFrameNavigated(object sender, NavigationEventArgs e)
        {
            // update selected item in nav view
            if (contentFrame.SourcePageType == typeof(SettingsPage))
            {
                NavigationView.SelectedItem = NavigationView.SettingsItem;
            }
            else if (contentFrame.SourcePageType != null)
            {
                string tag = pageDic.FirstOrDefault(entry => entry.Value == e.SourcePageType).Key;

                NavigationView.SelectedItem = NavigationView.MenuItems.OfType<NavigationViewItem>().FirstOrDefault(item => item.Tag.Equals(tag));
            }
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

        public static void Navigate(Type pageType, object parameter = null)
        {
            if (staticContentFrame == null) throw new NullReferenceException("Content frame is not loaded in the static instance.");

            if (pageDic.ContainsValue(pageType)) staticContentFrame.Navigate(pageType, parameter, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            else throw new ArgumentException("Type of Page does not exist: " + pageType.ToString());
        }

        private void Navigate(string navItemTag, NavigationTransitionInfo navTransitionInfo)
        {
            Type pageType = pageDic.ContainsKey(navItemTag) ? pageDic[navItemTag] : null;

            if (pageType != null && !pageType.Equals(contentFrame.CurrentSourcePageType))
            {
                NavigationTransitionInfo navTransInfo = pageType == typeof(LandingPage) ? 
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft } :
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight };

                contentFrame.Navigate(pageType, null, navTransInfo);
            }
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            Navigate("home", new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
        }
    }
}
