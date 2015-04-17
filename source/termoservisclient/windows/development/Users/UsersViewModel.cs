using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.ViewModel;
using TermoservisClient.Adapters;

namespace TermoservisClient.Windows.Development.Users {
	public class UsersViewModel : NotificationObject {
		private readonly IUsersDataServiceFacade usersDataServiceFacade;


		public UsersViewModel(IUsersDataServiceFacade usersDataServiceFacade) {
			if (usersDataServiceFacade == null)
				throw new ArgumentNullException("usersDataServiceFacade");
			this.usersDataServiceFacade = usersDataServiceFacade;
		}

		public void Load() {
			var dataQuery =
				from user in this.usersDataServiceFacade.DataContainer.UserSet
				orderby user.Name
				select new {
					User = String.Format("{0}\n({1}) {2}",
						user.Name, user.Type.ToString(), user.PIN),
					Address = String.Format("{0}\n{1} {2}\n{3}",
						user.ContactInfo.Address.StreetLine,
						user.ContactInfo.Address.Place.PostalOffice.ZIP,
						user.ContactInfo.Address.Place.Name,
						user.ContactInfo.Address.Place.PostalOffice.County.Country.Name),
					Telephones = user.ContactInfo.Telephone.Count.ToString()
				};
			this.UsersData = dataQuery;
		}

		#region Properties

		public IEnumerable<object> UsersData { get; set; }

		#endregion
	}
}
