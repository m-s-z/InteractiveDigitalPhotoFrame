using AAA.Utils.CloudProvider;
using IDPFLibrary;

namespace AAA.Models
{
    public class CloudProvider : ViewModelBase
    {
        #region fields

        private CloudTypeEnum _cloudType;
        private string _cloudEmail;

        #endregion

        #region properties

        public CloudTypeEnum CloudType
        {
            get => _cloudType;
            set => SetProperty(ref _cloudType, value);
        }
        public string CloudEmail
        {
            get => _cloudEmail;
            set => SetProperty(ref _cloudEmail, value);
        }

        #endregion

        #region methods

        public CloudProvider(CloudTypeEnum cloudType, string cloudEmail)
        {
            CloudType = cloudType;
            CloudEmail = cloudEmail;
        }

        #endregion
    }
}