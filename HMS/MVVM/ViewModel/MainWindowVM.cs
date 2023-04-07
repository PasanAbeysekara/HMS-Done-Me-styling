﻿using HMS.MVVM.Model;
using HMS.MVVM.Model.Authentication;
using HMS.MVVM.Model.InsidePrescription;
using HMS.MVVM.Model.InsidePrescription.insideDrug;
using HMS.MVVM.Model.InsidePrescription.insideTest;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	public class MainWindowVM
	{
		public MainWindowVM()
		{
			Random random = new Random();

			//// Create some dummy for bill data
			using (var context = new DataContext())
			{
				// To Make Dummies

				if (context.Drugs.Count() == 0)
				{
					for (int i = 0; i < 4; i++)
					{
						//		// -----------DRUGS---------------
						foreach (var mT in context.Drugs) context.Drugs.Remove(mT);
						string randomString1 = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 6).Select(s => s[random.Next(s.Length)]).ToArray());
						string randomString2 = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 6).Select(s => s[random.Next(s.Length)]).ToArray());
						Drug tmpDrug = new Drug { GenericName = randomString1, TradeName = randomString2 };
						context.Drugs.Add(tmpDrug);

						//		// -----------TESTS---------------
						foreach (var mT in context.Tests) context.Tests.Remove(mT);
						string randomString3 = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 6).Select(s => s[random.Next(s.Length)]).ToArray());
						string randomString4 = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 20).Select(s => s[random.Next(s.Length)]).ToArray());
						int randomFee = random.Next(1, 10) * 100;
						Test tmptest = new Test { TestName = randomString3, Description = randomString4, Fee = randomFee };
						context.Tests.Add(tmptest);

						//		// -----------DOCTORS---------------
						foreach (var mT in context.Doctors) context.Doctors.Remove(mT);
						int randomFee2 = random.Next(1, 10) * 100;
						string randomString5 = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 6).Select(s => s[random.Next(s.Length)]).ToArray());
						Doctor tmpDoc = new Doctor { Name = "Dr. " + randomString5, Fee = randomFee2 };
						context.Doctors.Add(tmpDoc);

					}
					context.SaveChanges();
				}

				//	//	// -----------PATIENTS---------------
				//foreach (var mT in context.Patients) context.Patients.Remove(mT);
				if (context.Patients.Count() == 0)
				{
					for (int i = 0; i < 10; i++)
					{
						var patient = new Patient
						{
							FullName = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 4)
						  .Select(s => s[random.Next(s.Length)]).ToArray()),
							Email = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 4)
						  .Select(s => s[random.Next(s.Length)]).ToArray()) + "@gmail.com",
							BirthDay = "10/24/2000",
							Phone = "011" + new string(Enumerable.Repeat("0123456789", 7)
						  .Select(s => s[random.Next(s.Length)]).ToArray()),
							Gender = (random.Next(2) == 0) ? 'M' : 'F',
							BloodGroup = (random.Next(2) == 0) ? "O+" : "B+",
							Address = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 14)
						  .Select(s => s[random.Next(s.Length)]).ToArray()),
							Weight = random.Next(60, 100),
							Height = random.Next(150, 190),
							AdmittedDate = DateTime.Now.AddDays(random.Next(7))
						};

						context.Patients.Add(patient);
					}
					context.SaveChanges();
				}

				//	//	// -----------PRESCRIPTIONS---------------
				//foreach (var mT in context.Prescriptions) context.Prescriptions.Remove(mT);
				if (context.Prescriptions.Count() < context.Patients.Count() * 2)
				{
					for (int i = 0; i < 10; i++)
					{
						var presc = new Prescription
						{
							PrescribedDate = DateTime.Now.AddDays(random.Next(7, 14)),
							PatientId = context.Patients.ToList()[random.Next(context.Patients.Count())].Id
						};
						context.Prescriptions.Add(presc);
					}
					context.SaveChanges();

				}


				//	//	// -----------APPOINTMENTS---------------
				//foreach (var mT in context.Appointments) context.Appointments.Remove(mT);
				if (context.Appointments.Count() < context.Patients.Count() * 2)
				{
					for (int i = 0; i < 10; i++)
					{
						var app = new Appointment
						{
							AppointedDate = DateTime.Now.AddDays(random.Next(7)),
							DoctorId = context.Doctors.ToList()[random.Next(context.Doctors.Count())].Id,
							PatientId = context.Patients.ToList()[random.Next(context.Patients.Count())].Id
						};
						context.Appointments.Add(app);
					}
					context.SaveChanges();
				}

				//	//	// -----------MEDICAL_TESTS---------------
				//foreach (var mT in context.MedicalTests) context.MedicalTests.Remove(mT);
				if (context.MedicalTests.Count() == 0)
				{
					for (int i = 0; i < 10; i++)
					{
						var mTest = new MedicalTest
						{
							Description = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 14).Select(s => s[random.Next(s.Length)]).ToArray()),
							TestId = context.Tests.ToList()[random.Next(context.Tests.Count())].Id,
							PrescriptionId = context.Prescriptions.ToList()[random.Next(context.Prescriptions.Count())].Id
						};
						context.MedicalTests.Add(mTest);
					}
					context.SaveChanges();
				}

				//	// -----------DOSAGES---------------
				//foreach (var mT in context.Dosages) context.Dosages.Remove(mT);
				if (context.Dosages.Count() == 0)
				{
					for (int i = 0; i < 10; i++)
					{
						var _dosages = new Dosage
						{
							DrugType = (random.Next(2) == 0) ? "Syrup" : "Tablets",
							Dose = (random.Next(3)),
							Duration = random.Next(3, 14),
							Comments = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 10).Select(s => s[random.Next(s.Length)]).ToArray()),
							DrugId = context.Drugs.ToList()[random.Next(context.Drugs.Count())].Id,
							PrescriptionId = context.Prescriptions.ToList()[random.Next(context.Prescriptions.Count())].Id
						};
						context.Dosages.Add(_dosages);
					}
					context.SaveChanges();
				}
				//	//}


				//	//	// -----------BILL---------------

				if (context.Bills.Count() == 0)
				{
					foreach (var tmp in context.Patients)
					{
						var apps = context.Appointments.Where(x => x.PatientId == tmp.Id).ToList();
						var prescs = context.Prescriptions.Where(x => x.PatientId == tmp.Id).ToList();

						double _docFee = 0;
						foreach (var app in apps)
						{
							_docFee += context.Doctors.Single(x => x.Id == app.DoctorId).Fee;
						}

						double _testFee = 0;
						foreach (var presc in prescs)
						{
							foreach (var medTest in context.MedicalTests.Where(x => x.PrescriptionId == presc.Id))
							{
								_testFee += context.Tests.Single(x => x.Id == medTest.TestId).Fee;
							}
						}

						var bill = new Bill
						{
							BillAmount = (_docFee + _testFee) * 110 / 100,
							PaymentMode = (random.Next(2) == 0) ? "Cash" : "Cards",
							Status = (random.Next(2) == 0) ? true : false,
							PaymentDate = DateTime.Now.AddDays(random.Next(7, 14)),
							PatientId = tmp.Id
						};
						context.Bills.Add(bill);
					}
					context.SaveChanges();
				}

				// // // ----------USERS------------------
				if (context.Users.Count() == 0)
				{
					context.Users.Add(new User("pasan", "6969", false));
					context.Users.Add(new User("Chand", "7070", false));
					context.Users.Add(new User("Tom", "16969", true));
					context.Users.Add(new User("Hardy", "16969", false));
					context.Users.Add(new User("James", "16969", false));
					context.Users.Add(new User("Bond", "16969", true));
					context.SaveChanges();
				}




				//	// Access a Bill object's related Patient details
				//	//using (var context = new DataContext())
				//	//{
				//	//	var bill = context.Bills.Include(b => b.Patient).FirstOrDefault();
				//	//	//Console.WriteLine("Bill Amount: {0}", bill.Amount);
				//	//	//Console.WriteLine("Patient Name: {0}", bill.Patient.Name);
				//	//	//Console.WriteLine("Patient Address: {0}", bill.Patient.Address);
				//	//	//Console.WriteLine("Patient Phone: {0}", bill.Patient.Phone);
				//	//	MessageBox.Show($"Bill Amount: {0} , Patient Name: {1}, Patient Address: {2}, Patient Phone: {3}", bill.BillAmount.ToString(), bill.Patient.FullName, bill.Patient.Address, bill.Patient.Phone);
			}

		}
	}
}
