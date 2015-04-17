using System;
using System.Windows;
using System.Windows.Controls;

namespace TermoservisClient.Windows.Development.Users {
	/// <summary>
	/// Interaction logic for UserWindow.xaml
	/// </summary>
	public partial class UserWindow : UserControl {
		public UserWindow() {
			InitializeComponent();
		}

		private void UserWindow_OnLoaded(object sender, RoutedEventArgs e) {
			this.ViewModel.Load();
		}

		#region Properties

		public UsersViewModel ViewModel { get; private set; }

		#endregion
	}
}
