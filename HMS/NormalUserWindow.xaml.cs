using HMS.MVVM.Model.InsidePrescription.insideDrug;
using HMS.MVVM.Model.InsidePrescription.insideTest;
using HMS.MVVM.Model.InsidePrescription;
using HMS.MVVM.Model;
using HMS.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HMS
{
	/// <summary>
	/// Interaction logic for NormalUserWindow.xaml
	/// </summary>
	public partial class NormalUserWindow : Window
	{
		private void Border_Mousedown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}
		private bool isMaximized = false;
		private void Border_MouseLeftButtondown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				if (isMaximized)
				{
					this.WindowState = WindowState.Normal;
					this.Width = 1080;
					this.Height = 720;

					isMaximized = false;
				}
				else
				{
					this.WindowState = WindowState.Maximized;
					isMaximized = true;
				}
			}
		}

		public NormalUserWindow()
		{
			DataContext = new NormalUserWindowVM();
			InitializeComponent();
		}

		private void AddMemberButton_Click(object sender, RoutedEventArgs e)
		{
			var window = new AddPatientWindow();
			window.Show();
		}

		private void CloseButton_Clicked(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
			//this.Close();
		}

		private void MinimizeButton_Clicked(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var window = new MainWindow();
			window.Show();
			this.Close();
		}

	}
}
