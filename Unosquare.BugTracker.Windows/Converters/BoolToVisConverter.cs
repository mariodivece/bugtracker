namespace Unosquare.BugTracker.Windows.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class BoolToVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                if (value == null || (value is bool && (bool)value == false)) return Visibility.Hidden;
                return Visibility.Visible;
            }
            else
            {
                if (value == null || (value is bool && (bool)value == false)) return Visibility.Visible;
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
