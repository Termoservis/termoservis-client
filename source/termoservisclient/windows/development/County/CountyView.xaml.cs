using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace TermoservisClient.Windows.Development.County {
	public partial class CountyView : UserControl {
		public CountyView() {
			InitializeComponent();

			// Assign ViewModel
			if (this.ViewModel == null)
				if ((this.ViewModel = this.DataContext as CountyViewModel) == null)
					throw new NullReferenceException("Invalid ViewModel");
		}

		private void AcceptNewCounty(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter || e.Key == Key.Return)
				this.ViewModel.CreateNewCounty();
		}

		private void RemoveCounty(object sender, KeyEventArgs e) {
			if (e.Key == Key.Delete) {
				this.ViewModel.RemoveSelected();
			}
		}

		#region Properties

		public CountyViewModel ViewModel { get; private set; }

		#endregion
	}
}
