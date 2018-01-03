namespace Unosquare.BugTracker.Windows
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using Unosquare.BugTracker.ViewModels;

    public class PinPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Position == false) return new Thickness();

            var pos = (Position)value;
            return new Thickness { Left = pos.X, Top = pos.Y };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thickness == false) return new Position();
            var t = (Thickness)value;
            return new Position
            {
                X = t.Left,
                Y = t.Top
            };
        }
    }
}
