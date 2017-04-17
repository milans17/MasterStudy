using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFStudy.Common;
using WPFStudy.DataProvider;
using WPFStudy.ServiceReference;
using WPFStudy.Views;
using System.Linq;
using Prism.Events;
using WPFStudy.Events;

namespace WPFStudy.ViewModels
{
    public class CourseViewModel : ViewModelBase, ICommanding
    {
        #region Fields

        private ObservableCollection<Course> courses;
        private ICommand openView;
        private ICommand editView;
        private ICommand removeElement;
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Constructor

        public CourseViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            Courses = new ObservableCollection<Course>(ServiceDataProvider.GetAllCourses());
            ServiceDataProvider.AddCourseNotification += ServiceDataProvider_AddCourseNotification;
            SubscribeToParentTableNameChange(eventAggregator);
        }

        #endregion

        #region Properties

        public ObservableCollection<Course> Courses
        {
            get { return courses; }
            set
            {
                courses = value;
                OnPropertyChanged("Courses");
            }
        }

        #endregion

        #region Methods

        private void SubscribeToParentTableNameChange(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<StudyProgramEvent>().Subscribe(p =>
            {
                var editSP = Courses.Where(x => x.StudyProgramId == p.StudyProgramId);

                foreach (var item in editSP)
                {
                    if (item.StudyProgramName != p.Name)
                    {
                        item.StudyProgramName = p.Name;
                    }
                }
            });

            eventAggregator.GetEvent<ProfessorEvent>().Subscribe(p =>
            {
                var editProfessorName = Courses.Where(x => x.ProfessorId == p.ProfessorId);

                foreach (var item in editProfessorName)
                {
                    if (item.ProfessorName != p.NameAndSurname)
                    {
                        item.ProfessorName = p.NameAndSurname;
                    }
                }
            });
        }

        private void ServiceDataProvider_AddCourseNotification(object sender, Events.CourseEventArgs e)
        {
            Courses.Add(e.Course);
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
            AddCourseView courseView = new AddCourseView();
            courseView.ShowDialog();
        }

        private void ExecuteEditView(object p)
        {
            if (p != null && p is Course)
            {
                var course = p as Course;

                AddCourseView courseView = new AddCourseView(course);
                courseView.ShowDialog();
            }
        }

        private void ExecuteRemove(object p)
        {
            if (p != null && p is Course)
            {
                var course = p as Course;

                ServiceDataProvider.DeleteCourse(course.CourseId);
                Courses.Remove(course);
            }
        }

        #endregion
    }
}
