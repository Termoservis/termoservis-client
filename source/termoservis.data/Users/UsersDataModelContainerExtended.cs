using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termoservis.Data.Users {
	public partial class UsersDataModelContainer : DbContext {
		public UsersDataModelContainer(string connectionString) : base(connectionString) {}
	}
}
