using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.MVVM.ViewModel
{
	public partial class NormalUserWindowVM : ObservableObject
	{
		public UserDashboardVM U_Dashboard_VM { get; set; }
		public UserAppointmentsVM U_Appointments_VM { get; set; }
		public UserBillingVM U_Billing_VM { get; set; }
		public UserPatientsVM U_Patients_VM { get; set; }
		public UserPrescriptionsVM U_Prescriptions_VM { get; set; }
		//public DiscoveryViewModel DiscoveryVM { get; set; }

		// temp setter for maintainting current state
		private object _currentView;

		[ObservableProperty]
		public string currentViewName;

		public object CurrentView
		{
			get { return _currentView; }
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		[RelayCommand]
		public void UserDashboardView()
		{
			U_Dashboard_VM = new UserDashboardVM();
			CurrentView = U_Dashboard_VM;
			CurrentViewName = "Dashboard";
		}
		[RelayCommand]
		public void UserAppointmentsView()
		{
			U_Appointments_VM = new UserAppointmentsVM();
			CurrentView = U_Appointments_VM;
			CurrentViewName = "Appointments";
		}
		[RelayCommand]
		public void UserBillingView()
		{
			U_Billing_VM = new UserBillingVM();
			CurrentView = U_Billing_VM;
			CurrentViewName = "Billings";
		}
		[RelayCommand]
		public void UserPatientsView()
		{
			U_Patients_VM = new UserPatientsVM();
			CurrentView = U_Patients_VM;
			CurrentViewName = "Patients";
		}
		[RelayCommand]
		public void UserPrescriptionsView()
		{
			U_Prescriptions_VM = new UserPrescriptionsVM();
			CurrentView = U_Prescriptions_VM;
			CurrentViewName = "Prescriptions";
		}

		public NormalUserWindowVM()
		{

			U_Dashboard_VM = new UserDashboardVM();
			CurrentView = U_Dashboard_VM;
			CurrentViewName = "Dashboard";

		}

		//private RelayCommand userDashboardViewCommand;

		//public ICommand UserDashboardViewCommand
		//{
		//	get
		//	{
		//		if (userDashboardViewCommand == null)
		//		{
		//			userDashboardViewCommand = new RelayCommand(UserDashboardView1);
		//		}

		//		return userDashboardViewCommand;
		//	}
		//}

		//private void UserDashboardView1()
		//{
		//	U_Dashboard_VM = new UserDashboardVM();
		//	CurrentView = U_Dashboard_VM;
		//}

		//private RelayCommand userPatientsViewCommand;

		//public ICommand UserPatientsViewCommand
		//{
		//	get
		//	{
		//		if (userPatientsViewCommand == null)
		//		{
		//			userPatientsViewCommand = new RelayCommand(UserPatientsView1);
		//		}

		//		return userPatientsViewCommand;
		//	}
		//}

		//private void UserPatientsView1()
		//{
		//	U_Patients_VM = new UserPatientsVM();
		//	CurrentView = U_Patients_VM;
		//}

		//private RelayCommand userAppointmentsViewCommand;

		//public ICommand UserAppointmentsViewCommand
		//{
		//	get
		//	{
		//		if (userAppointmentsViewCommand == null)
		//		{
		//			userAppointmentsViewCommand = new RelayCommand(UserAppointmentsView1);
		//		}

		//		return userAppointmentsViewCommand;
		//	}
		//}

		//private void UserAppointmentsView1()
		//{
		//	U_Appointments_VM = new UserAppointmentsVM();
		//	CurrentView = U_Appointments_VM;
		//}

		//private RelayCommand userPrescriptionsViewCommand;

		//public ICommand UserPrescriptionsViewCommand
		//{
		//	get
		//	{
		//		if (userPrescriptionsViewCommand == null)
		//		{
		//			userPrescriptionsViewCommand = new RelayCommand(UserPrescriptionsView1);
		//		}

		//		return userPrescriptionsViewCommand;
		//	}
		//}

		//private void UserPrescriptionsView1()
		//{
		//	U_Prescriptions_VM = new UserPrescriptionsVM();
		//	CurrentView = U_Prescriptions_VM;
		//}

		//private RelayCommand userBillingViewCommand;

		//public ICommand UserBillingViewCommand
		//{
		//	get
		//	{
		//		if (userBillingViewCommand == null)
		//		{
		//			userBillingViewCommand = new RelayCommand(UserBillingView1);
		//		}

		//		return userBillingViewCommand;
		//	}
		//}

		//private void UserBillingView1()
		//{
		//	U_Billing_VM = new UserBillingVM();
		//	CurrentView = U_Billing_VM;
		//}
	}
}
