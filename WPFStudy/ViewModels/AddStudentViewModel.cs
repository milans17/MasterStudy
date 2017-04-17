using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using WPFStudy.Common;
using WPFStudy.DataProvider;
using WPFStudy.Events;
using WPFStudy.ServiceReference;

namespace WPFStudy.ViewModels
{
    public class AddStudentViewModel : ViewModelBase, ISaveCancel, IDataErrorInfo
    {
        #region Fields

        private ICommand save;
        private ICommand cancel;
        private AddStudentView view;
        private Student editStudent;
        private string studyProgramName;
        private string studentName;
        private int studyProgramId;
        private int departmentId;
        private string address;
        private string birthPlace;
        private string phone;
        private int? studyYear;
        private int? balance;
        private string username;
        private string password;
        private DateTime? birthDate;
        private ObservableCollection<StudyProgram> sp;
        private readonly IEventAggregator eventAggr;

        #endregion

        #region Constructor

        public AddStudentViewModel(AddStudentView view, Student editStudent, IEventAggregator eventAggr)
        {
            this.view = view;
            this.editStudent = editStudent;
            this.eventAggr = eventAggr;

            if (editStudent != null)
            {
                StudentName = editStudent.NameAndSurname;
                StudyProgramId = editStudent.StudyProgramId;
                DepartmentId = editStudent.DepartmentId;
                Address = editStudent.Address;
                Phone = editStudent.Phone;
                BirthPlace = editStudent.BirthPlace;
                BirthDate = editStudent.BirthDate;
                StudyYear = editStudent.StudyYear;
                Balance = editStudent.Balance;
                Username = editStudent.Username;
                Password = editStudent.Password;
            }
            
            StudyPrograms = new ObservableCollection<StudyProgram>(ServiceDataProvider.GetAllStudyPrograms());
            Departments = ServiceDataProvider.GetAllDepartments();

            view.cbxDepartments.SelectionChanged += CbxDepartments_SelectionChanged;
        }

        #endregion

        #region Properties

        public ObservableCollection<StudyProgram> StudyPrograms { get; set; }

        public ObservableCollection<StudyProgram> SP
        {
            get { return sp; }
            set
            {
                sp = value;
                OnPropertyChanged("SP");
            }
        }

        public IEnumerable<Department> Departments { get; set; }

        public int StudyProgramId
        {
            get { return studyProgramId; }
            set
            {
                studyProgramId = value;
                OnPropertyChanged("StudyProgramId");
            }
        }

        public int DepartmentId
        {
            get { return departmentId; }
            set
            {
                departmentId = value;
                OnPropertyChanged("DepartmentId");
            }
        }


        public string StudyProgramName
        {
            get { return studyProgramName; }
            set
            {
                studyProgramName = value;
                OnPropertyChanged("StudyProgramName");
            }
        }

        public string StudentName
        {
            get { return studentName; }
            set
            {
                studentName = value;
                OnPropertyChanged("StudentName");
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

        public int? StudyYear
        {
            get { return studyYear; }
            set
            {
                studyYear = value;
                OnPropertyChanged("StudyYear");
            }
        }

        public int? Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
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

        #region Methods

        private void CbxDepartments_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedDepartmentId = DepartmentId;

            ObservableCollection<StudyProgram> SP = new ObservableCollection<StudyProgram>(ServiceDataProvider.GetAllSPForDepartmentId(selectedDepartmentId));

            if (SP.Count == 0)
            {
                e.Handled = true;
            }
            view.cbxStudyPrograms.ItemsSource = SP;
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
                if (editStudent != null)
                {
                    editStudent.NameAndSurname = StudentName;
                    editStudent.StudyProgramId = StudyProgramId;
                    editStudent.StudyProgramName = view.cbxStudyPrograms.Text;
                    editStudent.DepartmentId = DepartmentId;
                    editStudent.DepartmentName = view.cbxDepartments.Text;
                    editStudent.Address = Address;
                    editStudent.Balance = Balance;
                    editStudent.BirthDate = BirthDate;
                    editStudent.BirthPlace = BirthPlace;
                    editStudent.StudyYear = StudyYear;
                    editStudent.Phone = Phone;
                    editStudent.Username = Username;
                    editStudent.Password = Password;

                    ServiceDataProvider.EditStudent(editStudent);
                    eventAggr.GetEvent<StudentEvent>().Publish(editStudent);
                }
                else
                {
                    Student newStudent = new Student()
                    {
                        NameAndSurname = StudentName,
                        StudyProgramId = StudyProgramId,
                        DepartmentId = DepartmentId,
                        Address = Address,
                        Balance = Balance,
                        BirthDate = BirthDate,
                        BirthPlace = BirthPlace,
                        Phone = Phone,
                        StudyYear = StudyYear,
                        Username = Username,
                        Password = Password,
                    };

                    ServiceDataProvider.AddStudent(newStudent);
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
            if (string.IsNullOrEmpty(StudentName) || DepartmentId == 0 || StudyProgramId == 0)
                return false;
            else
                return true;
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
                if (propertyName.Equals(nameof(StudentName)) && StudentName != null)
                {
                    if (StudentName.Length == 0)
                    {
                        return "Enter Student name!";
                    }
                    if (StudentName.Length > 40)
                    {
                        return "Too long name!";
                    }
                    if (!Regex.IsMatch(StudentName, @"^[a-zA-Z\s]+$"))
                    {
                      return "Only letters are allowed!";
                    }
                }
                else if (propertyName.Equals(nameof(BirthPlace)) && BirthPlace != null)
                {
                    if (BirthPlace.Length == 0)
                    {
                        return "Enter Birth Place!";
                    }
                    if (BirthPlace.Length > 40)
                    {
                        return "Too long Birth Place!";
                    }
                    if (!Regex.IsMatch(BirthPlace, @"^[a-zA-Z\s]+$"))
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
                else if (propertyName.Equals(nameof(Balance)) && Balance != null)
                {
                    if (Balance == null)
                    {
                        return "Enter Balance Number!";
                    }
                    if (Regex.IsMatch(Balance.ToString(), @"/^(\s*|\d+)$/"))
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
