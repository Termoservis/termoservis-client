using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termoservis.Services.Fiscalization.Accounts.Importing.CSV {
	/// <summary>
	/// Account field descriptions
	/// According to Fiskal1 documentation
	/// Last updated 09.07.2013. (DD.MM.YYYY.) by Aleksandar Toplek
	/// </summary>
	/// <remarks>
	/// Filed types:
	/// 
	/// (string) StoreName
	/// (string) TreasuryName
	/// 
	/// (int) AccountNumber
	/// (DateTime) AccountDate (format: YYYY-MM-DD HH:mm:ss.nnnnnnn)
	/// 
	/// (int) CancellationAccountNumber
	/// (DateTime) CancellationAccountDate (format: YYYY-MM-DD HH:mm:ss.nnnnnnn)
	/// 
	/// (string) CustomerName (Surname)
	/// (string) CustomerFirstName
	/// (string) CustomerPIN
	/// (string) CustomerAddress
	/// (string) CustomerZIP
	/// (string) CustomerCity
	///
	/// (string) Reserved1
	/// (string) Reserved2
	/// (string) Reserved3
	///
	/// (string) PaymentMethod
	/// </remarks>
	public enum AccountsDescriptions : int {
		StoreName		= 0,
		TreasuryNumber	= 1,

		AccountNumber	= 2,
		AccountDate		= 3,
		
		CancellationAccountNumber	= 4,
		CancellationAccountDate		= 5,

		CustomerName		= 6,
		CustomerFirstName	= 7,
		CustomerPIN			= 8,
		CustomerAddress		= 9,
		CustomerZIP			= 10,
		CustomerCity		= 11,

		Reserved1 = 12,
		Reserved2 = 13,
		Reserved3 = 14,

		PaymentMethod = 15
	}
}
