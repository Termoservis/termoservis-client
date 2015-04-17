using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace TermoservisClient.Windows.Development.Country {
	/// <summary>
	/// Interaction logic for CountryView.xaml
	/// </summary>
	public partial class CountryView : UserControl {
		public CountryView() {
			InitializeComponent();

			// Assign ViewModel
			if (this.ViewModel == null)
				if ((this.ViewModel = this.DataContext as CountryViewModel) == null)
					throw new NullReferenceException("Invalid ViewModel");
		}

		private void RemoveCountry(object sender, KeyEventArgs e) {
			if (e.Key == Key.Delete)
				this.ViewModel.RemoveSelected();
		}

		private void AcceptNewCountry(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter || e.Key == Key.Return) 
				this.ViewModel.CreateNewCountry();
		}

		#region Properties

		public CountryViewModel ViewModel { get; private set; }

		#endregion
	}
}
