using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAA.Utils;
using Xamarin.Forms;

namespace AAA.Converters
{
    public class CloudTypeToImageSourceConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts type of a cloud provider into the image source of this provider.
        /// </summary>
        /// <param name="value">The value produced by binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Image source of a provider.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return CloudImageSourceDictionary.GetImageSource((CloudType)value);
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
