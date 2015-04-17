using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termoservis.Services.Fiscalization.Accounts.Importing.CSV {
	/// <summary>
	/// Items field descriptions
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
	/// (string) Reserved1
	/// 
	/// (string) ItemCode
	/// (string) ItemName
	/// (string) ItemType of type <see cref="Termoservis.Services.Fiscalization.Accounts.Importing.CSV.ItemTypes"/>
	/// 
	/// (double) Amount
	/// (double) Price
	/// 
	///	(double) Discount, in percentage
	/// (double) DiscountAmount
	/// 
	/// (double) TotalPrice
	///	(double) TotalPriceValue
	/// </remarks>
	public enum ItemsDescriptions : int {
		StoreName		= 0,
		TreasuryNumber	= 1,

		AccountNumber	= 2,
		AccountDate		= 3,

		Reserved1 = 4,

		ItemCode = 5,
		ItemName = 6,
		ItemType = 7,

		Amount	= 8,
		Price	= 9,

		Discount		= 10,
		DiscountAmount	= 11,

		TotalPrice		= 12,
		TotalPriceValue = 13
	}
}
