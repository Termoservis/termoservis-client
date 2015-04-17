using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Termoservis.Data.Users;

namespace TermoservisClient.Windows {
	/// <summary>
	/// Interaction logic for CustomerDeviceView.xaml
	/// </summary>
	public partial class CustomerDeviceView : MetroWindow {
		public CustomerDeviceView(CustomerDevice device) {
			InitializeComponent();

			// Assign ViewModel
			if (this.ViewModel == null)
				if ((this.ViewModel = this.DataContext as CustomerDeviceViewModel) == null)
					throw new NullReferenceException("Invalid ViewModel");

			this.ViewModel.Device = device;
		}

		#region Properties

		public CustomerDeviceViewModel ViewModel { get; private set; }

		#endregion
	}
}
