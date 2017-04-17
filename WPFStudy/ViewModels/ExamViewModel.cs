using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WPFStudy.Common;
using WPFStudy.DataProvider;
using WPFStudy.Events;
using WPFStudy.ServiceReference;
using WPFStudy.Views;

namespace WPFStudy.ViewModels
{
    public class ExamViewModel : ICommanding
    {
        #region Fields

        private ICommand openView;
        private ICommand editView;
        private ICommand removeElement;
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Constructor

        public ExamViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            Exams = new ObservableCollection<Exam>(ServiceDataProvider.GetAllExams());
            ServiceDataProvider.AddExamNotification += ServiceDataProvider_AddExamNotification;
            eventAggregator.GetEvent<ExamPeriodEvent>().Subscribe(p => SubcribeToParentTableNameChange(p));
            eventAggregator.GetEvent<CourseEvent>().Subscribe(p => SubcribeToParentTableNameChange(p));
        }

        #endregion

        #region Methods

        private void ServiceDataProvider_AddExamNotification(object sender, Events.ExamEventArgs e)
        {
            Exams.Add(e.Exam);
        }

        private void SubcribeToParentTableNameChange<T>(T p)
        {
            if (p is ExamPeriod)
            {
                var ep = p as ExamPeriod;

                var editExamPeriodName = Exams.Where(x => x.ExamPeriodId == ep.ExamPeriodId);

                foreach (var item in editExamPeriodName)
                {
                    if (item.ExamPeriodName != ep.Name)
                    {
                        item.ExamPeriodName = ep.Name;
                    }
                }
            }
            else
            {
                var c = p as Course;

                var editCourseName = Exams.Where(x => x.CourseId == c.CourseId);

                foreach (var item in editCourseName)
                {
                    if (item.CourseName != c.Name)
                    {
                        item.CourseName = c.Name;
                    }
                }
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<Exam> Exams { get; set; }

        #endregion

        #region ICommanding

        public ICommand OpenView
        {
            get
            {
                return openView ?? (openView = new RelayCommand(p => ExecuteOpen()));
            }
        }

        public ICommand EditView
        {
            get
            {
                return editView ?? (editView = new RelayCommand(p => ExecuteEdit(p)));
            }
        }

        public ICommand RemoveElement
        {
            get
            {
                return removeElement ?? (removeElement = new RelayCommand(p => ExecuteRemove(p)));
            }
        }

        private void ExecuteOpen()
        {
            AddExamView view = new AddExamView();
            view.ShowDialog();
        }

        private void ExecuteEdit(object p)
        {
            if (p != null && p is Exam)
            {
                var exam = p as Exam;

                AddExamView view = new AddExamView(exam);
                view.ShowDialog();
            }
        }

        private void ExecuteRemove(object p)
        {
            if (p != null && p is Exam)
            {
                var exam = p as Exam;

                ServiceDataProvider.DeleteExam(exam.ExamId);
                Exams.Remove(exam);
            }
        }

        #endregion
    }
}
