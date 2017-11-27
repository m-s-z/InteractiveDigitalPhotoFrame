using System;
using System.Globalization;
using AAA.Utils.Controls;
using Xamarin.Forms;

namespace AAA.Converters.Controls
{
    /// <summary>
    /// Converts type of card into size of image displayed on card.
    /// Implements IValueConverter interface.
    /// </summary>
    public class CardTypeToSizeConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts type of card into size of image displayed on card.
        /// </summary>
        /// <param name="value">The value produced by binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Size value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 80;
            }

            switch ((CardTypeEnum)value)
            {
                case CardTypeEnum.HighOneAction:
                    return 120;
                case CardTypeEnum.HighTwoActions:
                    return 120;
                case CardTypeEnum.ShortOneAction:
                    return 80;
                case CardTypeEnum.ShortTwoActions:
                    return 80;
                default:
                    return 80;
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