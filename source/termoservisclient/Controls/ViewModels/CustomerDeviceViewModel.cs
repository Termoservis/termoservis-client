using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Termoservis.Data.Users;

namespace TermoservisClient.Controls.ViewModels {
	//public class CustomerDeviceViewModel : DependencyObject {
	//	public CustomerDeviceViewModel(CustomerDevice customerDevice) {
	//		this.CustomerDevice = customerDevice;
	//	}


	//	public Service GetLastService() {
	//		if (this.CustomerDevice.Service.Count > 0) {
	//			var latestServiceQuery = from s in this.CustomerDevice.Service
	//									 orderby s.Date
	//									 select s;
	//			return latestServiceQuery.Last();
	//		}
				
	//		return null;
	//	}


	//	#region Properties

	//	public CustomerDevice CustomerDevice { get; private set; }

	//	public IEnumerable<WorkItemViewModel> WorkItems {
	//		get {
	//			var collection = new List<WorkItemViewModel>();
	//			collection.AddRange(this.CustomerDevice.Service.Select(service => new WorkItemViewModel(service)));
	//			collection.AddRange(this.CustomerDevice.Repair.Select(repair => new WorkItemViewModel(repair)));
	//			return collection.OrderByDescending(wi => wi.Model.Date);
	//		}
	//	} 

	//	public string CommissionDateString {
	//		get {
	//			if (this.CustomerDevice.CommissionDate.HasValue)
	//				return this.CustomerDevice.CommissionDate.Value.ToShortDateString();
	//			return "Nepoznato";
	//		}
	//	}

	//	public string LastServiceDateString {
	//		get {
	//			var service = this.GetLastService();
	//			if (service == null || !service.Date.HasValue)
	//				return "Nepoznato";

	//			return service.Date.Value.ToShortDateString();
	//		}
	//	}

	//	public string LastServiceEmployeeName {
	//		get {
	//			var service = this.GetLastService();
	//			if (service == null || service.Employee == null || String.IsNullOrEmpty(service.Employee.Name))
	//				return String.Empty;

	//			return service.Employee.Name;
	//		}
	//	}

	//	public ImageSource Image {
	//		get {
	//			if (this.CustomerDevice.Device.Manufacturer.Name == "Vaillant")
	//				return new BitmapImage(new Uri("/TermoservisClient;component/Resources/Images/VaillantLogo.jpg", UriKind.Relative));
	//			return null;
	//		}
	//	}

	//	public Visibility WorkItemsVisibility {
	//		get { return (Visibility)GetValue(WorkItemsVisibilityProperty); }
	//		set { SetValue(WorkItemsVisibilityProperty, value); }
	//	}
	//	public static readonly DependencyProperty WorkItemsVisibilityProperty =
	//		DependencyProperty.Register("WorkItemsVisibility", typeof(Visibility), typeof(CustomerDeviceViewModel), new PropertyMetadata(Visibility.Collapsed));

	//	#endregion
	//}
}