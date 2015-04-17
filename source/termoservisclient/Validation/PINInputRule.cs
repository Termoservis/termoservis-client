using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TermoservisClient.Validation {
	public class PINInputRule : ValidationRule {
		public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
			var input = value as string;

			if (String.IsNullOrEmpty(input))
				return new ValidationResult(false, "Polje ne može ostati prazno.");

			if (input.Length != 11 && input.Length != 13)
				return new ValidationResult(false, "Polje mora sadržavati točno 11 ili 13 znamenki.");

			if (input.Any(t => !Char.IsDigit(t)))
				return new ValidationResult(false, "Polje može sadržavati samo znamenke.");

			if (input.Length == 11) {
				int controlNumber = -1;
				bool parsedSuccessfully = Int32.TryParse(input.Last().ToString(CultureInfo.InvariantCulture), out controlNumber);
				if (!parsedSuccessfully || !CheckOIB(input, controlNumber))
					return new ValidationResult(false, "Unesen je neispravan OIB.");
			}

			return new ValidationResult(true, null);
		}

		private bool CheckOIB(string oib, int controlNumber) {
			// Checking OIB with ISO7064, MOD 11,10

			int co = 0;
			for (int index = 0; index < oib.Length - 1; index++) {
				int currentNumber;
				bool parsedSuccessfully = Int32.TryParse(oib.ElementAt(index).ToString(CultureInfo.InvariantCulture),
				                                         out currentNumber);
				if (!parsedSuccessfully) return false;

				// Step 1, 5
				int sum;
				if (index == 0) 
					sum = currentNumber + 10;
				else sum = co + currentNumber;

				// Step 2
				int mo = sum%10;
				if (mo == 0) mo = 10;

				//Step 3, 4
				int mul = mo*2;
				co = mul%11;
			}

			if (11 - co == controlNumber)
				return true;

			return false;
		}
	}
}
