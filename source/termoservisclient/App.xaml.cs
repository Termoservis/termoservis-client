using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro;

namespace TermoservisClient {
	public partial class App : Application {
		// NOTE Publish: 964PvrH2rj
		private static AppStateEventArgs appState = new AppStateEventArgs();


		public App() {
			Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

			this.AppVersion =
				System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();

			Task.Run(() => this.Initialize());
		}


		public void Initialize() {
			// Set application theme
			var accent = ThemeManager.DefaultAccents.First(a => a.Name == "Red");
			ThemeManager.ChangeTheme(this, accent, Theme.Light);
		}
		
		#region Properties

		public static event AppStateEventHandler OnStateChanged;

		public static string StateMessage {
			get { return App.appState.Message; }
			set {
				App.Current.Dispatcher.BeginInvoke(new Action(() => {
					App.appState.Message = value;

					if (App.OnStateChanged != null)
						App.OnStateChanged(App.Current, App.appState);
				}));
			}
		}

		public static AppStates StateType
		{
			get { return App.appState.State; }
			set {
				App.Current.Dispatcher.BeginInvoke(new Action(() => {
					App.appState.State = value;

					if (App.OnStateChanged != null)
						App.OnStateChanged(App.Current, App.appState);
				}));
			}
		}

		public string AppVersion { get; private set; }

		#endregion
	}
}
