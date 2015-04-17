using System.Collections.Generic;
using System.Windows.Input;

namespace TermoservisClient.Windows.Development.PostalInfoImporter {
	public partial class PostalInfoImporterWindow : System.Windows.Window {
		public PostalInfoImporterWindow() {
			InitializeComponent();
		}

		private void ImportData(object sender, KeyEventArgs e) {
			if (e.Key != Key.Return || e.Key != Key.Enter) return;

			var counties = new List<Termoservis.Data.Users.County>();
			string data = this.importTextBox.Text;

			this.ViewModel.ImportData(data);
		}


		#region Properties

		public PostalInfoImporterViewModel ViewModel { get; private set; }

		#endregion
	}
}
