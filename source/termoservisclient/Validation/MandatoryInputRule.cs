using System.Globalization;
using System.Windows.Controls;

namespace TermoservisClient.Validation {
	public class MandatoryInputRule : ValidationRule {
		public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
			var input = value as string;

			if (input != null && input.Length == 0)
				return new ValidationResult(false, "Polje ne može ostati prazno.");

			return new ValidationResult(true, null);
		}
	}
}