using System.Globalization;

namespace Pomocnik.Utils;

public class CentsToPlnConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int cents)
        {
            return cents / 100.0;
        }
        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double pln)
            return (int)Math.Round(pln * 100);
        if (double.TryParse(value?.ToString(), out var d))
            return (int)Math.Round(d * 100);
        return 0;
    }
}

