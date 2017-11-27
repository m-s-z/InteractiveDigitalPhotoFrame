using System;
using System.Globalization;
using AAA.Utils.CloudProvider;
using Xamarin.Forms;

namespace AAA.Converters.Controls
{
    /// <summary>
    /// Converts type of a cloud provider into the logo image source of this provider's cloud.
    /// Implements IValueConverter interface.
    /// </summary>
    public class CloudTypeToImageSourceConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts type of a cloud provider into the logo image source of this provider's cloud.
        /// </summary>
        /// <param name="value">The value produced by binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Image source of the cloud's logo.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }

            return CloudInformationDictionary.GetCloudInformation((CloudTypeEnum) value).CloudLogoPath;
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