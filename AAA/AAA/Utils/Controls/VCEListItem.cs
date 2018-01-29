    using System.Windows.Input;
using AAA.Utils.CloudProvider;
using IDPFLibrary;
using Xamarin.Forms;

namespace AAA.Utils.Controls
{
    public class VCEListItem : ViewModelBase
    {
        private Command _additionalCommand;
        private Command _mainCommand;
        private object _additionalCommandParameter;
        private object _mainCommandParameter;
        private string _additionalImageSource;
        private string _mainImageSource;
        private string _mainText;
        private CloudTypeEnum _subimageSource;
        private string _subtext;

        #region properties

        /// <summary>
        /// Additional command to execute on right side of item tap.
        /// </summary>
        public Command AdditionalCommand
        {
            get => _additionalCommand;
            set => SetProperty(ref _additionalCommand , value);
        }

        /// <summary>
        /// Main command to execute on item tap.
        /// </summary>
        public Command MainCommand
        {
            get => _mainCommand;
            set => SetProperty(ref _mainCommand , value);
        }

        /// <summary>
        /// Object specified by a binding source used by AdditionalCommand.
        /// </summary>
        public object AdditionalCommandParameter
        {
            get => _additionalCommandParameter;
            set => SetProperty(ref _additionalCommandParameter , value);
        }

        /// <summary>
        /// Object specified by a binding source used by MainCommand.
        /// </summary>
        public object MainCommandParameter
        {
            get => _mainCommandParameter;
            set => SetProperty(ref _mainCommandParameter , value);
        }

        /// <summary>
        /// Source to additional image displayed on the cell.
        /// </summary>
        public string AdditionalImageSource
        {
            get => _additionalImageSource;
            set => SetProperty(ref _additionalImageSource , value);
        }

        /// <summary>
        /// Source to main image displayed on the cell.
        /// </summary>
        public string MainImageSource
        {
            get => _mainImageSource;
            set => SetProperty(ref _mainImageSource , value);
        }

        /// <summary>
        /// Main text displayed on the cell.
        /// </summary>
        public string MainText
        {
            get => _mainText;
            set => SetProperty(ref _mainText , value);
        }

        /// <summary>
        /// Type of cloud to conver into subimage displayed on the cell.
        /// </summary>
        public CloudTypeEnum SubimageSource
        {
            get => _subimageSource;
            set => SetProperty(ref _subimageSource , value);
        }

        /// <summary>
        /// Subtext dispalyed on the cell.
        /// </summary>
        public string Subtext
        {
            get => _subtext;
            set => SetProperty(ref _subtext, value);
        }

        #endregion

        #region methods

        public VCEListItem(string mainText, Command mainCommand, string subtext,
            CloudTypeEnum subimageSource = CloudTypeEnum.None, Command additionalCommand = null, string additionalImageSource = "")
        {
            MainText = mainText;
            MainCommand = mainCommand;
            Subtext = subtext;
            SubimageSource = subimageSource;
            AdditionalImageSource = additionalImageSource;
            AdditionalCommand = additionalCommand;
        }

        #endregion
    }
}