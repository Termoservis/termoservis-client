using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using Termoservis.Data.Users;
using TermoservisClient.Adapters;

namespace TermoservisClient.Windows.Users.PreviewCustomer {
	public class PreviewCustomerViewModel : NotificationObject {
		private readonly IUsersDataServiceFacade usersDataServiceFacade;
		private Customer customer;


		public PreviewCustomerViewModel(IUsersDataServiceFacade usersDataServiceFacade) {
			Contract.Requires(usersDataServiceFacade != null);
			Contract.Ensures(this.usersDataServiceFacade != null);

			this.usersDataServiceFacade = usersDataServiceFacade;

			if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
				this.InitializeForDesigner();
		}

		private void InitializeForDesigner() {
			this.Customer = new Customer() {
				Name = "Aleksandar Toplek",
				ContactInfo = new ContactInfo() {
					Address = new Address() {
						StreetLine = "Trnjanska cesta 63",
						Place = new Place() {
							Name = "Zagreb",
							PostalOffice = new PostalOffice() {
								Name = "Zagreb", 
								ZIP = "10000",
								County = new County() {
									Name = "Grad Zagreb",
									CallCode = "01",
									Country = new Country() {
										Name = "Hrvatska",
										CallCode = "+385"
									}
								}
							}
						}
					},
					Email = "aleksandar.toplek@gmail.com",
					Telephone = new Collection<Telephone>() {
						new Telephone() {
							IsCustom = true,
							Number = "+385 98 9050818"
						}
					}
				},
				Type = UserTypes.Private,
				Note = "1. kat ulaz desno",
				CustomerDevice = new Collection<CustomerDevice>() {
					new CustomerDevice() {
						CommissionDate = new DateTime(2012, 11, 21),
						Device = new Device() {
							Manufacturer = new Manufacturer() {
								Name = "Vaillant d.o.o."
							},
							ProductionYear = "2010",
							Name = "VUW 123"
						}
					}
				}
			};
		}

		#region Properties
		
		public Customer Customer {
			get { return this.customer; }
			set { this.customer = value; this.RaisePropertyChanged(() => this.Customer); }
		}

		#endregion
	}
}
