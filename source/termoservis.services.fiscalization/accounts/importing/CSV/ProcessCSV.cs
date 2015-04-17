using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Termoservis.Data.Fiscalization;

namespace Termoservis.Services.Fiscalization.Accounts.Importing.CSV {
	public static class ProcessCSV {
		public const int AccountStringElementsNumber = 16;
		public const int ItemStringElementsNumber = 14;

		public static readonly Dictionary<string, ItemTypes> ItemTypesDictionary =
			new Dictionary<string, ItemTypes>() {
				{"ROBA", ItemTypes.Goods},
				{"USLUGA", ItemTypes.Service}
			};

		public static async Task<IEnumerable<AccountEntity>> ProcessAccountsAsync(
			string input, string separator) {
			// Separate elements
			var seperated = input.Split(new[] {separator, "\r\n"}, StringSplitOptions.None);

			// Check if there is right amount of elements available 
			// (1 because there is always one row empty)
			if (seperated.Length % AccountStringElementsNumber != 1)
				throw new InvalidDataException(
					"There needs to be " + AccountStringElementsNumber +
					" elements per Account to process");

			// Calculate number of accounts available
			var accountsCount = (seperated.Length - 1) / AccountStringElementsNumber;

			// Parallel call to process each individual account
			var accounts = new List<AccountEntity>(accountsCount);
			await Task.Run(() => {
				Parallel.For(0, accountsCount, i =>
					accounts.Add(ProcessAccount(seperated
						.Skip(i * AccountStringElementsNumber)
						.Take(AccountStringElementsNumber)
						.ToArray())));
			});

			return accounts;
		}

		private static AccountEntity ProcessAccount(string[] elements) {
			// Create account and assign text elements
			var account = new AccountEntity {
				StoreName = elements[(int)AccountsDescriptions.StoreName].Trim('"'),
				TreasuryName =
					elements[(int)AccountsDescriptions.TreasuryNumber].Trim('"'),
				CustomerEntity = new CustomerEntity() {
					Address = elements[(int)AccountsDescriptions.CustomerAddress].Trim('"'),
					City = elements[(int)AccountsDescriptions.CustomerCity].Trim('"'),
					FirstName = elements[(int)AccountsDescriptions.CustomerFirstName].Trim('"'),
					Name = elements[(int)AccountsDescriptions.CustomerName].Trim('"'),
					PIN = elements[(int)AccountsDescriptions.CustomerPIN].Trim('"'),
					ZIP = elements[(int)AccountsDescriptions.CustomerZIP].Trim('"')
				},
				PaymentMethod = elements[(int)AccountsDescriptions.PaymentMethod].Trim('"')
			};

			// Parse account date (required)
			string date = elements[(int)AccountsDescriptions.AccountDate].Substring(1, 26);
			DateTime accountDate;
			if (!DateTime.TryParse(
				elements[(int)AccountsDescriptions.AccountDate].Trim('"'),
				out accountDate))
				throw new InvalidCastException("Unable to parse Account's Date");
			account.Date = accountDate;

			// Parse account number (required)
			int accountNumber;
			if (!Int32.TryParse(
				elements[(int)AccountsDescriptions.AccountNumber],
				out accountNumber))
				throw new InvalidCastException("Unable to parse Account's Number");
			account.Number = accountNumber;

			// Parse account cancellation account date (optional)
			DateTime cancellationAccountDate;
			if (!DateTime.TryParse(
				elements[(int)AccountsDescriptions.CancellationAccountDate].Trim('"'),
				out cancellationAccountDate)) {
				System.Diagnostics.Debug.WriteLine(
					"Unable to parse Account's Cancellation Account Date");
				cancellationAccountDate = DateTime.MaxValue;
			}
			account.CancellationAccountDate = cancellationAccountDate;

			// Parse account cancellation account number (optional)
			int cancellationAccountNumber;
			if (!Int32.TryParse(
				elements[(int)AccountsDescriptions.CancellationAccountNumber],
				out cancellationAccountNumber))
				System.Diagnostics.Debug.WriteLine(
					"Unable to parse Account's Cancellation Account Number");
			account.CancellationAccountNumber = cancellationAccountNumber;

			return account;
		}

