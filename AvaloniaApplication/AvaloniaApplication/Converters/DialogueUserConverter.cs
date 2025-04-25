using System;
using System.Globalization;
using System.Threading.Channels;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Media;
using Example;

namespace AvaloniaApplication.Converters;

public class DialogueUserConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
        {
            return b? HorizontalAlignment.Right: HorizontalAlignment.Left;
        }
        else
        {
            return HorizontalAlignment.Left;
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}

public class DialogueTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
        {
            return b? Brush.Parse("#07C160"):Brush.Parse("#C0FF3E");
        }
        else
        {
            return Brush.Parse("#FFFFFF");
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}