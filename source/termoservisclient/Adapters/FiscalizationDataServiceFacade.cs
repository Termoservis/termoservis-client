using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Termoservis.Data;
using Termoservis.Data.Fiscalization;

namespace TermoservisClient.Adapters {
	public class FiscalizationDataServiceFacade : IFiscalizationDataServiceFacade, IDisposable {
		private readonly IApplicationSettingsServiceFacade
			applicationSettingsServiceFacade;

		private bool isDisposed;


		public FiscalizationDataServiceFacade(IApplicationSettingsServiceFacade applicationSettingsServiceFacade) {
			if (applicationSettingsServiceFacade == null)
				throw new ArgumentNullException("applicationSettingsServiceFacade");
			this.applicationSettingsServiceFacade = applicationSettingsServiceFacade;

			// TODO Connect using saved connection string
			this.DataContainer = new FiscalizationDataModelContainer(
				ConnectionStringBuilder.Get(
					ConnectionStringBuilder.Providers.Fiscalization, 
					this.applicationSettingsServiceFacade.ServerInstanceName ?? String.Empty));
		}


		public async Task OpenConnectionAsync() {
			try {
				System.Diagnostics.Debug.WriteLine(
					"Opening connection to Fiscalization Database...");
				await this.DataContainer.Database.Connection.OpenAsync();
			}
			catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine(
					"Unable to open connection to Fiscalization Database\n{0}", (object)ex.Message);
			}
		}

		#region Properties

		public FiscalizationDataModelContainer DataContainer { get; private set; }

		#endregion

		#region IDisposabble implementation

		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {
			if (this.isDisposed) return;

			if (disposing) {
				this.DataContainer.Dispose();
			}

			this.isDisposed = true;
		}

		#endregion
	}
}
