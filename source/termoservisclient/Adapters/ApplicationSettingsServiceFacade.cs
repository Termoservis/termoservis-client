using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using TermoservisClient.Properties;

namespace TermoservisClient.Adapters {
	public class ApplicationSettingsServiceFacade :
		IApplicationSettingsServiceFacade, IDisposable {
		private bool isDisposed;


		#region Settings

		public string ServerInstanceName {
			get { return Settings.Default.ServerInstanceName; }
			set {
				Settings.Default.ServerInstanceName = value;
				Settings.Default.Save();
			}
		}

		#endregion

		#region Keys

		private const string ServerInstanceNameKey = "ServerInstanceName";

		#endregion


		#region IDisposabble implementation

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (this.isDisposed) return;

			if (disposing) {
				Settings.Default.Save();
			}

			this.isDisposed = true;
		}

		#endregion
	}
}
