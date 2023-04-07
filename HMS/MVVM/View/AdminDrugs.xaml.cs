﻿using HMS.MVVM.View.Doctors;
using HMS.MVVM.View.Drugs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMS.MVVM.View
{
	/// <summary>
	/// Interaction logic for AdminDrugs.xaml
	/// </summary>
	public partial class AdminDrugs : UserControl
	{
		public AdminDrugs()
		{
			InitializeComponent();
		}
		private void AddMemberButton_Click(object sender, RoutedEventArgs e)
		{
			var window = new AddDrugWindow();
			window.Show();
		}
	}
}
