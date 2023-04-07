﻿using HMS.MVVM.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMS.MVVM.ViewModel
{
	public class EditPatientWindowVM : INotifyPropertyChanged, ICloseWindows
	{
		// #begin INotifyPropertyChanged Interface 
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		// #end

		// #begin for ICloseWindows
		public Action CloseAction { get; internal set; }
		public Action Close { get; set; }
		// #end

		private string _fullName;

		public string FullName { get { return _fullName; } set { _fullName = value; OnPropertyChanged(nameof(FullName)); } }

		private string _email;

		public string Email { get { return _email; } set { _email = value; OnPropertyChanged(nameof(Email)); } }

		private DateTime _dateOfBirth;

		public DateTime DateOfBirth { get { return _dateOfBirth; } set { _dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth)); } }

		public List<string> _genders;
		public List<string> Genders { get { return _genders; } set { _genders = value; OnPropertyChanged(nameof(Genders)); } }

		private string _gender;

		public string Gender { get { return _gender; } set { _gender = value; OnPropertyChanged(nameof(Gender)); } }


		private string _phone;

		public string Phone { get { return _phone; } set { _phone = value; OnPropertyChanged(nameof(Phone)); } }

		private string _blood;

		public string Blood { get { return _blood; } set { _blood = value; OnPropertyChanged(nameof(Blood)); } }

		private string _address;

		public string Address { get { return _address; } set { _address = value; OnPropertyChanged(nameof(Address)); } }

		private string _weight;

		public string Weight { get { return _weight; } set { _weight = value; OnPropertyChanged(nameof(Weight)); } }

		private string _height;

		public string Height { get { return _height; } set { _height = value; OnPropertyChanged(nameof(Height)); } }


		// Close Button Command
		private DelegateCommand _closeCommand;
		public DelegateCommand CloseCommand =>
			_closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

		void ExecuteCloseCommand()
		{
			using (DataContext context = new DataContext())
			{
				var pat_ = context.Patients.Single(x => x.IsPatientSelected == true);
				pat_.IsPatientSelected = false;
				context.SaveChanges();
			}

			Close?.Invoke();
		}

		// Create Button Command
		private DelegateCommand _createCommand;
		public DelegateCommand CreateCommand =>
			_createCommand ?? (_createCommand = new DelegateCommand(ExecuteCreateCommand));


		void ExecuteCreateCommand()
		{
			using (DataContext context = new DataContext())
			{
				Patient tmp = context.Patients.Single(x => x.IsPatientSelected == true);
				if (tmp != null)
				{
					//MessageBox.Show("Is selected true for selected student");
					//context.Students.Remove(tmp);
					//if (_firstName == null) _firstName = tmp.FirstName;
					//if (_lastName == null)  _lastName = tmp.LastName;
					//if (_gender == null) _gender = tmp.Gender.ToString();
					//if (_imagei == null) _imagei = tmp.Image;
					//if (_dateOfBirth == DateTime.ParseExact("2000-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture)) _dateOfBirth = DateTime.Parse(tmp.DateOfBirth);
					//if (_gPA == null) _gPA = tmp.GPA.ToString();
					//if (_email == null) _email= tmp.Email;
					//tmp.IsSelected = false;
					//context.Students.Add(new Student(_firstName, _lastName, _gender[0], _imagei, _dateOfBirth.ToShortDateString(), Convert.ToDouble(_gPA), _email));
					tmp.FullName = _fullName;
					tmp.Email = _email;
					tmp.BirthDay = _dateOfBirth.ToShortDateString();
					tmp.Gender = _gender[0];
					tmp.Phone = _phone;
					tmp.BloodGroup = _blood;
					tmp.Address = _address;
					tmp.Weight = Convert.ToDouble(_weight);
					tmp.Height = Convert.ToDouble(_height);
					tmp.IsPatientSelected = false;
					context.SaveChanges();
				}
				else
				{
					MessageBox.Show("Please select a patient again");
				}
			}
			MessageBox.Show("Refresh the patient records to see the changes 😊");
			Close?.Invoke();
		}

		public EditPatientWindowVM()
		{
			Genders = new List<string> { "Male", "Female" };
			//string dateString = "2000-01-01";
			//DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
			//_dateOfBirth = date;
			using (DataContext context = new DataContext())
			{
				//context.Patients.Single(x => x.Id == parameter.Id).IsPatientSelected = true;
				//context.SaveChanges();
				Patient pat_ = context.Patients.Single(x => x.IsPatientSelected == true);
				_fullName = pat_.FullName;
				_email = pat_.Email;
				_dateOfBirth = DateTime.Parse(pat_.BirthDay);
				_gender = pat_.Gender.ToString() == "M" ? "Male" : "Female";
				_phone = pat_.Phone;
				_blood = pat_.BloodGroup;
				_address = pat_.Address;
				_weight = pat_.Weight.ToString();
				_height = pat_.Height.ToString();

			}
		}

	}
}
