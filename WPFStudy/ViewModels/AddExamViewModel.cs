using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPFStudy.Common;
using WPFStudy.DataProvider;
using WPFStudy.ServiceReference;
using WPFStudy.Views;

namespace WPFStudy.ViewModels
{
    public class AddExamViewModel : ViewModelBase, ISaveCancel, IDataErrorInfo
    {
        #region Fields

        private AddExamView view;
        private Exam editExam;
        private int examPeriodId;
        private int courseId;
        private DateTime? dateAndTime = DateTime.Now.ToLocalTime();
        private string place;
        private int? price;
        private bool? isPassed;
        private ICommand save;
        private ICommand cancel;

        #endregion

        #region Constructor

        public AddExamViewModel(AddExamView view, Exam editExam)
        {
            this.view = view;
            this.editExam = editExam;

            ExamPeriods = new ObservableCollection<ExamPeriod>(ServiceDataProvider.GetAllExamPeriods());
            Courses = new ObservableCollection<Course>(ServiceDataProvider.GetAllCourses());

            if (editExam != null)
            {
                ExamPeriodId = editExam.ExamPeriodId;
                CourseId = editExam.CourseId;
                DateAndTime = editExam.DateAndTime;
                Place = editExam.Place;
                Price = editExam.Price;
                IsPassed = editExam.IsPassed;
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<ExamPeriod> ExamPeriods { get; set; }
        public ObservableCollection<Course> Courses { get; set; }

        public int ExamPeriodId
        {
            get { return examPeriodId; }
            set {
                examPeriodId = value;
                OnPropertyChanged("ExamPeriodId");
            }
        }


        public int CourseId
        {
            get { return courseId; }
            set {
                courseId = value;
                OnPropertyChanged("CourseId");
            }
        }

        public DateTime? DateAndTime
        {
            get { return dateAndTime; }
            set {
                dateAndTime = value;
                OnPropertyChanged("DateAndTime");
            }
        }

        public string Place
        {
            get { return place; }
            set {
                place = value;
                OnPropertyChanged("Place");
            }
        }

        public int? Price
        {
            get { return price; }
            set { price = value; }
        }


        public bool? IsPassed
        {
            get { return isPassed; }
            set { isPassed = value; }
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
                if (!ServiceDataProvider.ValidateExamInExamPeriod(ExamPeriodId, CourseId))
                {
                    MessageBox.Show("Exam for Course already exists in Exam Period!", "Exam Validation",
                        MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }

                if (editExam != null)
                {
                    editExam.ExamPeriodId = ExamPeriodId;
                    editExam.CourseId = CourseId;
                    editExam.DateAndTime = DateAndTime.Value;
                    editExam.Place = Place;
                    editExam.Price = Price;
                    editExam.IsPassed = IsPassed;

                    ServiceDataProvider.EditExam(editExam);
                }
                else
                {
                    Exam exam = new Exam()
                    {
                        ExamPeriodId = ExamPeriodId,
                        CourseId = CourseId,
                        DateAndTime = DateAndTime.Value,
                        Place = Place,
                        Price = Price,
                        IsPassed = IsPassed
                    };

                    ServiceDataProvider.AddExam(exam);
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
            if (ExamPeriodId == 0 || CourseId == 0 || !DateAndTime.HasValue || string.IsNullOrEmpty(Place))
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
                if (propertyName.Equals(nameof(DateAndTime)) && DateAndTime != null)
                {
                    if (!DateAndTime.HasValue)
                    {
                        return "Enter Date and Time for Exam!";
                    }
                }
                if (propertyName.Equals(nameof(Place)) && Place != null)
                {
                    if (Place.Length == 0)
                    {
                        return "Enter Place name!";
                    }
                    if (Place.Length > 40)
                    {
                        return "Too long name!";
                    }
                }

                return string.Empty;
            }
        }

        #endregion
    }
}
