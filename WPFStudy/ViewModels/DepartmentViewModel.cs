using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;
using WPFStudy.Common;
using WPFStudy.DataProvider;
using WPFStudy.Events;
using WPFStudy.ServiceReference;
using WPFStudy.Views;

namespace WPFStudy.ViewModels
{
    public class DepartmentViewModel : ViewModelBase, ICommanding
    {
        #region Fields

        private ObservableCollection<Department> departments;
        private ICommand openView;
        private ICommand editView;
        private ICommand removeElement;

        #endregion

        #region Constructor

        public DepartmentViewModel()
        {
            Departments =  new ObservableCollection<Department>(ServiceDataProvider.GetAllDepartments());
            ServiceDataProvider.AddDepartmentNotification += ServiceDataProvider_AddDepartmentNotification;
        }

        #endregion

        #region Properties

        public ObservableCollection<Department> Departments
        {
            get { return departments; }
            set
            {
                departments = value;
                OnPropertyChanged("Departments");
            }
        }

        #endregion

        #region Methods

        private void ServiceDataProvider_AddDepartmentNotification(object sender, DepartmentEventArgs e)
        {
            if (e.Department != null)
            {
                Departments.Add(e.Department);
            }
            else
            {
                throw new ArgumentException("Department");
            }
        }

        #endregion

        #region ICommanding

        public ICommand OpenView
        {
            get
            {
                return openView ?? (openView = new RelayCommand(p => ExecuteOpeningView(p)));
            }
        }

        public ICommand EditView
        {
            get
            {
                return editView ?? (editView = new RelayCommand(p => ExecuteEditView(p)));
            }
        }

        public ICommand RemoveElement
        {
            get
            {
                return removeElement ?? (removeElement = new RelayCommand(p => ExecuteRemove(p)));
            }
        }

        private void ExecuteOpeningView(object p)
        {
            AddDepartmentView departmentView = new AddDepartmentView();
            departmentView.ShowDialog();
        }

        private void ExecuteEditView(object p)
        {
            if (p != null && p is Department)
            {
                var department = p as Department;

                AddDepartmentView studentView = new AddDepartmentView(department);
                studentView.ShowDialog();
            }
        }

        private void ExecuteRemove(object p)
        {
            if (p != null && p is Department)
            {
                try
                {
                    var department = p as Department;

                    ServiceDataProvider.DeleteDepartment(department.DepartmentId);
                    Departments.Remove(department);
                }
                catch (FaultException<DeleteFault> fe)
                {
                    MessageBox.Show(string.Format("{0} {1}", fe.Detail.Message, fe.Detail.Description));
                }
            }
        }

        #endregion
    }
}
