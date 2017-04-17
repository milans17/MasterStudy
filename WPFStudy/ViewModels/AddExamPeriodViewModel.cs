using Prism.Events;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPFStudy.Common;
using WPFStudy.DataProvider;
using WPFStudy.Events;
using WPFStudy.ServiceReference;
using WPFStudy.Views;

namespace WPFStudy.ViewModels
{
    public class AddExamPeriodViewModel : ViewModelBase, ISaveCancel, IDataErrorInfo
    {
        #region Fields

        private ExamPeriod editExamPeriod;
        private AddExamPeriodView view;
        private string name;
        private DateTime? startDate = null;
        private DateTime? endDate = null;
        private int schoolYear;
        private bool isActive;
        private bool? isApsolvent;
        private ICommand save;
        private ICommand cancel;
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Constructor

        public AddExamPeriodViewModel(AddExamPeriodView view, ExamPeriod editExamPeriod, IEventAggregator eventAggregator)
        {
            this.editExamPeriod = editExamPeriod;
            this.view = view;
            this.eventAggregator = eventAggregator;

            if (editExamPeriod != null)
            {
                Name = editExamPeriod.Name;
                StartDate = editExamPeriod.StartDate;
                EndDate = editExamPeriod.EndDate;
                SchoolYear = editExamPeriod.SchoolYear;
                IsActive = editExamPeriod.IsActive;
                IsApsolvent = editExamPeriod.IsApsolvent;
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

        public DateTime? StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public int SchoolYear
        {
            get { return schoolYear; }
            set
            {
                schoolYear = value;
                OnPropertyChanged("SchoolYear");
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        public bool? IsApsolvent
        {
            get { return isApsolvent; }
            set { isApsolvent = value; }
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

        private void ExecuteSave(object p)
        {
            try
            {
                if (!ServiceDataProvider.ValidateExamPeriodActivity(StartDate.Value))
                {
                    MessageBox.Show("Active Exam Period already exists in defined period!", "Exam Period Validation",
                        MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }

                if (editExamPeriod != null)
                {
                    editExamPeriod.Name = Name;
                    editExamPeriod.StartDate = StartDate.Value;
                    editExamPeriod.EndDate = EndDate.Value;
                    editExamPeriod.SchoolYear = SchoolYear;
                    editExamPeriod.IsActive = IsActive;
                    editExamPeriod.IsApsolvent = IsApsolvent;

                    ServiceDataProvider.EditExamPeriod(editExamPeriod);
                    eventAggregator.GetEvent<ExamPeriodEvent>().Publish(editExamPeriod);
                }
                else
                {
                    ExamPeriod ep = new ExamPeriod()
                    {
                        Name = Name,
                        StartDate = StartDate.Value,
                        EndDate = EndDate.Value,
                        SchoolYear = SchoolYear,
                        IsActive = IsActive,
                        IsApsolvent = IsApsolvent
                    };

                    ServiceDataProvider.AddExamPeriod(ep);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                view.Close();
            }
        }

        private bool CanExecuteSave()
        {
            if (string.IsNullOrEmpty(Name) || !StartDate.HasValue || !EndDate.HasValue)
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
                        return "Enter Exam Period name!";
                    }
                    if (Name.Length > 40)
                    {
                        return "Too long name!";
                    }
                }
                else if (propertyName.Equals(nameof(StartDate)) && StartDate != null && EndDate != null)
                {
                    if (StartDate > EndDate)
                    {
                        return "Start date must be less than End date!";
                    }
                }
                else if (propertyName.Equals(nameof(EndDate)) && StartDate != null && EndDate != null)
                {
                    if (EndDate < StartDate)
                    {
                        return "End date must be higher than Start date!";
                    }
                }

                return string.Empty; 
            }
        }

        #endregion
    }
}
