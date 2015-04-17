using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termoservis.Data.Fiscalization;

namespace TermoservisClient.Adapters {
	public interface IFiscalizationDataServiceFacade {
		FiscalizationDataModelContainer DataContainer { get; }
		Task OpenConnectionAsync();
	}
}
