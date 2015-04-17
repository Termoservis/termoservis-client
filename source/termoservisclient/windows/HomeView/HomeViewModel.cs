using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using Microsoft.Practices.Prism.ViewModel;
using TermoservisClient.Adapters;
using TermoservisClient.Windows.Fiscalization.Window;
using TermoservisClient.Windows.Login;

namespace TermoservisClient.Windows {
	public class HomeViewModel : NotificationObject {
		private readonly IApplicationSettingsServiceFacade
			applicationSettingsServiceFacade;

		private readonly IFiscalizationDataServiceFacade
			fiscalizationDataServiceFacade;

		private readonly IUsersDataServiceFacade
			usersDataServiceFacade;

		private string homeMessage;
		private TaskbarItemProgressState homeProgressState;
		private bool isLoading;
		private bool isConnectedToFiscalization;
		private bool isConnectedToUsers;
		private bool showSettingsFlyout;

		// Settings
		private string serverInstanceName;


		public HomeViewModel(
			IApplicationSettingsServiceFacade applicationSettingsServiceFacade,
			IFiscalizationDataServiceFacade fiscalizationDataServiceFacade,
			IUsersDataServiceFacade usersDataServiceFacade) {
			if (applicationSettingsServiceFacade == null)
				throw new ArgumentNullException("applicationSettingsServiceFacade");
			if (fiscalizationDataServiceFacade == null)
				throw new ArgumentNullException("fiscalizationDataServiceFacade");
			if (usersDataServiceFacade == null)
				throw new ArgumentNullException("usersDataServiceFacade");
			this.applicationSettingsServiceFacade = applicationSettingsServiceFacade;
			this.fiscalizationDataServiceFacade = fiscalizationDataServiceFacade;
			this.usersDataServiceFacade = usersDataServiceFacade;

			// Attach events
			App.OnStateChanged += AppOnStateChanged;

			// Set default values
			this.IsLoading = true;
			this.IsConnectedToFiscalization = false;
			this.IsConnectedToUsers = false;
		}


		public async Task OnLoaded() {
			// Initialize and load database
			App.StateType = AppStates.Loading;
			App.StateMessage = "Učitavanje...";
			this.IsLoading = true;

			// Load settings
			App.StateMessage = "Učitavanje postavki...";
			this.ServerInstanceName =
				this.applicationSettingsServiceFacade.ServerInstanceName;

			// Connect to fiscalization service provider
			App.StateMessage = "Spajanje s Fiskalizacija...";
			await this.fiscalizationDataServiceFacade.OpenConnectionAsync();
			if (this.fiscalizationDataServiceFacade.DataContainer.Database.Connection.State == ConnectionState.Open)
				this.IsConnectedToFiscalization = true;

			// Connect to users service provider
			App.StateMessage = "Spajanje s Korisnicima...";
			await this.usersDataServiceFacade.OpenConnectionAsync();
			if (this.usersDataServiceFacade.DataContainer.Database.Connection.State == ConnectionState.Open)
				this.IsConnectedToUsers = true;

			App.StateType = AppStates.Ready;
			App.StateMessage = "Spreman";
			this.IsLoading = false;
		}

		private void AppOnStateChanged(object sender, AppStateEventArgs e) {
			this.HomeMessage = e.Message;

			switch (e.State) {
				case AppStates.Updating:
				case AppStates.Loading:
				case AppStates.Processing:
					this.HomeProgressState = TaskbarItemProgressState.Indeterminate;
					break;
				case AppStates.Offline:
					this.HomeProgressState = TaskbarItemProgressState.Error;
					break;
				default:
				case AppStates.Ready:
					this.HomeProgressState = TaskbarItemProgressState.None;
					break;
			}
		}

		public void RequestLogIn() {
			var loginWindow = new LoginWindow();
			var result = loginWindow.ShowDialog();

			// Terminate application of user closed 
			// login window before he was validated
			if (!result.HasValue || !result.Value)
				Application.Current.Shutdown();
		}

		public void ShowFiscalizationWindow() {
			var fiscalizationWindow = new FiscalizationWindow();
			fiscalizationWindow.Show();
		}

		#region Properties
		
		public string HomeMessage {
			get { return this.homeMessage; }
			set {
				this.homeMessage = value;
				this.RaisePropertyChanged(() => this.HomeMessage);
			}
		}

		public TaskbarItemProgressState HomeProgressState {
			get { return this.homeProgressState; }
			set {
				this.homeProgressState = value;
				this.RaisePropertyChanged(() => this.HomeProgressState);
			}
		}

		public bool IsLoading {
			get { return this.isLoading; }
			set {
				this.isLoading = value;
				this.RaisePropertyChanged(() => this.IsLoading);
			}
		}

		public bool IsConnectedToFiscalization {
			get { return this.isConnectedToFiscalization; }
			set {
				this.isConnectedToFiscalization = value;
				this.RaisePropertyChanged(() => this.IsConnectedToFiscalization);
			}
		}

		public bool IsConnectedToUsers {
			get { return this.isConnectedToUsers; }
			set {
				this.isConnectedToUsers = value;
				this.RaisePropertyChanged(() => this.IsConnectedToUsers);
			}
		}

		public bool ShowSettingsFlyout {
			get { return this.showSettingsFlyout; }
			set {
				this.showSettingsFlyout = value;
				this.RaisePropertyChanged(() => this.ShowSettingsFlyout);
			}
		}

		public string ServerInstanceName {
			get { return this.serverInstanceName; }
			set {
				this.serverInstanceName = value;
				this.applicationSettingsServiceFacade.ServerInstanceName = value;
				this.RaisePropertyChanged(() => this.ServerInstanceName);
			}
		}

		#endregion
	}
}