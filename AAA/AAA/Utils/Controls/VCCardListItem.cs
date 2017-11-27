using AAA.Models;
using AAA.Utils.CloudProvider;
using IDPFLibrary;
using Xamarin.Forms;

namespace AAA.Utils.Controls
{
    public class VCCardListItem : ViewModelBase
    {
        private CardTypeEnum _cardType;
        private Models.CloudProvider _cloudProvider;


        private Command _cardMainActionCommand;
        private Command _cardSecondActionCommand;



        public CardTypeEnum CardType { get => _cardType; set => SetProperty(ref _cardType, value); }
        public Command CardMainActionCommand { get => _cardMainActionCommand; set => SetProperty(ref _cardMainActionCommand, value); }
        public Command CardSecondActionCommand { get => _cardSecondActionCommand; set => SetProperty(ref _cardSecondActionCommand, value); }
        public Models.CloudProvider CloudProvider { get => _cloudProvider; set => SetProperty(ref _cloudProvider, value); }

        public VCCardListItem(CardTypeEnum cardType, Models.CloudProvider cloudProvider,
            Command cardMainActionCommand, Command cardSecondActionCommand = null)
        {
            CardType = cardType;
            CloudProvider = cloudProvider;
            CardMainActionCommand = cardMainActionCommand;
            CardSecondActionCommand = cardSecondActionCommand;
        }
    }
}