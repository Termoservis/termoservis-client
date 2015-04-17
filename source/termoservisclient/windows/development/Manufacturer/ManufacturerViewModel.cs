using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.ViewModel;
using TermoservisClient.Adapters;

namespace TermoservisClient.Windows.Development.Manufacturer {
	public class ManufacturerViewModel : NotificationObject {
		private readonly IUsersDataServiceFacade usersDataServiceFacade;


		public ManufacturerViewModel(IUsersDataServiceFacade usersDataServiceFacade) {
			if (usersDataServiceFacade == null)
				throw new ArgumentNullException("usersDataServiceFacade");
			this.usersDataServiceFacade = usersDataServiceFacade;
		}
	}
}
