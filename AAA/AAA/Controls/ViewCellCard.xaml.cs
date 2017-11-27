using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AAA.Controls
{
    /// <summary>
    /// ViewCellCard class.
    /// Custom ViewCell in form of card.
    /// </summary>
    public partial class ViewCellCard : ViewCell
    {
        #region properties

        /// <summary>
        /// Bindable property for Command.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for CommandParameter.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for CardImageSource.
        /// </summary>
        public static readonly BindableProperty CardImageSourceProperty =
            BindableProperty.Create("CardImageSource", typeof(string), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for CardSubtext.
        /// </summary>
        public static readonly BindableProperty CardSubtextProperty =
            BindableProperty.Create("CardSubtext", typeof(string), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for CardTitle.
        /// </summary>
        public static readonly BindableProperty CardTitleProperty =
            BindableProperty.Create("CardTitle", typeof(string), typeof(ViewCellExtension));

        /// <summary>
        /// Main command to execute on button tap.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Object specified by a binding source used by Command.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Source to an image displayed on the card.
        /// </summary>
        public string CardImageSource
        {
            get => (string)GetValue(CardImageSourceProperty);
            set => SetValue(CardImageSourceProperty, value);
        }

        /// <summary>
        /// Subtext of the card.
        /// </summary>
        public string CardSubtext
        {
            get => (string)GetValue(CardSubtextProperty);
            set => SetValue(CardSubtextProperty, value);
        }

        /// <summary>
        /// Title of the card.
        /// </summary>
        public string CardTitle
        {
            get => (string)GetValue(CardTitleProperty);
            set => SetValue(CardTitleProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// ViewCellCard class constructor.
        /// </summary>
        public ViewCellCard()
        {
            InitializeComponent();
        }

        #endregion
    }
}