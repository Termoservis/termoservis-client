using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Termoservis.Data.Users {
	public partial class Manufacturer {
		public static string ManufacturerLogoDirectory = "pack://application:,,,/Resources/Manufacturers/";

		public ImageSource Logo {
			get {
				Uri path = new Uri(ManufacturerLogoDirectory + this.Name.Replace(".", "") + ".png", UriKind.RelativeOrAbsolute);
				return new BitmapImage(path);
			}
		}
	}
}
