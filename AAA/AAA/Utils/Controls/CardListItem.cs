using AAA.Utils.CloudProvider;
using IDPFLibrary;
using Xamarin.Forms;

namespace AAA.Utils.Controls
{
    /// <summary>
    /// CardListItem class.
    /// </summary>
    public class CardListItem : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field of CardType property.
        /// </summary>
        private CardTypeEnum _cardType;

        /// <summary>
        /// Backing field of CloudType property.
        /// </summary>
        private CloudTypeEnum _cloudType;

        /// <summary>
        /// Backing field of CardMainActionCommand property.
        /// </summary>
        private Command _cardMainActionCommand;

        /// <summary>
        /// Backing field of CardSecondActionCommand property.
        /// </summary>
        private Command _cardSecondActionCommand;

        /// <summary>
        /// Backing field of CardMainActionCommandParameter property.
        /// </summary>
        private object _cardMainActionCommandParameter;

        /// <summary>
        /// Backing field of CardSecondActionCommandParameter property.
        /// </summary>
        private object _cardSecondActionCommandParameter;

        /// <summary>
        /// Backing field of CardImageSource property.
        /// </summary>
        private string _cardImageSource;

        /// <summary>
        /// Backing field of CardMainActionName property.
        /// </summary>
        private string _cardMainActionName;

        /// <summary>
        /// Backing field of CardSecondActionName property.
        /// </summary>
        private string _cardSecondActionName;

        /// <summary>
        /// Backing field of CardSubtext property.
        /// </summary>
        private string _cardSubtext;

        /// <summary>
        /// Backing field of CardTitle property.
        /// </summary>
        private string _cardTitle;

        #endregion

        #region properties

        /// <summary>
        /// Property indicating type of card.
        /// </summary>
        public CardTypeEnum CardType { get => _cardType; set => SetProperty(ref _cardType, value); }

        /// <summary>
        /// Property indicating type of cloud.
        /// </summary>
        public CloudTypeEnum CloudType { get => _cloudType; set => SetProperty(ref _cloudType, value); }

        /// <summary>
        /// Command which executes the main action on item tap. 
        /// </summary>
        public Command CardMainActionCommand { get => _cardMainActionCommand; set => SetProperty(ref _cardMainActionCommand, value); }

        /// <summary>
        /// Command which executes the secondary action on item tap. 
        /// </summary>
        public Command CardSecondActionCommand { get => _cardSecondActionCommand; set => SetProperty(ref _cardSecondActionCommand, value); }

        /// <summary>
        /// Object passed to the main command as a parameter.
        /// </summary>
        public object CardMainActionCommandParameter { get => _cardMainActionCommandParameter; set => SetProperty(ref _cardMainActionCommandParameter, value); }

        /// <summary>
        /// Object passed to the secondary command as a parameter.
        /// </summary>
        public object CardSecondActionCommandParameter { get => _cardSecondActionCommandParameter; set => SetProperty(ref _cardSecondActionCommandParameter, value); }

        /// <summary>
        /// Path to image to display.
        /// </summary>
        public string CardImageSource { get => _cardImageSource; set => SetProperty(ref _cardImageSource, value); }

        /// <summary>
        /// Text of a main action button.
        /// </summary>
        public string CardMainActionName { get => _cardMainActionName; set => SetProperty(ref _cardMainActionName, value); }

        /// <summary>
        /// Text of a secondary action button.
        /// </summary>
        public string CardSecondActionName { get => _cardSecondActionName; set => SetProperty(ref _cardSecondActionName, value); }

        /// <summary>
        /// Subtext displayed on a card.
        /// </summary>
        public string CardSubtext { get => _cardSubtext; set => SetProperty(ref _cardSubtext, value); }

        /// <summary>
        /// Title of a card.
        /// </summary>
        public string CardTitle { get => _cardTitle; set => SetProperty(ref _cardTitle, value); }

        #endregion

        #region methods

        /// <summary>
        /// CardListItem class constructor.
        /// </summary>
        /// <param name="cardType">Type of a card.</param>
        /// <param name="cardTitle">Title of a card.</param>
        /// <param name="cardMainActionCommand">Main command to execute on tap.</param>
        /// <param name="cardMainActionName">Text of a main command button.</param>
        /// <param name="cardImageSource">Path to an image of card.</param>
        /// <param name="cardSubtext">Subtext to display on card.</param>
        /// <param name="cardSecondActionCommand">Secondary command to execute on tap.</param>
        /// <param name="cardSecondActionName">Text of a secondary command button.</param>
        public CardListItem(CardTypeEnum cardType, string cardTitle,
            Command cardMainActionCommand, string cardMainActionName, string cardImageSource, string cardSubtext = "",
            Command cardSecondActionCommand = null, string cardSecondActionName = "")
        {
            CardType = cardType;
            CardTitle = cardTitle;
            CardMainActionCommand = cardMainActionCommand;
            CardMainActionName = cardMainActionName;
            CardSubtext = cardSubtext;
            CardSecondActionCommand = cardSecondActionCommand;
            CardSecondActionName = cardSecondActionName;
            CardImageSource = cardImageSource;
        }

        /// <summary>
        /// CardListItem class constructor.
        /// </summary>
        /// <param name="cardType">Type of a card.</param>
        /// <param name="cardTitle">Title of a card.</param>
        /// <param name="cardMainActionCommand">Main command to execute on tap.</param>
        /// <param name="cardMainActionName">Text of a main command button.</param>
        /// <param name="cloudType">Type of a cloud.</param>
        /// <param name="cardSubtext">Subtext to display on card.</param>
        /// <param name="cardSecondActionCommand">Secondary command to execute on tap.</param>
        /// <param name="cardSecondActionName">Text of a secondary command button.</param>
        public CardListItem(CardTypeEnum cardType, string cardTitle,
            Command cardMainActionCommand, string cardMainActionName, CloudTypeEnum cloudType, string cardSubtext = "",
            Command cardSecondActionCommand = null, string cardSecondActionName = "")
        {
            CardType = cardType;
            CardTitle = cardTitle;
            CardMainActionCommand = cardMainActionCommand;
            CardMainActionName = cardMainActionName;
            CardSubtext = cardSubtext;
            CardSecondActionCommand = cardSecondActionCommand;
            CardSecondActionName = cardSecondActionName;
            CloudType = cloudType;
        }

        #endregion
    }
}