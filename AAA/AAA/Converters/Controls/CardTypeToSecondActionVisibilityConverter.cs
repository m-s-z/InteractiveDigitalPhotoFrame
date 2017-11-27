using System;
using System.Globalization;
using AAA.Utils.Controls;
using Xamarin.Forms;

namespace AAA.Converters.Controls
{
    /// <summary>
    /// Converts type of card into bool defining visibility of card second action button.
    /// Implements IValueConverter interface.
    /// </summary>
    public class CardTypeToSecondActionVisibilityConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts type of card into bool defining visibility of card second action button.
        /// </summary>
        /// <param name="value">The value produced by binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Bool defining visibility.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            switch ((CardTypeEnum) value)
            {
                case CardTypeEnum.HighOneAction:
                    return false;
                case CardTypeEnum.HighTwoActions:
                    return true;
                case CardTypeEnum.ShortOneAction:
                    return false;
                case CardTypeEnum.ShortTwoActions:
                    return true;
                default:
                    return false;
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