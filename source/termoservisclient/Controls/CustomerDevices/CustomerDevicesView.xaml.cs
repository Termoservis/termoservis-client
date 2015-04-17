using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
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
using Microsoft.Practices.Prism;
using Termoservis.Data.Users;
using TermoservisClient.Extensions;

namespace TermoservisClient.Controls.CustomerDevices {
	public partial class CustomerDevicesView : UserControl {
		private readonly CustomerDevicesViewModel viewModel;


		public CustomerDevicesView() {
			InitializeComponent();

			// Assign View Model
			this.DataContext.AssignAsViewModel(ref this.viewModel);
		}
		
		//private WorkItemViewModel lastWorkItemSelected;
		//private void WorkItemSelectionChanged(object sender, SelectionChangedEventArgs e) {
		//	if (this.lastWorkItemSelected != null)
		//		this.lastWorkItemSelected.Wrapping = TextWrapping.NoWrap;

		//	var listBox = sender as ListBox;
		//	if (listBox == null) return;

		//	var selectedWorkItem = listBox.SelectedItem as WorkItemViewModel;
		//	if (selectedWorkItem == null) return;

		//	selectedWorkItem.Wrapping = TextWrapping.Wrap;
		//	this.lastWorkItemSelected = selectedWorkItem;
		//}

		//private CustomerDeviceViewModel lastCustomerDeviceSelected;
		//private void CustomerDeviceSelectionChanged(object sender, SelectionChangedEventArgs e) {
		//	if (this.lastCustomerDeviceSelected != null)
		//		this.lastCustomerDeviceSelected.WorkItemsVisibility = Visibility.Collapsed;
		//	System.Diagnostics.Debug.Write("Selection changed...");
		//	var listBox = sender as ListBox;
		//	if (listBox == null) return;

		//	var selectedDevice = listBox.SelectedItem as CustomerDeviceViewModel;
		//	if (selectedDevice == null) return;

		//	selectedDevice.WorkItemsVisibility = Visibility.Visible;
		//	this.lastCustomerDeviceSelected = selectedDevice;
		//}

		//private void Selector_OnSelected(object sender, RoutedEventArgs e) {
		//	if (this.lastCustomerDeviceSelected != null)
		//		this.lastCustomerDeviceSelected.WorkItemsVisibility = Visibility.Collapsed;
		//	System.Diagnostics.Debug.Write("Selection changed...");
		//	var listBox = sender as ListBox;
		//	if (listBox == null) return;

		//	var selectedDevice = listBox.SelectedItem as CustomerDeviceViewModel;
		//	if (selectedDevice == null) return;

		//	selectedDevice.WorkItemsVisibility = Visibility.Visible;
		//	this.lastCustomerDeviceSelected = selectedDevice;
		//}

		public void AssignCustomerDevices(IEnumerable<CustomerDevice> devices) {
			Contract.Requires(this.ViewModel != null);

			this.ViewModel.CustomerDevices.AddRange(devices ?? new List<CustomerDevice>());
		}

		public async Task AssignCustomerAsync(int customerID) {
			Contract.Requires(this.ViewModel != null);
			Contract.Requires(customerID >= 0);

			await this.ViewModel.AssignCustomerAsync(customerID);
		}


		#region Properties

		public CustomerDevicesViewModel ViewModel { get { return this.viewModel; } }

		//public Data.Customer Customer {
		//	get { return (Data.Customer)GetValue(CustomerProperty); }
		//	set { SetValue(CustomerProperty, value); }
		//}
		//public static readonly DependencyProperty CustomerProperty =
		//	DependencyProperty.Register("Customer", typeof(Data.Customer), typeof(CustomerDevicesView), new PropertyMetadata(default(Data.Customer), CustomerChangedCallback));

		//private static void CustomerChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
		//	var view = sender as CustomerDevicesView;
		//	if (view == null) throw new NullReferenceException("Invalid event sender or null");

		//	if (!(e.NewValue is Data.Customer))
		//		throw new ArgumentException("Invalid value type or null");

		//	view.ViewModel.Customer = e.NewValue as Data.Customer;
		//}

		#endregion
	}
}
