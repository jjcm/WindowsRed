//------------------------------------------------------------------------------
// <copyright file="LinksPage.xaml.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsRed.Views
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using RedditApi8;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
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

            await Global.Instance.LoadUserAsync();
            if (Global.Instance.RedditClient.IsLoggedIn)
            {
                this.loginButton.IsEnabled = false;
            }

            string subreddit = e.Parameter == null ? string.Empty : e.Parameter as string;
            await this.LoadStories(subreddit);
        }

        /// <summary>
        /// Loads the stories.
        /// </summary>
        /// <param name="subreddit">The subreddit.</param>
        /// <returns>Returns a task.</returns>
        private async Task LoadStories(string subreddit)
        {
            this.loading.Visibility = Visibility.Visible;
            this.subreddit = subreddit;
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

        /// <summary>
        /// Click event handler for the login button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            this.userName.Text = string.Empty;
            this.password.Password = string.Empty;
            CoreWindow window = Window.Current.CoreWindow;
            this.loginPopupBackground.Width = window.Bounds.Width + 6;
            this.loginPopup.HorizontalOffset = -3;
            this.loginPopup.VerticalOffset = (window.Bounds.Height / 2) - (180 / 2);
            this.loginPopup.IsOpen = true;
        }

        /// <summary>
        /// Event handler for when the user name text box loses focus.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void UserNameTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            this.userNameWaterMark.Visibility = Visibility.Visible;
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                this.userNameWaterMark.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Event handler for when the password text box loses focus.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void PasswordTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            this.passwordWaterMark.Visibility = Visibility.Visible;
            PasswordBox passwordBox = sender as PasswordBox;
            if (!string.IsNullOrEmpty(passwordBox.Password))
            {
                this.passwordWaterMark.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Click event handler for the popup login button.
        /// TODO: Disable login button. Popup login error. Logout.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private async void PopupLoginButtonClick(object sender, RoutedEventArgs e)
        {
            this.loginPopup.Visibility = Visibility.Collapsed;
            this.loading.Visibility = Visibility.Visible;
            await Global.Instance.LoginAsync(this.userName.Text, this.password.Password);
            await this.LoadStories(null);
        }
    }
}