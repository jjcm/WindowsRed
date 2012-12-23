//------------------------------------------------------------------------------
// <copyright file="LinkPage.xaml.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsRed.Views
{
    using System;
    using System.Collections.Generic;
    using RedditApi8;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
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
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is LinkData)
            {
                this.loading.Visibility = Visibility.Visible;
                LinkData link = e.Parameter as LinkData;

                // Pre-populate the information we have.
                this.link.DataContext = link;
                this.title.Text = string.Format("r/{0}", link.Subreddit);

                // Get comments and updated link information.
                Tuple<LinkData, List<CommentData>> t = await Global.Instance.RedditClient.GetCommentsAsync(link.Id);

                // Set the bindings.
                this.loading.Visibility = Visibility.Collapsed;
                this.link.DataContext = t.Item1;
                this.comments.ItemsSource = t.Item2;
            }
        }

        /// <summary>
        /// Event handler for the back button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}