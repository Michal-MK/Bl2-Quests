using System;
using System.Globalization;
using System.Windows.Data;

namespace Bl2Client {
	public class GivenByToColorConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return Colors.GetColor(value.ToString()!);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return value;
		}
	}
}
