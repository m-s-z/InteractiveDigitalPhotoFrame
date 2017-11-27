using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AAA.Utils.Controls;
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
        /// Bindable property for CardImageSource.
        /// </summary>
        public static readonly BindableProperty CardImageSourceProperty =
            BindableProperty.Create("CardImageSource", typeof(string), typeof(ViewCellCard));

        /// <summary>
        /// Bindable property for CardMainActionCommand.
        /// </summary>
        public static readonly BindableProperty CardMainActionCommandProperty =
            BindableProperty.Create("CardMainActionCommand", typeof(ICommand), typeof(ViewCellCard));

        /// <summary>
        /// Bindable property for CardMainActionCommandParameter.
        /// </summary>
        public static readonly BindableProperty CardMainActionCommandParameterProperty =
            BindableProperty.Create("CardMainActionCommandParameter", typeof(object), typeof(ViewCellCard));

        /// <summary>
        /// Bindable property for CardMainActionName.
        /// </summary>
        public static readonly BindableProperty CardMainActionNameProperty =
            BindableProperty.Create("CardMainActionName", typeof(string), typeof(ViewCellCard));

        /// <summary>
        /// Bindable property for CardSecondActionCommand.
        /// </summary>
        public static readonly BindableProperty CardSecondActionCommandProperty =
            BindableProperty.Create("CardSecondActionCommand", typeof(ICommand), typeof(ViewCellCard));

        /// <summary>
        /// Bindable property for CardSecondActionCommandParameter.
        /// </summary>
        public static readonly BindableProperty CardSecondActionCommandParameterProperty =
            BindableProperty.Create("CardSecondActionCommandParameter", typeof(object), typeof(ViewCellCard));

        /// <summary>
        /// Bindable property for CardSecondActionName.
        /// </summary>
        public static readonly BindableProperty CardSecondActionNameProperty =
            BindableProperty.Create("CardSecondActionName", typeof(string), typeof(ViewCellCard));

        /// <summary>
        /// Bindable property for CardSubtext.
        /// </summary>
        public static readonly BindableProperty CardSubtextProperty =
            BindableProperty.Create("CardSubtext", typeof(string), typeof(ViewCellCard));

        /// <summary>
        /// Bindable property for CardTitle.
        /// </summary>
        public static readonly BindableProperty CardTitleProperty =
            BindableProperty.Create("CardTitle", typeof(string), typeof(ViewCellCard));

        /// <summary>
        /// Bindable property for CardType.
        /// </summary>
        public static readonly BindableProperty CardTypeProperty =
            BindableProperty.Create("CardType", typeof(CardTypeEnum), typeof(ViewCellCard), CardTypeEnum.HighTwoActions);

        /// <summary>
        /// Type of card (short or long, one action or two).
        /// </summary>
        public CardTypeEnum CardType
        {
            get => (CardTypeEnum)GetValue(CardTypeProperty);
            set => SetValue(CardTypeProperty, value);
        }

        /// <summary>
        /// Main command to execute on main button tap.
        /// </summary>
        public ICommand CardMainActionCommand
        {
            get => (ICommand)GetValue(CardMainActionCommandProperty);
            set => SetValue(CardMainActionCommandProperty, value);
        }

        /// <summary>
        /// Second command to execute on second button tap.
        /// </summary>
        public ICommand CardSecondActionCommand
        {
            get => (ICommand)GetValue(CardSecondActionCommandProperty);
            set => SetValue(CardSecondActionCommandProperty, value);
        }

        /// <summary>
        /// Object specified by a binding source used by CardMainActionCommand.
        /// </summary>
        public object CardMainActionCommandParameter
        {
            get => GetValue(CardMainActionCommandParameterProperty);
            set => SetValue(CardMainActionCommandParameterProperty, value);
        }

        /// <summary>
        /// Object specified by a binding source used by CardSecondActionCommand.
        /// </summary>
        public object CardSecondActionCommandParameter
        {
            get => GetValue(CardSecondActionCommandParameterProperty);
            set => SetValue(CardSecondActionCommandParameterProperty, value);
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
        /// Main button name.
        /// </summary>
        public string CardMainActionName
        {
            get => (string)GetValue(CardMainActionNameProperty);
            set => SetValue(CardMainActionNameProperty, value);
        }

        /// <summary>
        /// Second button name.
        /// </summary>
        public string CardSecondActionName
        {
            get => (string)GetValue(CardSecondActionNameProperty);
            set => SetValue(CardSecondActionNameProperty, value);
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