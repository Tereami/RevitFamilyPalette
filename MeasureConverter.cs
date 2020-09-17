using System;
using System.Globalization;
using System.Windows.Data;

namespace RevitFamilyPalette
{
	public class MeasureConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (double)value / 4.0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}