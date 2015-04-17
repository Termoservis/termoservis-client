using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Termoservis.Data.Users;

namespace TermoservisClient.Controls.ViewModels {
	//public class WorkItemViewModel : DependencyObject {
	//	public WorkItemViewModel(WorkItem model) {
	//		this.Model = model;
	//		this.Wrapping = TextWrapping.NoWrap;
	//	}


	//	public WorkItem Model { get; private set; }

	//	public ImageSource Image {
	//		get { 
	//			if (Model is Service)
	//				return new BitmapImage(new Uri("/TermoservisClient;component/Resources/Images/Service.png", UriKind.Relative));
	//			else return new BitmapImage(new Uri("/TermoservisClient;component/Resources/Images/Repair.png", UriKind.Relative)); 
	//		}
	//	}

	//	public TextWrapping Wrapping {
	//		get { return (TextWrapping)GetValue(WrappingProperty); }
	//		set { SetValue(WrappingProperty, value); }
	//	}
	//	public static readonly DependencyProperty WrappingProperty =
	//		DependencyProperty.Register("Wrapping", typeof(TextWrapping), typeof(WorkItemViewModel), new PropertyMetadata(default(TextWrapping)));
	//}
}