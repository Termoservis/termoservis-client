using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using TermoservisClient.Containers;
using TermoservisClient.Controls;
using TermoservisClient.Controls.CustomerDevices;
using TermoservisClient.Windows;
using TermoservisClient.Windows.Development;
using TermoservisClient.Windows.Development.Country;
using TermoservisClient.Windows.Development.County;
using TermoservisClient.Windows.Development.Manufacturer;
using TermoservisClient.Windows.Development.PostalInfoImporter;
using TermoservisClient.Windows.Development.Users;
using TermoservisClient.Windows.Development.Window;
using TermoservisClient.Windows.Fiscalization.Home;
using TermoservisClient.Windows.Fiscalization.Window;
using TermoservisClient.Windows.Login;
using TermoservisClient.Windows.Users;
using TermoservisClient.Windows.Users.NewUser;
using TermoservisClient.Windows.Users.PreviewCustomer;

namespace TermoservisClient.Providers {
	public class ViewModelProvider : IDisposable {
		private bool isDisposed;
		private readonly ViewModelContainerService containerService;


		public ViewModelProvider() {
			this.containerService = new ViewModelContainerService();
		}


		#region ViewModels

		//
		// Login
		//
		public LoginWindowViewModel LoginWindowViewModel {
			get {
				return this.containerService.Container.Resolve<LoginWindowViewModel>();
			}
		}

		//
		// Home
		//
		public HomeViewModel HomeViewModel {
			get {
				return this.containerService.Container.Resolve<HomeViewModel>();
			}
		}

		//
		// Fiscalization
		//
		public FiscalizationHomeViewModel FiscalizationHomeViewModel {
			get {
				return this.containerService.Container.Resolve<FiscalizationHomeViewModel>();
			}
		}

		public FiscalizationPreviewWindowViewModel FiscalizationPreviewWindowViewModel {
			get {
				return
					this.containerService.Container
						.Resolve<FiscalizationPreviewWindowViewModel>();
			}
		}

		//
		// Users
		//
		public NewUserViewModel NewUserViewModel {
			get {
				return this.containerService.Container.Resolve<NewUserViewModel>();
			}
		}

		public PreviewCustomerViewModel PreviewCustomerViewModel {
			get {
				return this.containerService.Container.Resolve<PreviewCustomerViewModel>();
			}
		}

		public CustomerDevicesViewModel CustomerDevicesViewModel {
			get {
				return this.containerService.Container.Resolve<CustomerDevicesViewModel>();
			}
		}

		public ContactInfoEditViewModel ContactInfoEditViewModel {
			get {
				return this.containerService.Container.Resolve<ContactInfoEditViewModel>();
			}
		}

		//
		// Development
		//
		public DevelopmentWindowViewModel DevelopmentWindowViewModel {
			get {
				return this.containerService.Container.Resolve<DevelopmentWindowViewModel>();
			}
		}

		public CountryViewModel CountryViewModel {
			get {
				return this.containerService.Container.Resolve<CountryViewModel>();
			}
		}

		public CountyViewModel CountyViewModel {
			get {
				return this.containerService.Container.Resolve<CountyViewModel>();
			}
		}

		public ManufacturerViewModel ManufacturerViewModel {
			get {
				return this.containerService.Container.Resolve<ManufacturerViewModel>();
			}
		}

		public PostalInfoImporterViewModel PostalInfoImporterViewModel {
			get {
				return
					this.containerService.Container.Resolve<PostalInfoImporterViewModel>();
			}
		}

		public UsersViewModel UsersViewModel {
			get { return this.containerService.Container.Resolve<UsersViewModel>(); }
		}

		#endregion

		#region IDisposabble implementation

		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {
			if (this.isDisposed) return;

			if (disposing) {
				this.containerService.Dispose();
			}

			this.isDisposed = true;
		}

		#endregion
	}
}
