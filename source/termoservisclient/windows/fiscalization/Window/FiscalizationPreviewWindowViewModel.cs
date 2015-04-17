using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.ViewModel;
using Termoservis.Data.Fiscalization;
using Termoservis.Services.Fiscalization.Accounts;

namespace TermoservisClient.Windows.Fiscalization.Window {
	public class FiscalizationPreviewWindowViewModel : NotificationObject {
		private readonly ObservableCollection<AccountEntity> accounts;
		private bool areAccountsLoading;
		private DateTime accountsStartDate;
		private DateTime accountsEndDate;
		private double price;
		private double priceDelta;
		private double priceTotal;
		private string stores;
		// TODO Rename this to match "Rekapitulacija"
		private readonly ObservableCollection<StoreModel> storesShown; 

		public event EventHandler OnAccountsSet;


		public FiscalizationPreviewWindowViewModel() {
			this.accounts = new ObservableCollection<AccountEntity>();
			this.storesShown = new ObservableCollection<StoreModel>();
			this.AreAccountsLoading = true;
		}


		public void SetAccounts(IEnumerable<AccountEntity> accountsToAdd, IEnumerable<string> stores) {
			// Clear list of accounts and create new one
			this.accounts.Clear();
			this.accounts.AddRange(accountsToAdd);

			// Calculate prices
			this.Price = this.accounts.Sum(a => a.ItemsPriceSum);
			this.PriceDelta = this.accounts.Sum(a => a.ItemsPriceDeltaSum);
			this.PriceTotal = this.accounts.Sum(a => a.ItemsPriceTotalSum);

			// Build list of stores string
			var sb = new StringBuilder();
			foreach (var store in stores) {
				sb.Append(store);
				sb.Append(", ");
			}
			this.Stores = sb.ToString().TrimEnd(',', ' ');

			// Set list of stores shown
			this.storesShown.Clear();
			foreach (var store in stores) {
				var accountsQuery =
					(from a in this.accounts
					where a.StoreName == store
					select a).ToList();
				var storeModel = new StoreModel() {
					Name = store,
					Price = accountsQuery.Sum(a => a.ItemsPriceSum),
					PriceDelta = accountsQuery.Sum(a => a.ItemsPriceDeltaSum),
					PriceTotal = accountsQuery.Sum(a => a.ItemsPriceTotalSum)
				};
				this.storesShown.Add(storeModel);
			}

			// Mark as loaded and call event if needed
			this.AreAccountsLoading = false;
			if (this.OnAccountsSet != null)
				this.OnAccountsSet(this, null);
		}

		public void SetDateRange(DateTime startDate, DateTime endDate) {
			this.AccountsStartDate = startDate;
			this.AccountsEndDate = endDate;
		}

		#region Properties

		public IEnumerable<AccountEntity> Accounts {
			get { return this.accounts; }
		}

		public int AccountsCount {
			get { return this.accounts.Count; }
		}

		public bool AreAccountsLoading {
			get { return this.areAccountsLoading; }
			set {
				this.areAccountsLoading = value;
				this.RaisePropertyChanged(() => this.AreAccountsLoading);
				this.RaisePropertyChanged(() => this.AccountsCount);
			}
		}

		public DateTime AccountsStartDate {
			get { return this.accountsStartDate; }
			set {
				this.accountsStartDate = value;
				this.RaisePropertyChanged(() => this.AccountsStartDate);
			}
		}

		public DateTime AccountsEndDate {
			get { return this.accountsEndDate; }
			set {
				this.accountsEndDate = value;
				this.RaisePropertyChanged(() => this.AccountsEndDate);
			}
		}

		public double Price {
			get { return this.price; }
			set {
				this.price = value;
				this.RaisePropertyChanged(() => this.Price);
			}
		}

		public double PriceDelta {
			get { return this.priceDelta; }
			set {
				this.priceDelta = value;
				this.RaisePropertyChanged(() => this.PriceDelta);
			}
		}

		public double PriceTotal {
			get { return this.priceTotal; }
			set {
				this.priceTotal = value;
				this.RaisePropertyChanged(() => this.PriceTotal);
			}
		}

		public string Stores {
			get { return this.stores; }
			set {
				this.stores = value;
				this.RaisePropertyChanged(() => this.Stores);
			}
		}

		public ObservableCollection<StoreModel> StoresShown {
			get { return this.storesShown; }
		}

		#endregion
	}
}
