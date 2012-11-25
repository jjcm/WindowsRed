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
    using System.Diagnostics;
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
        /// The links.
        /// </summary>
        private ObservableCollection<LinkData> links;

        /// <summary>
        /// The subreddit used to display the title of the page.
        /// </summary>
        private string subreddit;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinksPage" /> class.
        /// </summary>
        public LinksPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            this.loading.Visibility = Visibility.Visible;
            this.subreddit = e.Parameter == null ? string.Empty : e.Parameter as string;
            this.Title.Text = string.IsNullOrEmpty(this.subreddit) ? "reddit" : string.Format("r/{0}", this.subreddit);
            this.links = new ObservableCollection<LinkData>();
            List<LinkData> links = await Global.Instance.RedditClient.GetPageAsync(this.subreddit);

            if (links != null)
            {
                foreach (LinkData link in links)
                {
                    this.links.Add(link);
                }
            }

            this.loading.Visibility = Visibility.Collapsed;
            this.mainList.ItemsSource = this.links;
        }

        /// <summary>
        /// Click event handler for MainList.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs" /> instance containing the event data.</param>
        private void MainListItemClick(object sender, ItemClickEventArgs e)
        {
            LinkData selectedLink = e.ClickedItem as LinkData;
            this.Frame.Navigate(typeof(LinkPage), selectedLink);
        }
    }
}