using System;
using System.Globalization;
using System.Windows.Data;

namespace TwitchClientViewer.Coverters
{
    public class StreamPreviewTemplateUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return StreamPreviewTemplateUrl.Convert(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}