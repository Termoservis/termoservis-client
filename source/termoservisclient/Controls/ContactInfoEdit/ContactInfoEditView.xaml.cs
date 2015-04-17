using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Termoservis.Data.Users;

namespace TermoservisClient.Controls {
	public partial class ContactInfoEditView : UserControl {
		// TODO On saving user, check if telephone number can be added and ask to add it to the list
		public ContactInfoEditView() {
			InitializeComponent();

			// Assign ViewModel
			if (this.ViewModel == null)
				if ((this.ViewModel = this.DataContext as ContactInfoEditViewModel) == null)
					throw new NullReferenceException("Invalid ViewModel");

			this.telephoneInputCountryCodeTextBox.PreviewKeyDown += AddNewTelephone;
			this.telephoneInputCountyCodeTextBox.PreviewKeyDown += AddNewTelephone;
			this.telephoneInputNumberTextBox.PreviewKeyDown += AddNewTelephone;
			this.telephonesLisBtox.PreviewKeyDown += RemoveTelephoneFromList;
		}


		public ContactInfo GetContactInfo() {
			return this.ViewModel.GetContactInfo();
		}

		private void OnLoaded(object sender, RoutedEventArgs e) {
		}

		private void AddNewTelephone(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter || e.Key == Key.Return) {
				this.ViewModel.AddTelephone(
					this.telephoneInputCountryCodeTextBox.Text,
					this.telephoneInputCountyCodeTextBox.Text,
					this.telephoneInputNumberTextBox.Text);

				this.telephoneInputNumberTextBox.Text = String.Empty;
				this.telephoneInputNumberTextBox.Focus();
			}
		}

		private void RemoveTelephoneFromList(object sender, KeyEventArgs e) {
			if (e.Key == Key.Delete) {
				this.ViewModel.RemoveTelephone(this.telephonesLisBtox.SelectedItem);
			}
		}


		#region Properties

		public ContactInfoEditViewModel ViewModel;

		#endregion
	}
}
