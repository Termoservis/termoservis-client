using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace TermoservisClient.Windows.Login {
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : MetroWindow {
		public LoginWindow() {
			InitializeComponent();

			// Assign ViewModel
			if (this.ViewModel == null)
				if ((this.ViewModel = this.DataContext as LoginWindowViewModel) == null)
					throw new NullReferenceException("Invalid ViewModel");
		}

		private void LoginWindowOnLoaded(object sender, RoutedEventArgs e) {
			// Focus on username box
			this.UserNameBox.Focus();
		}

		private void PasswordBoxKeyDown(object sender, KeyEventArgs e) {
			this.ViewModel.CapsLockState =
				System.Windows.Forms.Control.IsKeyLocked(Keys.CapsLock);
			
			this.CheckLogedInData(e.Key);
		}

		private void UserNameBoxKeyDown(object sender, KeyEventArgs e) {
			this.ViewModel.CapsLockState =
				System.Windows.Forms.Control.IsKeyLocked(Keys.CapsLock);

			this.CheckLogedInData(e.Key);
		}

		private void CheckLogedInData(Key pressed) {
			if (pressed == Key.Enter || pressed == Key.Return) {
				this.UserNameBox.MoveFocus(
					new TraversalRequest(FocusNavigationDirection.Next));
				
				this.ViewModel.ValidateUser(this.PasswordBox.Password ?? String.Empty);

				// Close windows if validates successfully
				if (this.ViewModel.IsLogedIn.HasValue && 
					this.ViewModel.IsLogedIn.Value) {
					this.DialogResult = this.ViewModel.IsLogedIn;
					this.Close();
				}
				else {
					// Clear both boxes
					this.UserNameBox.Clear();
					this.PasswordBox.Clear();

					// Focus on username
					this.UserNameBox.Focus();
				}
			}
		}

		#region Properties

		public LoginWindowViewModel ViewModel { get; private set; }

		#endregion
	}
}
