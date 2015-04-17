using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.ViewModel;

namespace TermoservisClient.Windows.Login {
	public class LoginWindowViewModel : NotificationObject {
		private static readonly Dictionary<string, string> Users =
			new Dictionary<string, string>() {
				{"mario.toplek", "17081968"},
				{"tatjana.toplek", "02091969"},
				{"admin", "lqYo9c6MvwvPjWD"},
				{"mtoplek", "mtoplek"},
				{"ttoplek", "ttoplek"}
			};

		private string username;
		private string password;
		private bool isInvalid;
		private bool capsLockState;


		public LoginWindowViewModel() {
			this.isInvalid = false;
		}


		public void ValidateUser(string passwordValue) {
			this.Password = passwordValue;

			if (Users.ContainsKey(this.Username) &&
				passwordValue == Users[this.Username])
				this.IsLogedIn = true;
			else this.IsInvalid = true;
		}

		#region Properties

		public bool? IsLogedIn { get; private set; }

		public string Username {
			get { return this.username; }
			set {
				this.username = value;
				this.RaisePropertyChanged(() => this.Username);
			}
		}

		public string Password {
			get { return this.password; }
			set {
				this.password = value;
				this.RaisePropertyChanged(() => this.Password);
			}
		}

		public bool IsInvalid {
			get { return this.isInvalid; }
			set {
				this.isInvalid = value;
				this.RaisePropertyChanged(() => this.IsInvalid);

				if (this.IsInvalid)
					this.IsLogedIn = false;
			}
		}

		public bool CapsLockState {
			get { return this.capsLockState; }
			set {
				this.capsLockState = value;
				this.RaisePropertyChanged(() => this.CapsLockState);
			}
		}

		#endregion
	}
}
