using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TermoservisClient.Commands {
	public static class NewUIWindowCommands {
		public static RoutedUICommand WindowClose = new RoutedUICommand("WindowClose", "WindowClose", typeof(NewUIWindowCommands));
		public static RoutedUICommand WindowMaximize = new RoutedUICommand("WindowMaximize", "WindowMaximize", typeof(NewUIWindowCommands));
		public static RoutedUICommand WindowMinimaze = new RoutedUICommand("WindowMinimaze", "WindowMinimaze", typeof(NewUIWindowCommands));
	}
}
