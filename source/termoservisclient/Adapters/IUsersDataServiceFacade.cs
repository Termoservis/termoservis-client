using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termoservis.Data.Users;

namespace TermoservisClient.Adapters {
	public interface IUsersDataServiceFacade {
		UsersDataModelContainer DataContainer { get; }
		Task OpenConnectionAsync();
	}
}
