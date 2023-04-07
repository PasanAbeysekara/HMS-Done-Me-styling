using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HMS.MVVM.Model;
using HMS.MVVM.View.Appointments;
using HMS.MVVM.View.Patients;
using HMS.MVVM.View.Prescriptions;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	public partial class PatientProfileVM : ObservableObject
	{
		// observable props

		[ObservableProperty]
		public string patId;

		[ObservableProperty]
		public string name;

		[ObservableProperty]
		public string age;

		[ObservableProperty]
		public string gender;

		[ObservableProperty]
		public string phone;

		[ObservableProperty]
		public string address;

		[ObservableProperty]
		public string email;

		[ObservableProperty]
		public string weight;

		[ObservableProperty]
		public string height;

		[ObservableProperty]
		public string blood;

		// Billing
		[ObservableProperty]
		public string doctorFee;

		[ObservableProperty]
		public string testFee;

		[ObservableProperty]
		public string hospitalFee;

		[ObservableProperty]
		public string totalFee;


		private DelegateCommand _addPrescriptionCommand;
		public DelegateCommand AddPrescriptionCommand =>
			_addPrescriptionCommand ?? (_addPrescriptionCommand = new DelegateCommand(ExecuteAddPrescriptionCommand));

		void ExecuteAddPrescriptionCommand()
		{
			using (DataContext context = new DataContext())
			{
				foreach (var pat_ in context.Patients) pat_.IsPatientSelected = false;
				context.SaveChanges();
				context.Patients.Single(x => x.Id == Convert.ToInt32(PatId)).IsPatientSelected = true;
				context.SaveChanges();
				MessageBox.Show($"is '{context.Patients.Single(x => x.Id == Convert.ToInt32(PatId)).FullName}' selected -> {context.Patients.Single(x => x.Id == Convert.ToInt32(PatId)).IsPatientSelected}");
				//Patient tt = context.Patients.Single(x => x.IsPatientSelected == true);
				//MessageBox.Show($"patient {tt.FullName}");
			}
			var window = new AddPrescriptionWindow();
			window.Show();

		}

		private DelegateCommand _addAppointmentCommand;
		public DelegateCommand AddAppointmentCommand =>
			_addAppointmentCommand ?? (_addAppointmentCommand = new DelegateCommand(ExecuteAddAppointmentCommand));

		void ExecuteAddAppointmentCommand()
		{
			using (DataContext context = new DataContext())
			{
				foreach (var pat_ in context.Patients) pat_.IsPatientSelected = false;
				context.SaveChanges();
				context.Patients.Single(x => x.Id == Convert.ToInt32(PatId)).IsPatientSelected = true;
				context.SaveChanges();
				MessageBox.Show($"is '{context.Patients.Single(x => x.Id == Convert.ToInt32(PatId)).FullName}' selected -> {context.Patients.Single(x => x.Id == Convert.ToInt32(PatId)).IsPatientSelected}");
				//Patient tt = context.Patients.Single(x => x.IsPatientSelected == true);
				//MessageBox.Show($"patient {tt.FullName}");
			}
			var window = new AddAppointmentWindow();
			window.Show();

		}


		private DelegateCommand _refreshCommand;
		public DelegateCommand RefreshCommand =>
			_refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCommand));

		void ExecuteRefreshCommand()
		{
			using (DataContext context = new DataContext())
			{
				appointments.Clear();
				var apps = context.Appointments.Where(x => x.PatientId == Convert.ToInt32(PatId)).ToList();
				if (apps != null) apps.ForEach(y => { appointments.Add(y); });
				else MessageBox.Show("This patient have no Appointments");
				prescriptions.Clear();
				var prescs = context.Prescriptions.Where(x => x.PatientId == Convert.ToInt32(PatId)).ToList();
				if (prescs != null) prescs.ForEach(p => { prescriptions.Add(p); });
				else MessageBox.Show("This patient have no Prescriptions");
				billingCalculate();
			}
		}




		private ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();


		public ObservableCollection<Appointment> Appointments
		{
			get => appointments;
			set
			{
				if (appointments != value)
				{
					appointments = value;
					OnPropertyChanged();
				}
			}
		}


		private ObservableCollection<Prescription> prescriptions = new ObservableCollection<Prescription>();


		public ObservableCollection<Prescription> Prescriptions
		{
			get => prescriptions;
			set
			{
				if (prescriptions != value)
				{
					prescriptions = value;
					OnPropertyChanged();
				}
			}
		}

		public void billingCalculate()
		{
			using (DataContext context = new DataContext())
			{
				var apps = context.Appointments.Where(x => x.PatientId == Convert.ToInt32(PatId)).ToList();
				var prescs = context.Prescriptions.Where(x => x.PatientId == Convert.ToInt32(PatId)).ToList();

				//Billing
				double _docFee = 0;
				foreach (var app in apps)
				{
					_docFee += context.Doctors.Single(x => x.Id == app.DoctorId).Fee;
				}
				DoctorFee = $"Doctor Fee             : LKR {_docFee}";

				double _testFee = 0;
				foreach (var presc in prescs)
				{
					//MessageBox.Show(presc.PrescribedDate.ToString());
					foreach (var medTest in context.MedicalTests.Where(x => x.PrescriptionId == presc.Id))
					{
						//MessageBox.Show("medTest.Description");
						_testFee += context.Tests.Single(x => x.Id == medTest.TestId).Fee;
					}

				}
				TestFee = $"Test Fee                  : LKR {_testFee}";

				HospitalFee = $"Hospital Fee (10%) : LKR {(_docFee + _testFee) * 0.1}";

				TotalFee = $"Total Fee                 : LKR {_docFee + _testFee + (_docFee + _testFee) * 0.1}";


				Random random = new Random();

				var bill = new Bill
				{
					BillAmount = _docFee + _testFee + (_docFee + _testFee) * 0.1,
					PaymentMode = (random.Next(2) == 0) ? "Cash" : "Card",
					Status = false,
					PaymentDate = DateTime.Now,
					PatientId = Convert.ToInt32(PatId)
				};

				var tmp = context.Bills.Where(x => x.Id == Convert.ToInt32(PatId));

				if (tmp.Count() > 0)
				{
					var _b = context.Bills.Single(x => x.Id == Convert.ToInt32(PatId));
					_b.BillAmount = _docFee + _testFee + (_docFee + _testFee) * 0.1;
					_b.PaymentMode = (random.Next(2) == 0) ? "Cash" : "Card";
					_b.Status = false;
					_b.PaymentDate = DateTime.Now;
					context.SaveChanges();
				}

				else
				{
					context.Bills.Add(bill);
					context.SaveChanges();
				}
			}
		}



		public PatientProfileVM()
		{
			using (DataContext context = new DataContext())
			{
				Patient tmp = context.Patients.Single(x => x.IsPatientSelected == true);
				foreach (var pat_ in context.Patients) pat_.IsPatientSelected = false;
				context.SaveChanges();

				// ID
				patId = tmp.Id.ToString();

				// name
				name = tmp.FullName;

				// age
				string birthDateString = tmp.BirthDay;
				DateTime birthDate = DateTime.Parse(birthDateString);
				DateTime today = DateTime.Today;
				int _age = today.Year - birthDate.Year;
				if (birthDate > today.AddYears(-_age))
				{
					_age--;
				}
				age = $"{_age} years";

				//gender
				gender = tmp.Gender == 'M' ? "Male" : "Female";

				//phone
				phone = tmp.Phone;

				//email
				email = tmp.Email;

				//address
				address = tmp.Address;

				//weight
				weight = $"{tmp.Weight} kg";

				//height
				height = $"{tmp.Height} cm";

				//blood
				blood = tmp.BloodGroup;


				var apps = context.Appointments.Where(x => x.PatientId == tmp.Id).ToList();
				if (apps != null) apps.ForEach(y => { appointments.Add(y); });
				else MessageBox.Show("This patient have no Appointments");
				var prescs = context.Prescriptions.Where(x => x.PatientId == tmp.Id).ToList();
				if (prescs != null) prescs.ForEach(p => { prescriptions.Add(p); });
				else MessageBox.Show("This patient have no Prescriptions");

				//Billing
				double _docFee = 0;
				foreach (var app in apps)
				{
					_docFee += context.Doctors.Single(x => x.Id == app.DoctorId).Fee;
				}
				doctorFee = $"Doctor Fee             : LKR {_docFee}";

				double _testFee = 0;
				foreach (var presc in prescs)
				{
					//MessageBox.Show(presc.PrescribedDate.ToString());
					foreach (var medTest in context.MedicalTests.Where(x => x.PrescriptionId == presc.Id))
					{
						//MessageBox.Show("medTest.Description");
						_testFee += context.Tests.Single(x => x.Id == medTest.TestId).Fee;
					}


				}
				testFee = $"Test Fee                  : LKR {_testFee}";

				hospitalFee = $"Hospital Fee (10%) : LKR {(_docFee + _testFee) * 0.1}";

				totalFee = $"Total Fee                 : LKR {_docFee + _testFee + (_docFee + _testFee) * 0.1}";

			}
		}
	}
}
