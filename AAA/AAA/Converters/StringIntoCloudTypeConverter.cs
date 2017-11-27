using System;
using System.Globalization;
using AAA.Utils.CloudProvider;
using Xamarin.Forms;

namespace AAA.Converters
{
    public class StringIntoCloudTypeConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// 
        /// Converter not implemented.
        /// </summary>
        /// <param name="value">The value produced by binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Result of a convertion.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts string into type of a cloud provider.
        /// </summary>
        /// <param name="value">The value produced by binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Cloud provider type.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "Dropbox":
                    return CloudTypeEnum.Dropbox;
                case "Flickr":
                    return CloudTypeEnum.Flickr;
                case "Google Drive":
                    return CloudTypeEnum.GoogleDrive;
                case "OneDrive":
                    return CloudTypeEnum.OneDrive;
                default:
                    return CloudTypeEnum.None;
            }
            
        }

        #endregion
    }
}