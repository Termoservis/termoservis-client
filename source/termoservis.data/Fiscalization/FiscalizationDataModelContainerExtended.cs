using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termoservis.Data.Fiscalization {
	public partial class FiscalizationDataModelContainer : DbContext {
		public FiscalizationDataModelContainer(string connectionString) : base(connectionString) {}
	}
}
