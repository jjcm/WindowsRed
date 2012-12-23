//------------------------------------------------------------------------------
// <copyright file="LinkControl.xaml.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsRed.Views
{
    using System;
    using RedditApi8;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// LinkControl class.
    /// </summary>
    public sealed partial class LinkControl : UserControl
    {
        /// <summary>
        /// The link dependency property.
        /// </summary>
        public static readonly DependencyProperty LinkProperty =
            DependencyProperty.Register(
                "Link",
                typeof(LinkData),
                typeof(LinkControl),
                new PropertyMetadata(null, OnLinkChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkControl" /> class.
        /// </summary>
        public LinkControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        public LinkData Link { get; set; }

        /// <summary>
        /// Called when [link changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        public static void OnLinkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LinkControl instance = d as LinkControl;
            if (instance != null)
            {
                LinkData link = e.NewValue as LinkData;
                instance.Link = link;
                instance.karma.Text = link.Score.ToString();
                instance.title.Text = link.Title;
                instance.subredditName.Text = link.Subreddit;
                instance.numberOfComments.Text = link.NumComments.ToString();
                if (!string.IsNullOrEmpty(link.Thumbnail) && !string.Equals(link.Thumbnail, "self"))
                {
                    instance.thumbnail.Source = new BitmapImage(new Uri(link.Thumbnail));
                }
            }
        }
    }
}