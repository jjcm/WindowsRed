//------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsRed.Views
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

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
            this.Loaded += this.PageLoaded;
        }

        /// <summary>
        /// Event handler for when the page is loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LinksPage));
        }
    }
}