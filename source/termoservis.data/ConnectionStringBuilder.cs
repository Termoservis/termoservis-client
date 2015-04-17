using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termoservis.Data {
	public static class ConnectionStringBuilder {
		private static readonly Dictionary<Providers, string> providerStrings =
			new Dictionary<Providers, string>() {
				{Providers.Fiscalization, "metadata=res://*/Fiscalization.FiscalizationDataModel.csdl|res://*/Fiscalization.FiscalizationDataModel.ssdl|res://*/Fiscalization.FiscalizationDataModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source={0};initial catalog=TermoservisFiscalization;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework\""},
				{Providers.Users, "metadata=res://*/Users.UsersDataModel.csdl|res://*/Users.UsersDataModel.ssdl|res://*/Users.UsersDataModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source={0};initial catalog=TermoservisUsers;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework\""}
			};


		public static string Get(Providers provider, string serverInstance) {
			if (String.IsNullOrEmpty(serverInstance))
				throw new InvalidDataException("Server Instance name is invalid");

			return String.Format(
				ConnectionStringBuilder.providerStrings[provider],
				serverInstance);
		}


		public enum Providers {
			Fiscalization,
			Users
		}
	}
}
