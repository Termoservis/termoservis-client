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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Orflow {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			// Attach window events
			this.Loaded += MainWindowLoaded;

			InitializeComponent();
		}

		private void MainWindowLoaded(object sender, RoutedEventArgs e) {
			this.TextBlockVersion.Text = String.Format("Version: {0}",
				System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
		}
	}
}
