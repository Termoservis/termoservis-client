using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using TermoservisClient.Extensions;
using TermoservisClient.Windows.Development.Window;
using TermoservisClient.Windows.Login;
using TermoservisClient.Windows.Users.NewUser;
using TermoservisClient.Windows.Users.PreviewCustomer;

namespace TermoservisClient.Windows {
	public partial class HomeView : MetroWindow {
		private HomeViewModel viewModel;


		public HomeView() {
			InitializeComponent();

			// Assign View Model
			this.DataContext.AssignAsViewModel(ref this.viewModel);
		}

		private async void HomeWindowLoaded(object sender, RoutedEventArgs e) {
#if !DEBUG
			this.ViewModel.RequestLogIn();
#else
			(new DevelopmentWindow()).Show();
#endif

			await this.ViewModel.OnLoaded();
		}

		private void FiscalizationButtonClick(object sender, RoutedEventArgs e) {
			this.ViewModel.ShowFiscalizationWindow();
		}

		private void HomeViewOnKeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.F1)
				this.ViewModel.ShowFiscalizationWindow();
		}

		private void ShowSettingsClick(object sender, RoutedEventArgs e) {
			this.ViewModel.ShowSettingsFlyout = true;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
			(new NewUserView()).Show();
		}

		private void ButtonBase_OnClick1(object sender, RoutedEventArgs e) {
			(new PreviewCustomerView()).Show();
		}

		#region Properties

		public HomeViewModel ViewModel {
			get { return this.viewModel; }
		}

		#endregion
	}
}
