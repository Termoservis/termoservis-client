using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.ViewModel;
using Termoservis.Data.Users;
using TermoservisClient.Adapters;
using TermoservisClient.Extensions;

namespace TermoservisClient.Controls.CustomerDevices {
	public class CustomerDevicesViewModel : NotificationObject {
		private readonly IUsersDataServiceFacade usersDataServiceFacade;
		private readonly ObservableCollection<CustomerDevice> devices;
		private bool isLoadingDevices;


		public CustomerDevicesViewModel(IUsersDataServiceFacade usersDataServiceFacadeInstance) {
			Contract.Requires(usersDataServiceFacadeInstance != null);
			Contract.Ensures(this.devices != null);
			Contract.Ensures(this.usersDataServiceFacade != null);

			this.usersDataServiceFacade = usersDataServiceFacadeInstance;

			this.devices = new ObservableCollection<CustomerDevice>();

			// Create mockup models
			if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
				this.InitializeForDesigner();
		}

		private void InitializeForDesigner() {
			Contract.Requires(this.devices != null);

			this.devices.Add(new CustomerDevice() {
				CommissionDate = new DateTime(2010, 05, 27),
				Commissioner = new Commissioner() {
					Name = "Mario Toplek"
				},
				Customer = new Customer() {
					Name = "Aleksandar Toplek"
				},
				Service = new Collection<Service>(new List<Service>() {
						new Service() {
							Date = new DateTime(2013, 03, 12),
							Description = "Servis i ciscenje",
							Price = "300 kn",
							Employee = new Employee() {
								Name = "Martin Kneklin"
							}
						}
					}),
				Note = "Opis koji se moze dodati za svaki uredaj",
				Device = new Device() {
					ProductionYear = "2007",
					Name = "VUW",
					Manufacturer = new Manufacturer() {
						Name = "Vaillant d.o.o."
					}
				}
			});
		}


		public async Task AssignCustomerAsync(int customerID) {
			Contract.Requires(customerID >= 0);

			this.IsLoadingDevices = true;
			var customerDevices = await Task.Run(() => this.GetCustomerDevices(customerID));
			await this.CustomerDevices.AddRangeSafeAsync(customerDevices);
			this.IsLoadingDevices = false;
		}

		private IEnumerable<CustomerDevice> GetCustomerDevices(int customerID) {
			Contract.Requires(this.usersDataServiceFacade != null);
			Contract.Requires(customerID >= 0);

			// Retrieves all users that are Customers and have matching ID
			var devicesQuery =
				from user in this.usersDataServiceFacade.DataContainer.UserSet
				where user is Customer
				where user.Id.Equals(customerID)
				select user as Customer;

			// Take first customer and return owned devices if available
			var customer = devicesQuery.FirstOrDefault();
			if (customer != null) 
				return customer.CustomerDevice;
			else return null;
		}

		#region Properties

		public ObservableCollection<CustomerDevice> CustomerDevices { get { return this.devices; } }

		public bool IsLoadingDevices {
			get { return this.isLoadingDevices; }
			set {
				this.isLoadingDevices = value;
				this.RaisePropertyChanged(() => this.IsLoadingDevices);
			}
		}

		#endregion
	}
}
