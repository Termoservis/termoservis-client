using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.ViewModel;
using Termoservis.Data.Users;
using TermoservisClient.Adapters;

namespace TermoservisClient.Windows.Development.PostalInfoImporter {
	public class PostalInfoImporterViewModel : NotificationObject {
		private readonly IUsersDataServiceFacade usersDataServiceFacade;


		public PostalInfoImporterViewModel(
			IUsersDataServiceFacade usersDataServiceFacade) {
			if (usersDataServiceFacade == null)
				throw new ArgumentNullException("usersDataServiceFacade");
			this.usersDataServiceFacade = usersDataServiceFacade;
		}


		public void ImportData(string data) {
			int totalLines = 0, currentLine = 0;

			Task.Run(() => {
				string[] lines = data.Split(new[] {'\n'});
				totalLines = lines.Length;
				foreach (string line in lines) {
					string[] columns =
						line.Replace('\r'.ToString(), "").Trim().Split(new[] {'\t'});

					int startIndex = 0;
					string countyName = columns[3].ToLower();
					while (startIndex >= 0 && startIndex + 1 < countyName.Length) {
						startIndex = countyName.IndexOf(' ', startIndex + 1);
						countyName = countyName.Remove(startIndex + 1, 1).Insert(startIndex + 1,
							columns[3][startIndex + 1].ToString(
								CultureInfo.InvariantCulture));
					}

					Termoservis.Data.Users.County county;
					if (this.usersDataServiceFacade.DataContainer.CountrySet.Any() &&
						this.usersDataServiceFacade.DataContainer.CountySet.Any(
							c => c.Name == countyName))
						county =
							this.usersDataServiceFacade.DataContainer.CountySet.Local.First(
								c => c.Name == countyName);
					else
						county = new Termoservis.Data.Users.County() {
							Country = this.usersDataServiceFacade.DataContainer.CountrySet.First(),
							Name = countyName,
							CallCode = "<Unknown>"
						};

					PostalOffice postalOffice;
					try {
						var postalOfficeQuery =
							from po in this.usersDataServiceFacade.DataContainer.PostalOfficeSet
							where po.ZIP == columns[0]
							select po;
						postalOffice = postalOfficeQuery.First();
					}
					catch (InvalidOperationException) {
						postalOffice = new PostalOffice() {
							County = county,
							Name = columns[1].Trim().Replace(" ", ""),
							ZIP = columns[0]
						};
					}

					if (this.usersDataServiceFacade.DataContainer.PlaceSet.Any(
							p => !(p.Name == columns[2].Trim() && p.PostalOffice.ZIP == postalOffice.ZIP))) {
						var place = new Place() {
							PostalOffice = postalOffice,
							Name = columns[2].Trim()
						};
						this.usersDataServiceFacade.DataContainer.PlaceSet.Add(place);
						this.usersDataServiceFacade.DataContainer.SaveChanges();
					}
					currentLine += 1;
				}
			});
		}
	}
}