		public static async Task ProcessItemsAsync(
			string input, string separator, IEnumerable<AccountEntity> accounts) {
			// Separate elements
			var seperated = input.Split(new[] {separator, "\r\n"}, StringSplitOptions.None);

			// Check if there is right amount of elements available
			// (1 because there is always one row empty)
			if (seperated.Length % ItemStringElementsNumber != 1)
				throw new InvalidDataException(
					"There needs to be " + ItemStringElementsNumber +
					" elements per Item to process");

			// Calculate number of items available
			var itemsCount = (seperated.Length - 1) / ItemStringElementsNumber;

			// Parallel call to process each individual items
			await Task.Run(() => {
				Parallel.For(0, itemsCount, i => ProcessItem(seperated
					.Skip(i * ItemStringElementsNumber)
					.Take(ItemStringElementsNumber)
					.ToArray(), accounts));
			});
		}

		private static void ProcessItem(string[] elements,
			IEnumerable<AccountEntity> accounts) {
			// Retrieve account info
			int accountNumber;
			if (!Int32.TryParse(
				elements[(int)ItemsDescriptions.AccountNumber].Trim('"'),
				out accountNumber))
				throw new InvalidCastException("Unable to parse Account's Number");
			DateTime accountDate;
			if (!DateTime.TryParse(
				elements[(int)ItemsDescriptions.AccountDate].Trim('"'),
				out accountDate))
				throw new InvalidCastException("Unable to parse Account's Date");
			string storeName = elements[(int)ItemsDescriptions.StoreName].Trim('"');
			string treasuryNumber =
				elements[(int)ItemsDescriptions.TreasuryNumber].Trim('"');

			// Create item and assign text elements
			var item = new ItemEntity() {
				Code = elements[(int)ItemsDescriptions.ItemCode].Trim('"'),
				Name = elements[(int)ItemsDescriptions.ItemName].Trim('"'),
				Type =
					ItemTypesDictionary[elements[(int)ItemsDescriptions.ItemType].Trim('"')]
			};

			// Parse item amount (required)
			double amount;
			if (!Double.TryParse(
				elements[(int)ItemsDescriptions.Amount],
				out amount))
				throw new InvalidCastException("Unable to parse Item's Amount");
			item.Amount = amount;

			// Parse item discount (optional)
			double discount;
			if (!Double.TryParse(
				elements[(int)ItemsDescriptions.Discount],
				out discount))
				System.Diagnostics.Debug.WriteLine("Unable to parse Item's Discount");
			item.Discount = discount;

			// Parse item discount amount (optional)
			double discountAmount;
			if (!Double.TryParse(
				elements[(int)ItemsDescriptions.DiscountAmount],
				out discountAmount))
				System.Diagnostics.Debug.WriteLine("Unable to parse Item's Discount Amount");
			item.DiscountAmount = discountAmount;

			// Parse item price (required)
			double price;
			if (!Double.TryParse(
				elements[(int)ItemsDescriptions.Price],
				out price))
				throw new InvalidCastException("Unable to parse Item's Price");
			item.Price = price;

			// Parse item total price (required)
			double totalPrice;
			if (!Double.TryParse(
				elements[(int)ItemsDescriptions.TotalPrice],
				out totalPrice))
				throw new InvalidCastException("Unable to parse Item's Total Price");
			item.TotalPrice = totalPrice;

			// Parse item price value (optional)
			double totalPriceValue;
			if (!Double.TryParse(
				elements[(int)ItemsDescriptions.TotalPriceValue],
				out totalPriceValue))
				System.Diagnostics.Debug.WriteLine(
					"Unable to parse Item's Total Price Value");
			item.TotalPriceValue = discount;

			// Match account to this item
			var accountQuery = from a in accounts
				where a.StoreName == storeName
				where a.TreasuryName == treasuryNumber
				where a.Number == accountNumber
				where a.Date == accountDate
				select a;
			var accountMatches = accountQuery.ToList();
			if (accountMatches.Count != 1)
				throw new InvalidDataException("Item matches none or more than one account!");

			// Add item to matched account
			var firstMatchedAccount = accountMatches.FirstOrDefault();
			if (firstMatchedAccount != null) {
				item.AccountEntity = firstMatchedAccount;
				firstMatchedAccount.ItemEntities.Add(item);
			}
			else System.Diagnostics.Debug.WriteLine("Matched account is null");
		}
	}
}
