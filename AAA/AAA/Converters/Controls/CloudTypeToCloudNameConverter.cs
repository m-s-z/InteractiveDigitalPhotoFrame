using System;
using System.Globalization;
using AAA.Utils.CloudProvider;
using IDPFLibrary;
using Xamarin.Forms;

namespace AAA.Converters.Controls
{
    /// <summary>
    /// Converts type of a cloud provider into the name of this cloud.
    /// Implements IValueConverter interface.
    /// </summary>
    public class CloudTypeToCloudNameConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts type of a cloud provider into the name of this cloud.
        /// </summary>
        /// <param name="value">The value produced by binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Name of the cloud.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((CloudProviderType)value == CloudProviderType.Dropbox)
            {
                return CloudInformationDictionary.GetCloudInformation(CloudTypeEnum.Dropbox).CloudName;
            }
            else if ((CloudProviderType)value == CloudProviderType.Flickr)
            {
                return CloudInformationDictionary.GetCloudInformation(CloudTypeEnum.Flickr).CloudName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Converter not implemented.
        /// </summary>
        /// <param name="value">The value produced by binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Result of a convertion.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}