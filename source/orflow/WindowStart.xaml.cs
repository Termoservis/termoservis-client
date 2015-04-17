using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Orflow {
	/// <summary>
	/// UI Code Logic for WindowStart
	/// </summary>
	public partial class WindowStart : Window {
		// Variables
		private DispatcherTimer indicatorUpdateTimer;


		/// <summary>
		/// Creates object of type WindowStart and initializes it
		/// </summary>
		public WindowStart() {
			this.InitializeComponent();

			// Indicator timer
			this.indicatorUpdateTimer = new DispatcherTimer();
			this.indicatorUpdateTimer.Interval = TimeSpan.FromMilliseconds(200);
			this.indicatorUpdateTimer.IsEnabled = true;
			this.indicatorUpdateTimer.Tick += IndicatorTick;

			// Sets objects to initial state
			this.SetOverallProgressAsync(0d);
			this.SetCurrentProgressAsync(0d);
			this.SetCurrentProgressInderteminateAsync(true);
			this.SetCurrentProgressIndicatorAsync(String.Empty);
			this.SetCurrentProgressStateAsync(String.Empty);
			this.IsIndicatorActive = false;
		}


		/// <summary>
		/// On window controls loaded
		/// </summary>
		/// <param name="sender">Not used</param>
		/// <param name="e">Not used</param>
		private void WindowLoaded(object sender = null, RoutedEventArgs e = null) {
			Updater updater = new Updater(this);
			updater.BeginUpdate();
		}
		


		/// <summary>
		/// Updates current progress indicator to simple -\|/-\|/ text
		/// </summary>
		/// <param name="sender">Not used</param>
		/// <param name="args">Not used</param>
		private void SimpleIndicatorTick() {
			// Skip update if indicator isn't visible
			if (!this.IsIndicatorActive) return;

			// Gets current indicator
			string value = this.currentProgressIndicator.Content.ToString();

			// Sets next indicators
			if (value == "\u2013") value = "\\";
			else if (value == "\\") value = "|";
			else if (value == "|") value = "/";
			else value = "\u2013";

			// Requests to apply next indicator
			this.SetCurrentProgressIndicatorAsync(value);
		}

		/// <summary>
		/// Calls indicator method on each indicator tick
		/// </summary>
		/// <param name="sender">Not used</param>
		/// <param name="eventArgs">Not used</param>
		private void IndicatorTick(object sender, EventArgs eventArgs) {
			if (this.IndicatorTickAction != null)
				this.IndicatorTickAction.Invoke();
		}

		/// <summary>
		/// Sets overall progress ProgressBar value
		/// </summary>
		/// <param name="value">Value of ProgressBar to set between 0 and 1</param>
		/// <remarks>
		/// Uses async dispatcher invoke call 
		/// </remarks>>
		public void SetOverallProgressAsync(double value) {
			if (Double.IsNaN(value) || value < 0 || value > 1)
				throw new ArgumentOutOfRangeException("value");

			this.overallProgressBar.Dispatcher.InvokeAsync(() => {
				this.overallProgressBar.Value = Math.Abs(value);
			});
		}

		/// <summary>
		/// Sets current progress ProgressBar value
		/// </summary>
		/// <param name="value">Value of ProgressBar to set between 0 and 1</param>
		/// <remarks>>
		/// Uses async dispatcher invoke call 
		/// </remarks>>
		public void SetCurrentProgressAsync(double value) {
			if (Double.IsNaN(value) || value < 0 || value > 1)
				throw new ArgumentOutOfRangeException("value");

			this.currentProgressBar.Dispatcher.InvokeAsync(() => {
				this.currentProgressBar.Value = value;
			});
		}

		/// <summary>
		/// Sets current progress ProgressBar indeterminate mode
		/// </summary>
		/// <param name="isIndeterminate">State of ProgressBar indeterminate mode</param>
		/// <remarks>>
		/// Uses async dispatcher invoke call 
		/// </remarks>>
		public void SetCurrentProgressInderteminateAsync(bool isIndeterminate) {
			this.currentProgressBar.Dispatcher.InvokeAsync(() => {
				this.currentProgressBar.IsIndeterminate = isIndeterminate;
			});
		}

		/// <summary>
		/// Sets current progress state message
		/// </summary>
		/// <param name="state">Message to set</param>
		/// <remarks>>
		/// Uses async dispatcher invoke call 
		/// </remarks>>
		public void SetCurrentProgressStateAsync(string state) {
			if (state == null) throw new ArgumentNullException("state");

			this.currentProgressState.Dispatcher.InvokeAsync(() => {
				this.currentProgressState.Content = state;
			});
		}

		/// <summary>
		/// Sets current progress indicator
		/// </summary>
		/// <param name="indicator">Indicator to set</param>
		/// <remarks>>
		/// Uses async dispatcher invoke call 
		/// </remarks>>
		public void SetCurrentProgressIndicatorAsync(string indicator) {
			if (indicator == null) throw new ArgumentNullException("indicator");

			this.currentProgressIndicator.Dispatcher.InvokeAsync(() => {
				this.currentProgressIndicator.Content = indicator;
			});
		}

		/// <summary>
		/// Method called upon template apply on this object
		/// </summary>
		public override void OnApplyTemplate() {
			// Attach event for window drag move
			Rectangle windowMoveRect = this.GetObjectFromTemplate<Rectangle>("NewUIWindowHeaderMoveRectangle");
			if (windowMoveRect != null)
				windowMoveRect.PreviewMouseLeftButtonDown += WindowMove;
			else throw new NullReferenceException("windowMoveRect");

			// Attach event for window minimize button
			Button minimizeButton = this.GetObjectFromTemplate<Button>("NewUIWindowHeaderMinimizeButton");
			if (minimizeButton != null)
				minimizeButton.Click += WindowMinimize;
			else throw new NullReferenceException("minimizeButton");

			// Disable window maximize/restore button
			Button maximizeButton = this.GetObjectFromTemplate<Button>("NewUIWindowHeaderMaximizeButton");
			if (maximizeButton != null)
				maximizeButton.IsEnabled = false;
			else throw new NullReferenceException("maximizeButton");

			// Attach event for window close button
			Button closeButton = this.GetObjectFromTemplate<Button>("NewUIWindowHeaderCloseButton");
			if (closeButton != null)
				closeButton.Click += WindowClose;
			else throw new NullReferenceException("closeButton");


			base.OnApplyTemplate();
		}

		/// <summary>
		/// Closes window
		/// </summary>
		/// <param name="sender">Not used</param>
		/// <param name="e">Not used</param>
		private void WindowClose(object sender, RoutedEventArgs e) {
			this.Close();
		}

		/// <summary>
		/// Sets window state to minimized
		/// </summary>
		/// <param name="sender">Not used</param>
		/// <param name="e">Not used</param>
		private void WindowMinimize(object sender, RoutedEventArgs e) {
			this.WindowState = WindowState.Minimized;
		}

		/// <summary>
		/// Starts drag move action on window
		/// </summary>
		/// <param name="sender">Not used</param>
		/// <param name="e">Not used</param>
		private void WindowMove(object sender = null, MouseButtonEventArgs e = null) {
			this.DragMove();
		}

		/// <summary>
		/// Finds dependency object of given name in template and casts it to requested type
		/// </summary>
		/// <typeparam name="T">Type to cast dependency object to</typeparam>
		/// <param name="name">Name of object in template</param>
		/// <returns>Returns matching object from template or null if object of requested type isn't found</returns>
		/// <exception cref="ArgumentNullException">Argument "name" is null</exception>
		private T GetObjectFromTemplate<T>(string name) where T : FrameworkElement {
			if (String.IsNullOrEmpty(name)) 
				throw new ArgumentNullException("name");

			return this.GetTemplateChild(name) as T;
		}


		#region Properties

		/// <summary>
		/// Gets or sets current progress indicator visibility
		/// </summary>
		public bool IsIndicatorActive {
			get { return this.currentProgressIndicator.Visibility == Visibility.Visible; }
			set {
				this.currentProgressIndicator.Dispatcher.InvokeAsync(() => {
					this.currentProgressIndicator.Visibility = value ? Visibility.Visible : Visibility.Hidden;
				});
			}
		}

		/// <summary>
		/// Gets or sets action that is called on each indicator timer tick
		/// </summary>
		public Action IndicatorTickAction { get; set; }

		/// <summary>
		/// Event is called every 200ms when current progress indicator needs to be updated
		/// </summary>
		public event EventHandler OnIndicatorUpdate {
			add { this.indicatorUpdateTimer.Tick += value; }
			remove { this.indicatorUpdateTimer.Tick -= value; }
		}

		#endregion
	}
}
