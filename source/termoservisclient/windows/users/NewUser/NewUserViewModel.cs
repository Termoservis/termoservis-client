using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using Termoservis.Data.Users;
using TermoservisClient.Adapters;

namespace TermoservisClient.Windows {
	public class NewUserViewModel : NotificationObject {
		private readonly IUsersDataServiceFacade usersDataServiceFacade;
		private int userTypeIndex;
		private string userName;
		private string userPIN;
		private DateTime employeeEmployedSince;
		private string manufacturerWebsite;
		private Manufacturer selectedManufacturer;


		public NewUserViewModel(IUsersDataServiceFacade usersDataServiceFacade) {
			Contract.Requires(usersDataServiceFacade != null);
			Contract.Ensures(this.usersDataServiceFacade != null);

			this.usersDataServiceFacade = usersDataServiceFacade;
		}


		public void SaveCustomer(ContactInfo contactInfo) {
			this.SaveUser<Customer>(contactInfo);
		}

		public void SaveManufacturer(ContactInfo contactInfo) {
			this.SaveUser<Manufacturer>(contactInfo);
		}

		public void SaveUser<T>(ContactInfo contactInfo) where T : User, new() {
			 Contract.Requires(contactInfo != null);

			var user = new T() {
				Name = this.UserName,
				PIN = this.UserPIN,
				Type = (UserTypes)this.UserTypeIndex,
				ContactInfo = contactInfo
			};

			if (user is Employee) {
				(user as Employee).EmployedSince = this.EmployeeEmployedSince;
			}

			if (user is Manufacturer) {
				(user as Manufacturer).Website = this.ManufacturerWebsite;
			}

			this.usersDataServiceFacade.DataContainer.UserSet.Local.Add(user);
			this.usersDataServiceFacade.DataContainer.SaveChanges();
		}

		#region Properties

		public int UserTypeIndex {
			get { return this.userTypeIndex; }
			set {
				this.userTypeIndex = value;
				this.RaisePropertyChanged(() => this.UserTypeIndex);
			}
		}

		public string UserName {
			get { return this.userName; }
			set {
				this.userName = value;
				this.RaisePropertyChanged(() => this.UserName);
			}
		}

		public string UserPIN {
			get { return this.userPIN; }
			set {
				this.userPIN = value;
				this.RaisePropertyChanged(() => this.UserPIN);
			}
		}

		public DateTime EmployeeEmployedSince {
			get { return this.employeeEmployedSince; }
			set {
				this.employeeEmployedSince = value;
				this.RaisePropertyChanged(() => this.EmployeeEmployedSince);
			}
		}

		public string ManufacturerWebsite {
			get { return this.manufacturerWebsite; }
			set {
				this.manufacturerWebsite = value;
				this.RaisePropertyChanged(() => this.ManufacturerWebsite);
			}
		}

		public Manufacturer SelectedManufacturer {
			get { return this.selectedManufacturer; }
			set {
				this.selectedManufacturer = value;
				this.RaisePropertyChanged(() => this.SelectedManufacturer);
			}
		}

		#endregion
	}
}
