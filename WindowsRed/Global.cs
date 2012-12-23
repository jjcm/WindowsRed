//------------------------------------------------------------------------------
// <copyright file="Global.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsRed
{
    using System.Linq;
    using System.Threading.Tasks;
    using RedditApi8;
    using Windows.Security.Credentials;

    /// <summary>
    /// Global class.
    /// </summary>
    public class Global
    {
        /// <summary>
        /// The vault resource.
        /// </summary>
        private const string VaultResource = "[WindowsRed] Credentials";

        /// <summary>
        /// The user name.
        /// </summary>
        private string userName;

        /// <summary>
        /// The password.
        /// </summary>
        private string password;

        /// <summary>
        /// Initializes static members of the <see cref="Global" /> class.
        /// </summary>
        static Global()
        {
            Instance = new Global();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Global" /> class.
        /// </summary>
        public Global()
        {
            this.RedditClient = new RedditClient("WindowsRed");
            this.Vault = new PasswordVault();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Global Instance { get; private set; }

        /// <summary>
        /// Gets or sets the reddit client.
        /// </summary>
        /// <value>
        /// The reddit client.
        /// </value>
        public RedditClient RedditClient { get; set; }

        /// <summary>
        /// Gets or sets the vault.
        /// </summary>
        /// <value>
        /// The vault.
        /// </value>
        public PasswordVault Vault { get; set; }

        /// <summary>
        /// Logs in.
        /// </summary>
        /// <param name="userName">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns><c>true</c> if the log in was successful; otherwise <c>false</c>.</returns>
        public async Task<bool> LoginAsync(string userName, string password)
        {
            bool loggedIn = await this.RedditClient.LoginAsync(userName, password);
            if (loggedIn)
            {
                this.userName = userName;
                this.password = password;
                this.SaveUser();
            }

            return loggedIn;
        }

        /// <summary>
        /// Loads the user.
        /// </summary>
        /// <returns><c>true</c> if the user was loaded; otherwise <c>false</c>.</returns>
        public async Task<bool> LoadUserAsync()
        {
            try
            {
                PasswordCredential credential = this.Vault.FindAllByResource(VaultResource).FirstOrDefault();
                if (credential != null)
                {
                    this.userName = credential.UserName;
                    this.password = this.Vault.Retrieve(VaultResource, this.userName).Password;
                    await this.LoginAsync(this.userName, this.password);
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        /// <summary>
        /// Saves the user.
        /// </summary>
        public void SaveUser()
        {
            this.Vault.Add(new PasswordCredential(VaultResource, this.userName, this.password));
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        public void DeleteUser()
        {
            this.Vault.Remove(this.Vault.Retrieve(VaultResource, this.userName));
        }
    }
}