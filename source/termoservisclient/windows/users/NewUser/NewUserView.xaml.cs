using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Termoservis.Data.Users;
using TermoservisClient.Extensions;
using TermoservisClient.Windows;

namespace TermoservisClient.Windows.Users.NewUser {
	public partial class NewUserView : MetroWindow {
		// TODO Check if user with same initials or exactly same name exists
		// TODO Check if user with same PIN exists
		private NewUserViewModel viewModel;

		public NewUserView() {
			InitializeComponent();

			// Assign View Model
			this.DataContext.AssignAsViewModel(ref this.viewModel);
		}

		private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			this.ViewModel.SaveManufacturer(this.CustomerEditContactInfoView.GetContactInfo());
		}

		private void UserTypeChanged(object sender, SelectionChangedEventArgs e) {
			if (!this.IsLoaded) return;

			if (this.ViewModel.UserTypeIndex == (int)UserTypes.Private) {
				this.CustomerNameLabel.Content = "Ime i Prezime";
				this.CustomerPINTextBox.IsEnabled = false;
			}
			else {
				this.CustomerNameLabel.Content = "Naziv";
				this.CustomerPINTextBox.IsEnabled = true;
			}
		}


		#region Properties

		public NewUserViewModel ViewModel { get { return this.viewModel; } }

		#endregion
	}
}
