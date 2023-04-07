using CommunityToolkit.Mvvm.ComponentModel;
using HMS.MVVM.Model;
using HMS.MVVM.View.MessageWindow;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	public partial class UserBillingVM : ObservableObject
	{
		private ObservableCollection<Bill> _billsData = new ObservableCollection<Bill>();

		public ObservableCollection<Bill> BillsData
		{
			get => _billsData;
			set
			{
				if (_billsData != value)
				{
					_billsData = value;
					OnPropertyChanged();
				}
			}
		}

		private DelegateCommand _refreshListCommand;
		public DelegateCommand RefreshListCommand =>
			_refreshListCommand ?? (_refreshListCommand = new DelegateCommand(ExecuteRefreshListCommand));

		void ExecuteRefreshListCommand()
		{
			//MessageBox.Show("You clicked refresh");
			var messageWindow = new MessageWindow("You clicked refresh 🔃");
			messageWindow.ShowDialog();

			Read();
		}

		// Delete prescription command using prism core package
		private DelegateCommand<Bill> _deleteBillCommand;
		public DelegateCommand<Bill> DeleteBillCommand =>
			_deleteBillCommand ?? (_deleteBillCommand = new DelegateCommand<Bill>(ExecuteBillCommand));

		void ExecuteBillCommand(Bill parameter)
		{
			//string deletedPrescriptionName = "";
			//using (DataContext context = new DataContext())
			//{
			//	Prescription selectedPrescription = parameter;
			//	if (selectedPrescription != null)
			//	{
			//		Prescription pres = context.Prescriptions.Single(x => x.Id == selectedPrescription.Id);
			//		deletedPrescriptionName = pres.Id.ToString();
			//		context.Prescriptions.Remove(pres);
			//		context.SaveChanges();
			//	}
			//}
			//MessageBox.Show($"Prescription #'{deletedPrescriptionName}' deleted sucessfuly 😊 !");
			//MessageBox.Show("Full Name : "+parameter.Patient.FullName + " \nNo of Drugs" + parameter.Dosages.Count + "\nNo of Drugs" + parameter.MedicalTests.Count);


			//using (DataContext context = new DataContext())
			//{
			//	MessageBox.Show("patient id - " + parameter.PatientId.ToString() + "\npatient name - " + context.Patients.Single(x => x.Id == parameter.PatientId).FullName);

			//}

			Read();
		}

		public UserBillingVM()
		{
			Read();
		}


		public void Read()
		{
			using (DataContext context = new DataContext())
			{
				//students = context.Students.ToList();
				_billsData.Clear();
				foreach (var bi in context.Bills)
				{
					_billsData.Add(bi);
				}
			}

		}
	}
}
