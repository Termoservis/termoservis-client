using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.ViewModel;
using Termoservis.Data.Fiscalization;
using Termoservis.Services.Fiscalization.Accounts;
using Termoservis.Services.Fiscalization.Accounts.Importing.CSV;
using TermoservisClient.Adapters;
using TermoservisClient.Windows.Fiscalization.Window;

namespace TermoservisClient.Windows.Fiscalization.Home {
	public class FiscalizationHomeViewModel : NotificationObject {
		private const string StoresAllSelector = "Sve";

		private readonly IFiscalizationDataServiceFacade
			fiscalizationDataServiceFacade;

		private bool isLoading;
		private DateTime accountRangeFrom;
		private DateTime accountRangeTo;
		private int accountNumberFrom;
		private int accountNumberTo;
		private int accountNumberMin;
		private int accountNumberMax;
		private string selectedStore;
		private bool filterByAccountNumber;
		private bool filterByStore;
		private bool hasAccounts;


		public FiscalizationHomeViewModel(IFiscalizationDataServiceFacade fiscalizationDataServiceFacade) {
			if (fiscalizationDataServiceFacade == null)
				throw new ArgumentNullException("fiscalizationDataServiceFacade");
			this.fiscalizationDataServiceFacade = fiscalizationDataServiceFacade;

			this.IsLoading = true;

			// Default account stores
			this.Stores = new ObservableCollection<string>();
			this.SetStores(new List<string>());

			// Default account dates range values
			this.AccountRangeTo = new DateTime(
				DateTime.Now.Year, 
				DateTime.Now.Month,
				DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
			this.AccountRangeFrom =
					new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

			// Default account numbers values
			this.AccountNumberFrom =
				this.AccountNumberMin = 0;
			this.AccountNumberMax = 
				this.AccountNumberTo = 1;

			// Default filter settings
			this.FilterByAccountNumber = false;
			this.FilterByStore = false;
		}

		public async void LoadData() {
			await this.UpdateAccountTreasuries();
			await this.UpdateAccountNumbers();

			this.IsLoading = false;
		}

		private async Task UpdateAccountTreasuries() {
			await Task.Run(() => {
				// Retrieve treasuries
				var storesQuery =
					(from account in
						this.fiscalizationDataServiceFacade.DataContainer.AccountEntitySet
						where (account.Date >= this.accountRangeFrom &&
							   account.Date <= this.accountRangeTo)
						select account.StoreName).Distinct().ToList();

				// Set stores
				this.SetStores(storesQuery);
			});
		}

		private void SetStores(IEnumerable<string> stores) {
			if (!Application.Current.Dispatcher.CheckAccess()) {
				Application.Current.Dispatcher.Invoke(() => this.SetStores(stores));
				return;
			}

			this.Stores.Clear();
			this.Stores.Add(StoresAllSelector);
			this.Stores.AddRange(stores);
			this.SelectedStore = this.Stores.FirstOrDefault();
		}

		private async Task UpdateAccountNumbers() {
			await Task.Run(() => {
				// Retrieve account numbers
				var accountNumberMinQuery =
					(from account in
						this.fiscalizationDataServiceFacade.DataContainer.AccountEntitySet
						where (account.Date >= this.accountRangeFrom &&
							   account.Date <= this.accountRangeTo)
						orderby account.Number ascending
						select account.Number
						).FirstOrDefault();
				var accountNumberMaxQuery =
					(from account in
						this.fiscalizationDataServiceFacade.DataContainer.AccountEntitySet
						where (account.Date >= this.accountRangeFrom &&
							   account.Date <= this.accountRangeTo)
						orderby account.Number descending
						select account.Number
						).FirstOrDefault();
				if (accountNumberMinQuery == accountNumberMaxQuery &&
					accountNumberMaxQuery == 0) {
					this.HasAccounts = false;
					this.FilterByAccountNumber = false;
				}
				else {
					this.HasAccounts = true;
					this.AccountNumberMin = accountNumberMinQuery;
					this.AccountNumberMax = accountNumberMaxQuery;
					this.AccountNumberFrom = this.AccountNumberMin;
					this.AccountNumberTo = this.AccountNumberMax;
				}
			});
		}

		public async void PreviewAccounts() {
			// Show preview window
			var previewWindow = new FiscalizationPreviewWindow();
			previewWindow.Show();

			// Load filtered data to show
			await Task.Run(() => {
				// Query for data with filtering
				// TODO Separate to another method
				System.Diagnostics.Debug.WriteLine("Querying accounts data...");
				var query =
					(from accountEntity in
						this.fiscalizationDataServiceFacade.DataContainer.AccountEntitySet
						where (accountEntity.Date >= this.accountRangeFrom &&
							   accountEntity.Date <= this.accountRangeTo)
						where (!this.FilterByAccountNumber ||
							   (accountEntity.Number >= this.accountNumberFrom &&
								accountEntity.Number <= this.accountNumberTo))
						where
							(!this.FilterByStore ||
							 (this.selectedStore == StoresAllSelector ||
							  accountEntity.StoreName == this.selectedStore))
						orderby accountEntity.StoreName, accountEntity.Number ascending
						select accountEntity).ToList();
				System.Diagnostics.Debug.WriteLine("Got {0} accounts", query.Count);

				// Set retrieved data to preview window
				previewWindow.Dispatcher.Invoke(() => {
					// Set directly to View model if window is already loaded
					// in other case attach to Loaded event and than set data
					if (previewWindow.IsLoaded)
						this.SetPreviewWindowData(previewWindow.ViewModel, query);
					else
						previewWindow.Loaded +=
							(sender, args) => this.SetPreviewWindowData(previewWindow.ViewModel, query);
				});
			});
		}

		private void SetPreviewWindowData(
			FiscalizationPreviewWindowViewModel viewModel,
			IEnumerable<AccountEntity> accounts) {
			// Retrieve stores filter
			var stores = new List<string>();
			if (this.SelectedStore == StoresAllSelector) {
				var storesQuery =
					(from a in
						this.fiscalizationDataServiceFacade.DataContainer.AccountEntitySet
					select a.StoreName).Distinct().ToList();
				stores.AddRange(storesQuery);
			}
			else stores.Add(this.SelectedStore);

			// Set accounts and range for preview document
			viewModel.SetAccounts(accounts, stores);
			viewModel.SetDateRange(
				this.accountRangeFrom,
				this.accountRangeTo);
		}

		public async Task<IEnumerable<string>> ImportFrom(string[] paths) {
			// TODO Alert user which files did contain accounts
			// Loop through paths and check if there are any folders
			// add filed from folder root to path list

			var accounts = new List<AccountEntity>();
			var invalidFiles = new List<string>();

			// Go through files and 
			foreach (string filePath in paths) {
				try {
					// Get accounts and items file names
					var accountsFileName = System.IO.Path.GetTempFileName();
					var itemsFileName = System.IO.Path.GetTempFileName();

					// Extract needed files
					// TODO Separate to method
					await Task.Run(() => {
						// Open ZIP file for reading
						using (var archive = System.IO.Compression.ZipFile.OpenRead(filePath)) {
							archive.GetEntry("Fiskal1_RACUNI.csv")
								.ExtractToFile(accountsFileName, true);
							archive.GetEntry("Fiskal1_STAVKE.csv")
								.ExtractToFile(itemsFileName, true);
						}
					});

					// Read and process accounts
					using (var accountsReaderStream = File.OpenText(accountsFileName)) {
						var accountsContent = (await accountsReaderStream.ReadToEndAsync());
						accounts.AddRange(await ProcessCSV.ProcessAccountsAsync(accountsContent, ";"));
						accounts = accounts.Distinct().ToList();
					}

					// Read and process items
					using (var itemsReaderStream = File.OpenText(itemsFileName)) {
						var itemsContent = (await itemsReaderStream.ReadToEndAsync());
						await ProcessCSV.ProcessItemsAsync(itemsContent, ";", accounts);
					}
				}
				catch (Exception) {
					System.Diagnostics.Debug.WriteLine("Unable to read file: {0}", filePath);
					invalidFiles.Add(String.Format("Nevaljana datoteka na putanji \"{0}\"",
						filePath));
				}
			}

			var result = await SaveAccounts(accounts);

			// Reload data that depend on account
			this.LoadData();

			return invalidFiles.Union(result);
		}

		public async Task<IEnumerable<string>> SaveAccounts(IEnumerable<AccountEntity> accounts) {
			var duplicateAccounts = new List<string>();

			await Task.Run(() => {
				// Query colliding accounts
				System.Diagnostics.Debug.WriteLine("Querying data...");

				// Merge accounts
				System.Diagnostics.Debug.WriteLine("Merging...");
				foreach (var localAccount in accounts) {
					// Query server accounts that match local account
					// TODO Seperate to another method
					var query =
						(from accountEntity in
							this.fiscalizationDataServiceFacade.DataContainer.AccountEntitySet
							where
								localAccount.StoreName == accountEntity.StoreName &&
								localAccount.TreasuryName == accountEntity.TreasuryName &&
								localAccount.Number == accountEntity.Number &&
								localAccount.Date.Year == accountEntity.Date.Year
							select accountEntity).ToList().FirstOrDefault();


					// Only add new accounts
					if (query == null)
						this.fiscalizationDataServiceFacade.DataContainer.AccountEntitySet.Add(
							localAccount);
					else duplicateAccounts.Add(String.Format("Račun {0} već postoji", query));
				}

				// Save changes
				System.Diagnostics.Debug.WriteLine("Saving data...");
				this.fiscalizationDataServiceFacade.DataContainer.SaveChanges();
				System.Diagnostics.Debug.WriteLine("Saved.");
			});

			return duplicateAccounts;
		}

		#region Properties

		public bool IsLoading {
			get { return this.isLoading; }
			set {
				this.isLoading = value;
				this.RaisePropertyChanged(() => this.IsLoading);
				this.RaisePropertyChanged(() => this.HasLoaded);
			}
		}

		public bool HasLoaded {
			get { return !this.isLoading; }
		}

		public DateTime AccountRangeFrom {
			get { return this.accountRangeFrom; }
			set {
				this.accountRangeFrom = new DateTime(
					value.Year, value.Month, value.Day, 0, 0, 0);
				this.RaisePropertyChanged(() => this.AccountRangeFrom);

				// Update data
				if (!this.IsLoading) {
					this.IsLoading = true;
					this.LoadData();
				}
			}
		}

		public DateTime AccountRangeTo {
			get { return this.accountRangeTo; }
			set {
				this.accountRangeTo = new DateTime(
					value.Year, value.Month, value.Day, 23, 59, 59); ;
				this.RaisePropertyChanged(() => this.AccountRangeTo);

				// Update data
				if (!this.IsLoading) {
					this.IsLoading = true;
					this.LoadData();
				}
			}
		}

		public int AccountNumberFrom {
			get { return this.accountNumberFrom; }
			set {
				this.accountNumberFrom = value;
				this.RaisePropertyChanged(() => this.AccountNumberFrom);
			}
		}

		public int AccountNumberTo {
			get { return this.accountNumberTo; }
			set {
				this.accountNumberTo = value;
				this.RaisePropertyChanged(() => this.AccountNumberTo);
			}
		}

		public int AccountNumberMin {
			get { return this.accountNumberMin; }
			set {
				this.accountNumberMin = value;
				this.RaisePropertyChanged(() => this.AccountNumberMin);
			}
		}

		public int AccountNumberMax {
			get { return this.accountNumberMax; }
			set {
				this.accountNumberMax = value;
				this.RaisePropertyChanged(() => this.AccountNumberMax);
			}
		}

		public ObservableCollection<string> Stores { get; private set; }

		public string SelectedStore {
			get { return this.selectedStore; }
			set {
				this.selectedStore = value;
				this.RaisePropertyChanged(() => this.SelectedStore);
			}
		}

		public bool FilterByAccountNumber {
			get { return this.filterByAccountNumber; }
			set {
				if (this.hasAccounts)
					this.filterByAccountNumber = value;
				else this.filterByAccountNumber = false;
				this.RaisePropertyChanged(() => this.FilterByAccountNumber);
			}
		}

		public bool FilterByStore {
			get { return this.filterByStore; }
			set {
				this.filterByStore = value;
				this.RaisePropertyChanged(() => this.FilterByStore);
			}
		}

		public bool HasAccounts {
			get { return this.hasAccounts; }
			set {
				this.hasAccounts = value;
				this.RaisePropertyChanged(() => this.HasAccounts);
			}
		}

		#endregion
	}
}
