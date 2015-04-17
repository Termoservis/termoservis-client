using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using TermoservisClient.Windows.Users;
using TermoservisClient.Windows.Users.PreviewCustomer;

namespace TermoservisClient.Windows.Users.PreviewCustomer {
	// TODO Close children windows

	public partial class PreviewCustomerView : MetroWindow {
		private readonly PreviewCustomerViewModel viewModel;

		public PreviewCustomerView() {
			InitializeComponent();

			// Assign View Model
			this.DataContext.AssignAsViewModel(ref this.viewModel);

			this.ViewModel.PropertyChanged += (sender, args) => {
				if (args.PropertyName.Equals("Customer"))
					if (this.CustomerDevicesView != null)
						this.CustomerDevicesView.AssignCustomerDevices(
							this.ViewModel.Customer.CustomerDevice);
			};
		}

		private void EmailCustomerClick(object sender, MouseButtonEventArgs e) {
			Process.Start("mailto:" + this.ViewModel.Customer.ContactInfo.Email);
		}


		#region Properties

		public PreviewCustomerViewModel ViewModel { get { return this.viewModel; } }

		#endregion
	}
}
