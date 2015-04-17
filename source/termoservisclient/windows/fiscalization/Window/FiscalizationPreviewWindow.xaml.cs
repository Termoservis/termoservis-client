using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace TermoservisClient.Windows.Fiscalization.Window {
	/// <summary>
	/// Interaction logic for FiscalizationPreviewWindow.xaml
	/// </summary>
	public partial class FiscalizationPreviewWindow : MetroWindow {
		public FiscalizationPreviewWindow() {
			InitializeComponent();

			// Assign ViewModel
			if (this.ViewModel == null)
				if ((this.ViewModel = this.DataContext as FiscalizationPreviewWindowViewModel) == null)
					throw new NullReferenceException("Invalid ViewModel");

			this.ViewModel.OnAccountsSet += ViewModelOnOnAccountsSet;
		}

		private void ViewModelOnOnAccountsSet(object sender, EventArgs eventArgs) {
			foreach (var account in this.ViewModel.Accounts) {
				var element = new ContentControl {Content = account};
				var uiBlock = new BlockUIContainer(element);
				var accountBlock = new Section(uiBlock);
				this.PreviewDocument.Blocks.InsertBefore(
					this.PreviewDocument.Blocks.ElementAt(this.PreviewDocument.Blocks.Count - 2), accountBlock);
			}
		}

		#region Properties

		public FiscalizationPreviewWindowViewModel ViewModel { get; private set; }

		#endregion

		private void PrintPreviewClick(object sender, RoutedEventArgs e) {
			var pd = new PrintDialog();
			if (pd.ShowDialog() == true) {
				var pageSize = new Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight);
				var paginator = this.PreviewDocument as IDocumentPaginatorSource;
				paginator.DocumentPaginator.PageSize = pageSize;
				pd.PrintDocument(paginator.DocumentPaginator,
					String.Format("Termoservis Fiskalizacija {0} - {1}",
						this.ViewModel.AccountsStartDate.Date.ToShortDateString(),
						this.ViewModel.AccountsEndDate.Date.ToShortDateString()));
			}
		}
	}
}
