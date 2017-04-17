using Prism.Events;
using System;
using System.Collections.ObjectModel;
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
    public class AddStudyProgramViewModel : ViewModelBase, ISaveCancel, IDataErrorInfo
    {
        #region Fields

        private AddStudyProgramView view;
        private StudyProgram editSP;
        private ICommand save;
        private ICommand cancel;
        private string name;
        private int studyProgramId;
        private int departmentId;
        private int? selffinancedPlaces;
        private int? budgetPlaces;
        private int? tuition;
        private readonly IEventAggregator eventArgs;

        #endregion

        #region Constructor

        public AddStudyProgramViewModel(AddStudyProgramView view, StudyProgram editSP, IEventAggregator eventArgs)
        {
            this.editSP = editSP;
            this.view = view;
            this.eventArgs = eventArgs;

            Departments = new ObservableCollection<Department>(ServiceDataProvider.GetAllDepartments());

            if (editSP != null)
            {
                Name = editSP.Name;
                DepartmentId = editSP.DepartmentId;
                SelffinancedPlaces = editSP.SelffinancedPlaces;
                BudgetPlaces = editSP.BudgetPlaces;
                Tuition = editSP.Tuition;
            }
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public int StudyProgramId
        {
            get { return studyProgramId; }
            set { studyProgramId = value; }
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

        public int? SelffinancedPlaces
        {
            get { return selffinancedPlaces; }
            set
            {
                selffinancedPlaces = value;
                OnPropertyChanged("SelffinancedPlaces");
            }
        }

        public int? BudgetPlaces
        {
            get { return budgetPlaces; }
            set
            {
                budgetPlaces = value;
                OnPropertyChanged("BudgetPlaces");
            }
        }

        public int? Tuition
        {
            get { return tuition; }
            set
            {
                tuition = value;
                OnPropertyChanged("Tuition");
            }
        }

        public ObservableCollection<Department> Departments { get; set; }

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
                if (editSP != null)
                {
                    editSP.Name = Name;
                    editSP.DepartmentId = DepartmentId;
                    editSP.SelffinancedPlaces = SelffinancedPlaces;
                    editSP.BudgetPlaces = BudgetPlaces;
                    editSP.Tuition = Tuition;

                    ServiceDataProvider.EditStudyProgram(editSP);
                    eventArgs.GetEvent<StudyProgramEvent>().Publish(editSP);
                }
                else
                {
                    StudyProgram newStudyProgram = new StudyProgram()
                    {
                        Name = Name,
                        DepartmentId = DepartmentId,
                        SelffinancedPlaces = SelffinancedPlaces,
                        BudgetPlaces = BudgetPlaces,
                        Tuition = Tuition
                    };

                    ServiceDataProvider.AddStudyProgram(newStudyProgram);
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
            if (!string.IsNullOrEmpty(Name) || DepartmentId == 0)
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
                if (propertyName.Equals(nameof(Name)) && Name != null)
                {
                    if (Name.Length == 0)
                    {
                        return "Enter Study Program name!";
                    }
                    if (Name.Length > 40)
                    {
                        return "Too long name!";
                    }
                    if (!Regex.IsMatch(Name, @"^[a-zA-Z\s]+$"))
                    {
                        return "Only letters are allowed!";
                    }
                }

                return string.Empty;
            }
        }

        #endregion
    }
}
