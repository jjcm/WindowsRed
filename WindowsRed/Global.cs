//------------------------------------------------------------------------------
// <copyright file="Global.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsRed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RedditApi8;

    /// <summary>
    /// Global class.
    /// </summary>
    public class Global
    {
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
    }
}