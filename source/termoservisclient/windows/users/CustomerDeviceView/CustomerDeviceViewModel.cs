using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using Termoservis.Data.Users;
using TermoservisClient.Adapters;

namespace TermoservisClient.Windows {
	public class CustomerDeviceViewModel : NotificationObject {
		private IUsersDataServiceFacade usersDataServiceFacade;
		private CustomerDevice customerDevice;
		private IEnumerable<WorkItem> workItems; 


		public CustomerDeviceViewModel(IUsersDataServiceFacade usersDataServiceFacade) {
			if (usersDataServiceFacade == null)
				throw new ArgumentNullException("usersDataServiceFacade");
			this.usersDataServiceFacade = usersDataServiceFacade;
		}


		#region Properties

		public IEnumerable<WorkItem> DeviceWorkItems {
			get { return this.workItems; }
			set {
				this.workItems = value;
				this.RaisePropertyChanged(() => this.DeviceWorkItems);
			}
		}

		public CustomerDevice Device {
			get { return this.customerDevice; }
			set {
				this.customerDevice = value;
				this.RaisePropertyChanged(() => this.Device);

				// Set Work Items query
				if (value != null) {
					this.DeviceWorkItems =
						(from r in this.Device.Repair
							select r as WorkItem)
							.Union(
								from s in this.Device.Service
								select s as WorkItem);
				}
			}
		}

		#endregion
	}
}