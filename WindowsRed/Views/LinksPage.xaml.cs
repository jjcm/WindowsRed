//------------------------------------------------------------------------------
// <copyright file="LinksPage.xaml.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsRed.Views
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
    /// LinksPage class.
    /// </summary>
    public sealed partial class LinksPage : Page
    {
        /// <summary>
        /// The client.
        /// </summary>
        private RedditClient client;

        /// <summary>
        /// The links.
        /// </summary>
        private ObservableCollection<LinkData> links;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinksPage" /> class.
        /// </summary>
        public LinksPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.client = new RedditClient("WindowsRed");
            this.links = new ObservableCollection<LinkData>();
            foreach (LinkData link in await this.client.GetFrontPageAsync())
            {
                this.links.Add(link);
            }

            this.mainList.ItemsSource = this.links;
        }
    }
}