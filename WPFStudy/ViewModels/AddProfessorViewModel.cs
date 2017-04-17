using Prism.Events;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using WPFStudy.Common;
using WPFStudy.DataProvider;
using WPFStudy.Events;
using WPFStudy.ServiceReference;
using WPFStudy.Views;

namespace WPFStudy.ViewModels
{
    public class AddProfessorViewModel : ViewModelBase, ISaveCancel, IDataErrorInfo
    {
        #region Fields

        private AddProfessorView view;
        private Professor editProf;
        private string nameAndSurname;
        private bool? isAdmin;
        private string education;
        private string address;
        private string birthPlace;
        private DateTime? birthDate;
        private string phone;
        private string username;
        private string password;
        private ICommand save;
        private ICommand cancel;
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Constructor

        public AddProfessorViewModel(AddProfessorView view, Professor editProf, IEventAggregator eventAggregator)
        {
            this.view = view;
            this.editProf = editProf;
            this.eventAggregator = eventAggregator;

            if (editProf != null)
            {
                NameAndSurname = editProf.NameAndSurname;
                IsAdmin = editProf.IsAdmin;
                Address = editProf.Address;
                BirthPlace = editProf.BirthPlace;
                BirthDate = editProf.BirthDate;
                Phone = editProf.Phone;
                Education = editProf.Education;
                Username = editProf.Username;
                Password = editProf.Password;
            }
        }

        #endregion

        #region Properties

        public string NameAndSurname
        {
            get { return nameAndSurname; }
            set
            {
                nameAndSurname = value;
                OnPropertyChanged("NameAndSurname");
            }
        }

        public bool? IsAdmin
        {
            get { return isAdmin; }
            set
            {
                isAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public string BirthPlace
        {
            get { return birthPlace; }
            set
            {
                birthPlace = value;
                OnPropertyChanged("BirthPlace");
            }
        }

        public DateTime? BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string Education
        {
            get { return education; }
            set
            {
                education = value;
                OnPropertyChanged("Education");
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Username");
            }
        }

        #endregion

        #region ISaveCancel

        public ICommand Save
        {
            get
            {
                return save ?? (save = new RelayCommand(p => ExecuteSave(p), p => CanExecuteSave()));
            }
        }

        public ICommand Cancel
        {
            get
            {
                return cancel ?? (cancel = new RelayCommand(p => { view.Close(); }));
            }
        }

        private void ExecuteSave(object param)
        {
            try
            {
                if (editProf != null)
                {
                    editProf.NameAndSurname = NameAndSurname;
                    editProf.IsAdmin = IsAdmin;
                    editProf.Phone = Phone;
                    editProf.Address = Address;
                    editProf.BirthDate = BirthDate;
                    editProf.BirthPlace = BirthPlace;
                    editProf.Education = Education;
                    editProf.Username = Username;
                    editProf.Password = Password;

                    ServiceDataProvider.EditProfessor(editProf);
                    eventAggregator.GetEvent<ProfessorEvent>().Publish(editProf);
                }
                else
                {
                    Professor p = new Professor()
                    {
                        NameAndSurname = NameAndSurname,
                        IsAdmin = IsAdmin,
                        Address = Address,
                        Phone = Phone,
                        BirthDate = BirthDate,
                        BirthPlace = BirthPlace,
                        Education = Education,
                        Username = Username,
                        Password = Password
                    };

                    ServiceDataProvider.AddProfessor(p);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            finally
            {
                view.Close();
            }
        }

        private bool CanExecuteSave()
        {
            return !string.IsNullOrEmpty(NameAndSurname);
        }

        #endregion

        #region IDataErrorInfo

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string propertyName]
        {
            get
            {
                if (propertyName.Equals(nameof(NameAndSurname)) && NameAndSurname != null)
                {
                    if (NameAndSurname.Length == 0)
                    {
                        return "Enter Professor name!";
                    }
                    if (NameAndSurname.Length > 40)
                    {
                        return "Too long name!";
                    }
                    if (!Regex.IsMatch(NameAndSurname, @"^[a-zA-Z\s]+$"))
                    {
                        return "Only letters are allowed!";
                    }
                }
                else if (propertyName.Equals(nameof(Phone)) && Phone != null)
                {
                    if (Phone.Length == 0)
                    {
                        return "Enter Phone Number!";
                    }
                    if (Regex.IsMatch(Phone, @"/^(\s*|\d+)$/"))
                    {
                        return "Only numbers are allowed!";
                    }
                }

                return string.Empty;
            }
        }

        #endregion
    }
}
