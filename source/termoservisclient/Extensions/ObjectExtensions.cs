using System;
using Microsoft.Practices.Prism.ViewModel;

namespace TermoservisClient.Extensions {
	public static class ObjectExtensions {
		public static void AssignAsViewModel<TViewModelType>(this object @this, ref TViewModelType viewModel) where TViewModelType : NotificationObject {
			if (viewModel == null)
				if ((viewModel = @this as TViewModelType) == null)
					throw new NullReferenceException("Invalid ViewModel");
		}
	}
}