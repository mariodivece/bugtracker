namespace Unosquare.BugTracker.Windows.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value is bool == false) return Brushes.Red;
            return ((bool)value) ? Brushes.Green : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
