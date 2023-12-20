using System;
using System.Globalization;

namespace App.Converters;

public class Base64ImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var base64Image = value as string;
        if (!string.IsNullOrEmpty(base64Image))
        {
            byte[] imageBytes = System.Convert.FromBase64String(base64Image);
            return ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
