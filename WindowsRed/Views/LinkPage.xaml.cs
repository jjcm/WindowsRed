//------------------------------------------------------------------------------
// <copyright file="LinkPage.xaml.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsRed.Views
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using RedditApi8;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// LinkPage class.
    /// </summary>
    public sealed partial class LinkPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkPage" /> class.
        /// </summary>
        public LinkPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is LinkData)
            {
                LinkData link = e.Parameter as LinkData;
                this.title.Text = string.Format("r/{0}", link.Subreddit);
                this.link.DataContext = link;
            }
        }
    }
}