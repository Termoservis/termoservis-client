using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Termoservis.Data.Users;

namespace TermoservisClient.Converters {
	public class TelephoneToStringConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is Telephone) {
				var telephoneObject = value as Telephone;
				if (telephoneObject.IsCustom)
					return telephoneObject.Number;
				else return String.Format("{2} {1} {0}",
				                     telephoneObject.Number,
				                     telephoneObject.County.CallCode.TrimStart(new[] {'0'}),
				                     telephoneObject.County.Country.CallCode.Replace("00", "+"));
			}
			return "Invalid value type or null";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}
}
