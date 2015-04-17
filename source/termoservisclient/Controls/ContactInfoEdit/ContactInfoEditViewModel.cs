using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using Termoservis.Data.Users;
using TermoservisClient.Adapters;
using TermoservisClient.Extensions;

namespace TermoservisClient.Controls {
	public class ContactInfoEditViewModel : NotificationObject {
		// TODO Show users of same address
		// TODO Check if address contains only initials
		private readonly IUsersDataServiceFacade usersDataServiceFacade;
		private readonly ObservableCollection<Country> availableCountries;
		private readonly ObservableCollection<County> availableCounties;
		private readonly ObservableCollection<Place> availablePlaces;
		private Country selectedCountry;
		private County selectedCounty;
		private Place selectedPlace;
		private string customerAddress;
		private string customerEmail;


		public ContactInfoEditViewModel(IUsersDataServiceFacade usersDataServiceFacade) {
			Contract.Requires(usersDataServiceFacade != null);
			Contract.Ensures(this.usersDataServiceFacade != null);

			this.usersDataServiceFacade = usersDataServiceFacade;

			this.availableCountries = new ObservableCollection<Country>();
			this.availableCounties = new ObservableCollection<County>();
			this.availablePlaces = new ObservableCollection<Place>();

			this.Load();
		}

		private async void Load() {
			await this.AvailableCountries.AddRangeSafeAsync(this.usersDataServiceFacade.DataContainer.CountrySet);
		}


		public void AddTelephone(string countryCode, string countyCode, string number) {
			// Create telephone entity
			var telephone = new Telephone() {
				County = this.SelectedCounty,
				Number = number,
				IsCustom = false
			};

			// Check if telephone is custom (mobile phone)
			if (countryCode != this.SelectedCountry.CallCode ||
				countyCode != this.SelectedCounty.CallCode) {
				telephone.Number = String.Format("{0} {1} {2}",
												 countryCode.Replace("00", "+"),
												 countyCode.TrimStart(new[] { '0' }),
												 number);
				telephone.IsCustom = true;
			}

			// Check if telephone already exists
			if (this.Telephones.Any(existing => existing.County == telephone.County &&
			                                    existing.Number == telephone.Number &&
			                                    existing.IsCustom == telephone.IsCustom)) {
				// TODO Replace this with custom window
				MessageBox.Show("Uneseni telefonski broj već postoji!", "Termoservis Client", MessageBoxButton.OK,
				                MessageBoxImage.Information);
			}
			else {
				// Add telephone to local database
				this.Telephones.Add(telephone);
			}
		}

		public void RemoveTelephone(object selectedItem) {
			var telehone = selectedItem as Telephone;
			if (telehone == null) {
				System.Diagnostics.Debug.WriteLine("Give object isn't Data.Telephone");
				return;
			}

			if (this.Telephones.Contains(telehone)) {
				this.Telephones.Remove(telehone);
			}
		}

		public ContactInfo GetContactInfo() {
			// TODO Check if new place/county/country needs to be created

			var contactInfo = new ContactInfo() {
				Email = this.CustomerEmail,
				Telephone = this.Telephones,
				Address = new Address() {
					StreetLine = this.CustomerAddress,
					Place = this.SelectedPlace
				}
			};

			if (this.SelectedPlace == null)
				return null; // Invalid contact info

			return contactInfo;
		}

		private async void SelectedCountryChangedHandle() {
			this.AvailableCounties.Clear();

			var availableCountiesQuery =
				from c in this.SelectedCountry.County
				from po in c.PostalOffice
				from p in po.Place
				orderby p.Address.Count descending, c.Name
				select c;
			await this.AvailableCounties.AddRangeSafeAsync(availableCountiesQuery);
			this.RaisePropertyChanged(() => this.SelectedCountry);
		}

		private async void SelectedCountyChangedHandle() {
			this.AvailablePlaces.Clear();

			var availablePlacesQuery = 
				from po in selectedCounty.PostalOffice
				from p in po.Place
				orderby p.Address.Count descending, p.Name
				select p;
			await this.AvailablePlaces.AddRangeSafeAsync(availablePlacesQuery);
			this.RaisePropertyChanged(() => this.SelectedCounty);
		}

		private async void SelectedPlaceChangedHandle() {
			// TODO Check if this could be used for something
			this.RaisePropertyChanged(() => this.SelectedPlace);
		}


		#region Properties

		// Telephones list
		public ObservableCollection<Telephone> Telephones { get; set; }

		// List of available Countries
		public ObservableCollection<Country> AvailableCountries {
			get { return this.availableCountries; }
		}

		// List of available Counties
		public ObservableCollection<County> AvailableCounties {
			get { return this.availableCounties; }
		}

		// List of available Places
		public ObservableCollection<Place> AvailablePlaces {
			get { return this.availablePlaces; }
		}

		// Currently selected Country
		public Country SelectedCountry {
			get { return this.selectedCountry; }
			set {
				this.selectedCountry = value;
				this.SelectedCountryChangedHandle();
			}
		}

		// Currently selected County
		public County SelectedCounty {
			get { return this.selectedCounty; }
			set {
				this.selectedCounty = value;
				this.SelectedCountyChangedHandle();
			}
		}

		// Currently selected Place
		public Place SelectedPlace {
			get { return this.selectedPlace; }
			set {
				this.selectedPlace = value;
				this.SelectedPlaceChangedHandle();
			}
		}

		// Contact address
		public string CustomerAddress {
			get { return this.customerAddress; }
			set {
				this.customerAddress = value;
				this.RaisePropertyChanged(() => this.CustomerAddress);
			}
		}

		// Contact email
		public string CustomerEmail {
			get { return this.customerEmail; }
			set {
				this.customerEmail = value;
				this.RaisePropertyChanged(() => this.CustomerEmail);
			}
		}

		// Customer website
		// NOTE Implement if necessery
		//public string CustomerWebsite {
		//	get { return (string)GetValue(CustomerWebsiteProperty); }
		//	set { SetValue(CustomerWebsiteProperty, value); }
		//}
		//public static readonly DependencyProperty CustomerWebsiteProperty =
		//	DependencyProperty.Register("CustomerWebsite", typeof(string), typeof(ContactInfoEditViewModel), new PropertyMetadata(String.Empty));

		#endregion
	}
}
