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
	/// Interaction logic for FiscalizationWindow.xaml
	/// </summary>
	public partial class FiscalizationWindow : MetroWindow {
		// TODO Ispisati customer podatke ispod storno
		// TODO Storno get account x/y/z
		// TODO broj stranice

		// TODO Folder se moze uvesti
		// TODO Show colliding accounts in custom windows instead in dialog
		// TODO Show only account numbers and stores for selected date range

		// TODO Backup na cloud
		// TODO Remove users db access if not given

		// TODO Restart application nakon nekog vremena

		public FiscalizationWindow() {
			InitializeComponent();
		}
	}
}
