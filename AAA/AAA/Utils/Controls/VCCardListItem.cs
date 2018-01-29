using AAA.Models;
using AAA.Utils.CloudProvider;
using IDPFLibrary;
using IDPFLibrary.DTO.AAA.Cloud.Response;
using Xamarin.Forms;

namespace AAA.Utils.Controls
{
    /// <summary>
    /// VCCardListItem class.
    /// </summary>
    public class VCCardListItem : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing fields of CardType property.
        /// </summary>
        private CardTypeEnum _cardType;

        /// <summary>
        /// Backing field of CloudProvider property.
        /// </summary>
        private RCloud _cloudProvider;

        /// <summary>
        /// Backing field of CardMainActionCommand property.
        /// </summary>
        private Command _cardMainActionCommand;

        /// <summary>
        /// Backing field of CardSecondActionCommand property.
        /// </summary>
        private Command _cardSecondActionCommand;

        #endregion

        #region properties

        /// <summary>
        /// Property indicating type of card.
        /// </summary>
        public CardTypeEnum CardType { get => _cardType; set => SetProperty(ref _cardType, value); }

        /// <summary>
        /// Command which executes the main action on item tap. 
        /// </summary>
        public Command CardMainActionCommand { get => _cardMainActionCommand; set => SetProperty(ref _cardMainActionCommand, value); }

        /// <summary>
        /// Command which executes the secondary action on item tap. 
        /// </summary>
        public Command CardSecondActionCommand { get => _cardSecondActionCommand; set => SetProperty(ref _cardSecondActionCommand, value); }

        /// <summary>
        /// Property indicating cloud provider.
        /// </summary>
        public RCloud CloudProvider { get => _cloudProvider; set => SetProperty(ref _cloudProvider, value); }

        #endregion

        #region methods

        /// <summary>
        /// VCCardListItem class constructor.
        /// </summary>
        /// <param name="cardType">Type of a card.</param>
        /// <param name="cloudProvider">Cloud provider</param>
        /// <param name="cardMainActionCommand">Main command to execute on tap.</param>
        /// <param name="cardSecondActionCommand">Secondary command to execute on tap.</param>
        public VCCardListItem(CardTypeEnum cardType, RCloud cloudProvider,
            Command cardMainActionCommand, Command cardSecondActionCommand = null)
        {
            CardType = cardType;
            CloudProvider = cloudProvider;
            CardMainActionCommand = cardMainActionCommand;
            CardSecondActionCommand = cardSecondActionCommand;
        }

        #endregion
    }
}