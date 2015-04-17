using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Termoservis.Services.Fiscalization.Accounts;
using Termoservis.Services.Fiscalization.Accounts.Importing.CSV;

namespace TermoservisClient.Windows.Fiscalization.Home {
	/// <summary>
	/// Interaction logic for FiscalizationHomeView.xaml
	/// </summary>
	public partial class FiscalizationHomeView : UserControl {
		public FiscalizationHomeView() {
			InitializeComponent();

			// Assign ViewModel
			if (this.ViewModel == null)
				if ((this.ViewModel = this.DataContext as FiscalizationHomeViewModel) == null)
					throw new NullReferenceException("Invalid ViewModel");
		}

		private void FiscalizationHomeViewOnLoaded(object sender, RoutedEventArgs e) {
			this.ViewModel.LoadData();
		}

		private void PreviewAccounts(object sender, RoutedEventArgs e) {
			this.ViewModel.PreviewAccounts();
		}

		#region Account import

		private async void OnImportDroped(object sender, DragEventArgs e) {
			if (!e.Data.GetDataPresent(DataFormats.FileDrop))
				return;

			// Retrieve dropped file names
			var files = (string[])e.Data.GetData(DataFormats.FileDrop);

			// Import accounts and return result
			var result = (await this.ViewModel.ImportFrom(files)).ToList();
			if (result.Any()) {
				// Build user text
				var sb = new StringBuilder();
				sb.AppendLine("Prilikom uvoza pojavile su se neke zapreke: \n");
				foreach (var message in result)
					sb.AppendLine(message);

				// Show message box
				MessageBox.Show(sb.ToString(), "Fiskalizacija - Termoservis",
					MessageBoxButton.OK, MessageBoxImage.Information);
			}

			// Reset drop area to default
			var rectangle = sender as Rectangle;
			if (rectangle == null)
				System.Diagnostics.Debug.WriteLine(
					"Couldn't retrieve rectangle object on Drop");
			else this.ResetDragDropArea(rectangle);
		}

		private void UIElement_OnDragOver(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effects = DragDropEffects.Link;
				var rectangle = sender as Rectangle;
				if (rectangle != null)
					rectangle.Fill = new SolidColorBrush(Color.FromArgb(25, 255, 0, 0));
			}
			else {
				e.Effects = DragDropEffects.None;
			}
		}

		private void UIElement_OnDragLeave(object sender, DragEventArgs e) {
			var rectangle = sender as Rectangle;
			if (rectangle == null)
				System.Diagnostics.Debug.WriteLine(
					"Couldn't retrieve rectangle object on DragLeave");
			else this.ResetDragDropArea(rectangle);
		}

		private void ResetDragDropArea(Rectangle area) {
			area.Fill = new SolidColorBrush(Colors.Transparent);
		}

		#endregion

		#region Properties

		public FiscalizationHomeViewModel ViewModel { get; private set; }

		#endregion
	}
}
