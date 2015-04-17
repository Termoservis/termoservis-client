using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.Prism.ViewModel;
using TermoservisClient.Windows.Development.Country;

namespace TermoservisClient.Windows.Development.Window {
	public class DevelopmentWindowViewModel :NotificationObject {
		public DevelopmentWindowViewModel() {
			this.Tools = new ObservableCollection<DevelopmentTool>();
		}


		public void Load() {
			this.LoadTools();
		}

		private void LoadTools() {
			this.Tools.Add(new DevelopmentTool() {
				Name = "Country editor",
				Control = new CountryView()
			});
		}

		#region Properties

		public ObservableCollection<DevelopmentTool> Tools { get; private set; } 

		#endregion

		public class DevelopmentTool {
			public string Name { get; set; }
			public UserControl Control { get; set; }
		}
	}
}
