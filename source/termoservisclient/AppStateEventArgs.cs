using System;

namespace TermoservisClient {
	public class AppStateEventArgs : EventArgs {
		public string Message;
		public AppStates State;
		public double Progress;
	}
}