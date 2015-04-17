using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termoservis.Data.Users {
	public partial class CustomerDevice {
		public Service LastService {
			get {
				return (from service in this.Service
						orderby service.Date descending
						select service).FirstOrDefault();
			}
		}
	}
}
