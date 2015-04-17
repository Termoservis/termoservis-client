using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using TermoservisClient.Adapters;

namespace TermoservisClient.Containers {
	public class ServiceFacadeContainerService : IDisposable {
		private bool isDisposed;
		private readonly UnityContainer container;


		public ServiceFacadeContainerService() {
			this.container = new UnityContainer();
			this.InitializeContainer();
		}

		private void InitializeContainer() {
			this.container.RegisterType<
				IApplicationSettingsServiceFacade,
				ApplicationSettingsServiceFacade>();
			this.container.RegisterType<
				IFiscalizationDataServiceFacade,
				FiscalizationDataServiceFacade>();
			this.container.RegisterType<
				IUsersDataServiceFacade,
				UsersDataServiceFacade>();
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
