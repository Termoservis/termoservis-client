using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.ViewModel;
using TermoservisClient.Adapters;

namespace TermoservisClient.Windows.Development.County {
	public class CountyViewModel : NotificationObject {
		private readonly IUsersDataServiceFacade usersDataServiceFacade;
		private Termoservis.Data.Users.Country selectedCountry;
		private string name;
		private string callcode;
		private object selectedItem;


		public CountyViewModel(IUsersDataServiceFacade usersDataServiceFacade) {
			if (usersDataServiceFacade == null)
				throw new ArgumentNullException("usersDataServiceFacade");
			this.usersDataServiceFacade = usersDataServiceFacade;

			this.Counties = new ObservableCollection<Termoservis.Data.Users.County>();
		}


		public void Add(Termoservis.Data.Users.Country country, 
			string name, string callCode) {
			if (country == null)
				MessageBox.Show("Select Country first", "Error", MessageBoxButton.OK,
					MessageBoxImage.Error);

			var county = new Termoservis.Data.Users.County() {
				Name = name,
				CallCode = callCode,
				Country = country
			};

			this.usersDataServiceFacade.DataContainer.CountySet.Add(county);
			this.usersDataServiceFacade.DataContainer.SaveChanges();

			this.RefreshCounties();
		}

		public void Remove(Termoservis.Data.Users.County county) {
			if (county == null) return;

			this.usersDataServiceFacade.DataContainer.CountySet.Remove(county);
			this.usersDataServiceFacade.DataContainer.SaveChanges();

			this.RefreshCounties();
		}

		public void RefreshCounties() {
			var countiesQuery =
				from county in this.usersDataServiceFacade.DataContainer.CountySet
				where county.Country.Id == this.SelectedCountry.Id
				select county;

			this.Counties.Clear();
			this.Counties.AddRange(countiesQuery);
		}

		public void CreateNewCounty() {
			this.Add(this.SelectedCountry, this.Name, this.Callcode);
		}

		public void RemoveSelected() {
			var selectedCounty = this.SelectedItem as Termoservis.Data.Users.County;
			if (selectedCounty != null)
				this.Remove(selectedCounty);
		}

		#region Properties

		public ObservableCollection<Termoservis.Data.Users.County> Counties {
			get;
			private set;
		}

		public ObservableCollection<Termoservis.Data.Users.Country> Countries {
			get;
			private set;
		}

		public Termoservis.Data.Users.Country SelectedCountry {
			get { return this.selectedCountry; }
			set {
				this.selectedCountry = value;
				this.RaisePropertyChanged(() => this.SelectedCountry);
			}
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

		public object SelectedItem {
			get { return this.selectedItem; }
			set {
				this.selectedItem = value;
				this.RaisePropertyChanged(() => this.SelectedItem);
			}
		}

		#endregion
	}
}
