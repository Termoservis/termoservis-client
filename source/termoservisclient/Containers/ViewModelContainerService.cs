using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using TermoservisClient.Adapters;
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
using TermoservisClient.Windows.Users.PreviewCustomer;
using CustomerDeviceViewModel = TermoservisClient.Windows.CustomerDeviceViewModel;

namespace TermoservisClient.Containers {
	public class ViewModelContainerService : IDisposable {
		private bool isDisposed;
		private readonly UnityContainer container;
		private readonly ServiceFacadeContainerService serviceFacadeContainerService;


		public ViewModelContainerService() {
			this.serviceFacadeContainerService = new ServiceFacadeContainerService();
			this.container = new UnityContainer();
			this.InitializeContainer();
		}


		private void InitializeContainer() {
			// Home
			this.container.RegisterInstance(
				new LoginWindowViewModel());
			this.container.RegisterInstance(
				new HomeViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IApplicationSettingsServiceFacade>(),
					this.serviceFacadeContainerService.Container
						.Resolve<IFiscalizationDataServiceFacade>(),
					this.serviceFacadeContainerService.Container
						.Resolve<IUsersDataServiceFacade>()));

			// Users view models
			this.container.RegisterInstance(
				new CustomerDeviceViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IUsersDataServiceFacade>()));
			this.container.RegisterInstance(
				new NewUserViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IUsersDataServiceFacade>()));
			this.container.RegisterInstance(
				new PreviewCustomerViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IUsersDataServiceFacade>()));
			this.container.RegisterInstance(
				new CustomerDevicesViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IUsersDataServiceFacade>()));
			this.container.RegisterInstance(
				new ContactInfoEditViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IUsersDataServiceFacade>()));

			// Fiscalization view models
			this.container.RegisterInstance(
				new FiscalizationHomeViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IFiscalizationDataServiceFacade>()));
			this.container.RegisterInstance(
				new FiscalizationPreviewWindowViewModel(), 
					new PerResolveLifetimeManager());

			// Development
			this.container.RegisterInstance(
				new DevelopmentWindowViewModel());
			this.container.RegisterInstance(
				new CountryViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IUsersDataServiceFacade>()));
			this.container.RegisterInstance(
				new CountyViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IUsersDataServiceFacade>()));
			this.container.RegisterInstance(
				new ManufacturerViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IUsersDataServiceFacade>()));
			this.container.RegisterInstance(
				new PostalInfoImporterViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IUsersDataServiceFacade>()));
			this.container.RegisterInstance(
				new UsersViewModel(
					this.serviceFacadeContainerService.Container
						.Resolve<IUsersDataServiceFacade>()));
		}

		#region Properties

		public UnityContainer Container {
			get { return this.container; }
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
				this.container.Dispose();
			}

			this.isDisposed = true;
		}

		#endregion
	}
}
