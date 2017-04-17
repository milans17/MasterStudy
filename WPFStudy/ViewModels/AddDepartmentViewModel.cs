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
    public class AddDepartmentViewModel : ViewModelBase, ISaveCancel, IDataErrorInfo
    {
        #region Fields

        private AddDepartmentView view;
        private Department editDepartment;
        private string name;
        private int? foundationYear;
        private string departmentHead;
        private string website;
        private ICommand save;
        private ICommand cancel;
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Constructor

        public AddDepartmentViewModel(AddDepartmentView view, Department editDepartment, IEventAggregator eventAggregator)
        {
            this.view = view;
            this.editDepartment = editDepartment;
            this.eventAggregator = eventAggregator;

            if (editDepartment != null)
            {
                Name = editDepartment.Name;
                FoundationYear = editDepartment.FoundationYear;
                DepartmentHead = editDepartment.DepartmentHead;
                Website = editDepartment.Website;
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

        public int? FoundationYear
        {
            get { return foundationYear; }
            set
            {
                foundationYear = value;
                OnPropertyChanged("FoundationYear");
            }
        }

        public string DepartmentHead
        {
            get { return departmentHead; }
            set
            {
                departmentHead = value;
                OnPropertyChanged("DepartmentHead");
            }
        }
                
        public string Website
        {
            get { return website; }
            set
            {
                website = value;
                OnPropertyChanged("Website");
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
                if (editDepartment != null)
                {
                    editDepartment.DepartmentHead = DepartmentHead;
                    editDepartment.FoundationYear = FoundationYear;
                    editDepartment.Name = Name;
                    editDepartment.Website = Website;

                    ServiceDataProvider.EditDepartment(editDepartment);
                    eventAggregator.GetEvent<DepartmentEvent>().Publish(editDepartment);
                }
                else
                {
                    Department newDepartment = new Department()
                    {
                        Name = Name,
                        FoundationYear = FoundationYear,
                        DepartmentHead = DepartmentHead,
                        Website = website
                    };

                    ServiceDataProvider.AddDepartment(newDepartment);
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
            return !string.IsNullOrEmpty(Name);
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
                        return "Enter Department name!";
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
                else if (propertyName.Equals(nameof(FoundationYear)) && FoundationYear != null)
                {
                    if (Regex.IsMatch(FoundationYear.ToString(), @"/^(\s*|\d+)$/"))
                    {
                        return "Only numbers are allowed!";
                    }
                }
                else if (propertyName.Equals(nameof(DepartmentHead)) && DepartmentHead != null)
                {
                    if (DepartmentHead.Length == 0)
                    {
                        return "Enter Department Head name!";
                    }
                    if (DepartmentHead.Length > 40)
                    {
                        return "Too long name!";
                    }
                    if (!Regex.IsMatch(DepartmentHead, @"^[a-zA-Z\s]+$"))
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
