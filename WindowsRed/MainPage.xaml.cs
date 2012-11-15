//------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsRed
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;
    using WindowsRed.Views;

    /// <summary>
    /// MainPage class.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += this.FrameLoaded;
        }

        /// <summary>
        /// Frames the loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void FrameLoaded(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LinksPage));
        }
    }
}