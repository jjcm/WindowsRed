//------------------------------------------------------------------------------
// <copyright file="CommentControl.xaml.cs" company="non.io">
// © non.io
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsRed.Views
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
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
    /// CommentControl class.
    /// </summary>
    public sealed partial class CommentControl : UserControl
    {
        /// <summary>
        /// The comment dependency property.
        /// </summary>
        public static readonly DependencyProperty CommentProperty =
            DependencyProperty.Register(
                "Comment",
                typeof(CommentData),
                typeof(CommentControl),
                new PropertyMetadata(null, OnCommentChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentControl" /> class.
        /// </summary>
        public CommentControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        public CommentData Comment
        {
            get
            {
                return (CommentData)this.GetValue(CommentProperty);
            }

            set
            {
                this.SetValue(CommentProperty, value);
            }
        }

        /// <summary>
        /// Called when [comment changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        public static void OnCommentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CommentControl instance = d as CommentControl;
            if (instance != null)
            {
                CommentData comment = e.NewValue as CommentData;
                instance.Comment = comment;
                instance.karma.Text = (comment.Ups - comment.Downs).ToString();
                instance.author.Text = comment.Author;
                instance.body.Text = WebUtility.HtmlDecode(comment.Body);
                if (comment.Replies != null)
                {
                    // <Grid x:Name="replies" Grid.Row="2" Margin="20,0,-20,0" />
                    foreach (CommentData reply in comment.Replies.GetDataList<CommentData>())
                    {
                        // Create the reply row.
                        RowDefinition rowDefinition = new RowDefinition();
                        rowDefinition.Height = GridLength.Auto;
                        instance.container.RowDefinitions.Add(rowDefinition);

                        // Place the reply in the new row.
                        CommentControl commentControl = new CommentControl();
                        commentControl.Margin = new Thickness(20, 0, -20, 0);
                        commentControl.Comment = reply;
                        instance.container.Children.Add(commentControl);
                        Grid.SetRow(commentControl, instance.container.RowDefinitions.Count - 1);
                    }
                }
            }
        }
    }
}