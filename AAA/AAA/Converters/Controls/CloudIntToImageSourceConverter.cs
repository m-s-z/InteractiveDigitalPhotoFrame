using System;
using System.Globalization;
using AAA.Utils.CloudProvider;
using Xamarin.Forms;

namespace AAA.Converters.Controls
{
    /// <summary>
    /// Converts int describing type of a cloud provider
    /// into the logo image source of this provider's cloud.
    /// Implements IValueConverter interface.
    /// </summary>
    public class CloudIntToImageSourceConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts int describing type of a cloud provider
        /// into the logo image source of this provider's cloud.
        /// </summary>
        /// <param name="value">The value produced by binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Image source of the cloud's logo.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if ((int)value == 2)
            {
                return CloudInformationDictionary.GetCloudInformation(CloudTypeEnum.Dropbox).CloudLogoPath;
            }
            else if ((int)value == 3)
            {
                return CloudInformationDictionary.GetCloudInformation(CloudTypeEnum.Flickr).CloudLogoPath;
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