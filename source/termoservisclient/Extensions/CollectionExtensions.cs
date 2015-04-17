using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism;

namespace TermoservisClient.Extensions {
	public static class CollectionExtensions {
		public static void AddRangeSafe<T>(this Collection<T> @this, IEnumerable<T> elements) {
			if (Application.Current.CheckAccess())
				@this.AddRange(elements);
			else Application.Current.Dispatcher.Invoke(() => @this.AddRange(elements));
		}

		public static async Task AddRangeSafeAsync<T>(this Collection<T> @this, IEnumerable<T> elements) {
			if (Application.Current.CheckAccess())
				@this.AddRange(elements);
			else await Application.Current.Dispatcher.BeginInvoke(new ThreadStart(() => @this.AddRange(elements)));
		}
	}
}
