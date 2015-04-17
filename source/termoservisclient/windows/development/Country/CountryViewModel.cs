using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.ViewModel;
using TermoservisClient.Adapters;

namespace TermoservisClient.Windows.Development.Country {
	public class CountryViewModel : NotificationObject {
		private readonly IUsersDataServiceFacade usersDataServiceFacade;
		private string name;
		private string callcode;
		private object selectedItem;


		public CountryViewModel(IUsersDataServiceFacade usersDataServiceFacade) {
			if (usersDataServiceFacade == null)
				throw new ArgumentNullException("usersDataServiceFacade");
			this.usersDataServiceFacade = usersDataServiceFacade;

			this.Countries = new ObservableCollection<Termoservis.Data.Users.Country>();
		}


		public void AddNewCountry(string name, string callCode) {
			var country = new Termoservis.Data.Users.Country() {
				Name = name,
				CallCode = callCode
			};

			this.usersDataServiceFacade.DataContainer.CountrySet.Add(country);
			this.usersDataServiceFacade.DataContainer.SaveChanges();
		}

		public void RemoveCountry(Termoservis.Data.Users.Country selected) {
			if (selected == null) return;

			this.usersDataServiceFacade.DataContainer.CountrySet.Remove(selected);
			this.usersDataServiceFacade.DataContainer.SaveChanges();
		}

		public void RemoveSelected() {
			var selectedCountry = this.selectedItem as Termoservis.Data.Users.Country;
			if (selectedCountry != null)
				this.RemoveCountry(selectedCountry);
		}

		public void CreateNewCountry() {
			this.AddNewCountry(this.Name, this.Callcode);

			// Clear fields
			this.Name = String.Empty;
			this.Callcode = String.Empty;
		}

		#region Properties

		public ObservableCollection<Termoservis.Data.Users.Country> Countries {
			get;
			private set;
		}

		public string Name {
			get { return this.name; }
			set {
				this.name = value;
				this.RaisePropertyChanged(() => this.Name);
			}
		}

		public string Callcode {
			get { return this.callcode; }
			set {
				this.callcode = value;
				this.RaisePropertyChanged(() => this.Callcode);
			}
		}

		public object SelectedObject {
			get { return this.selectedItem; }
			set {
				this.selectedItem = value;
				this.RaisePropertyChanged(() => this.SelectedObject);
			}
		}

		#endregion
	}
}