using AAA.Utils.CloudProvider;
using IDPFLibrary;
using Xamarin.Forms;

namespace AAA.Utils.Controls
{
    public class CardListItem : ViewModelBase
    {
        private CardTypeEnum _cardType;
        private CloudTypeEnum _cloudType;

        private Command _cardMainActionCommand;
        private Command _cardSecondActionCommand;

        private object _cardMainActionCommandParameter;
        private object _cardSecondActionCommandParameter;

        private string _cardImageSource;
        private string _cardMainActionName;
        private string _cardSecondActionName;
        private string _cardSubtext;
        private string _cardTitle;



        public CardTypeEnum CardType { get => _cardType; set => SetProperty(ref _cardType, value); }
        public CloudTypeEnum CloudType { get => _cloudType; set => SetProperty(ref _cloudType, value); }
        public Command CardMainActionCommand { get => _cardMainActionCommand; set => SetProperty(ref _cardMainActionCommand, value); }
        public Command CardSecondActionCommand { get => _cardSecondActionCommand; set => SetProperty(ref _cardSecondActionCommand, value); }
        public object CardMainActionCommandParameter { get => _cardMainActionCommandParameter; set => SetProperty(ref _cardMainActionCommandParameter, value); }
        public object CardSecondActionCommandParameter { get => _cardSecondActionCommandParameter; set => SetProperty(ref _cardSecondActionCommandParameter, value); }
        public string CardImageSource { get => _cardImageSource; set => SetProperty(ref _cardImageSource, value); }
        public string CardMainActionName { get => _cardMainActionName; set => SetProperty(ref _cardMainActionName, value); }
        public string CardSecondActionName { get => _cardSecondActionName; set => SetProperty(ref _cardSecondActionName, value); }
        public string CardSubtext { get => _cardSubtext; set => SetProperty(ref _cardSubtext, value); }
        public string CardTitle { get => _cardTitle; set => SetProperty(ref _cardTitle, value); }

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
    }
}