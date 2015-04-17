using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TermoservisClient.Converters {
	public class InvertedBoolToVisibilityConverter : IValueConverter {
		public InvertedBoolToVisibilityConverter() {
			this.DefaultHidden = Visibility.Hidden;
		}


		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (!(value is Boolean))
				return "Invalid value type or null";

			return ((Boolean)value) ? this.DefaultHidden : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			if (!(value is Visibility))
				return "Invalid value type or null";

			return ((Visibility)value) != Visibility.Visible;
		}


		#region Properties

		public Visibility DefaultHidden { get; set; }

		#endregion
	}
}