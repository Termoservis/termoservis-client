using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TermoservisClient.Extensions {
	public static class DependencyObjectExtensions {
		public static T GetVisualParent<T>(this DependencyObject child) where T : Visual {
			while ((child != null) && !(child is T)) {
				child = VisualTreeHelper.GetParent(child);
			}
			return child as T;
		}
	}
}
