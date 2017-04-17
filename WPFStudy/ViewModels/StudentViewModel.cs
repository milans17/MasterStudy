using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFStudy.DataProvider;
using WPFStudy.ServiceReference;
using System.Linq;
using WPFStudy.Events;
using WPFStudy.Common;
using Prism.Events;

namespace WPFStudy.ViewModels
{
    public class StudentViewModel : ViewModelBase, ICommanding
    {
        #region Fields

        private ObservableCollection<Student> students;
        private ICommand openView;
        private ICommand editView;
        private ICommand removeElement;
        private readonly IEventAggregator eventAggr;

        #endregion

        #region Constructor

        public StudentViewModel(IEventAggregator eventAggr)
        {
            this.eventAggr = eventAggr;
            students = new ObservableCollection<Student>(ServiceDataProvider.GetAllStudents());

            ServiceDataProvider.AddStudentNotification += ServiceDataProvider_AddStudentNotification;
            SubscribeToParentTableNameChange(eventAggr);
        }

        #endregion

        #region Properties

        public ObservableCollection<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                OnPropertyChanged("Students");
            }
        }

        #endregion

        #region Methods

        private void SubscribeToParentTableNameChange(IEventAggregator eventAggr)
        {
            eventAggr.GetEvent<DepartmentEvent>().Subscribe(p =>
            {
                var departments = Students.Where(x => x.DepartmentId == p.DepartmentId);

                foreach (var item in departments)
                {
                    if (item.DepartmentName != p.Name)
                    {
                        item.DepartmentName = p.Name;
                    }
                }
            });

            eventAggr.GetEvent<StudyProgramEvent>().Subscribe(s =>
            {
                var students = Students.Where(x => x.StudyProgramId == s.StudyProgramId);

                foreach (var item in students)
                {
                    if (item.StudyProgramName != s.Name)
                    {
                        item.StudyProgramName = s.Name;
                    }
                }
            });
        }

        private void ServiceDataProvider_AddStudentNotification(object sender, StudentEventArgs e)
        {
            Students.Add(e.Student);
        }

        #endregion

        #region ICommanding

        public ICommand OpenView
        {
            get
            {
                return openView ?? (openView = new RelayCommand(p => ExecuteOpening(p)));
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

        private void ExecuteOpening(object p)
        {
            AddStudentView studentView = new AddStudentView();
            studentView.ShowDialog();
        }

        private void ExecuteEdit(object p)
        {
            if (p != null && p is Student)
            {
                var student = p as Student;

                AddStudentView studentView = new AddStudentView(student);
                studentView.ShowDialog();
            }
        }

        private void ExecuteRemove(object p)
        {
            if (p != null && p is Student)
            {
                var student = p as Student;

                ServiceDataProvider.DeleteStudent(student.StudentId);
                Students.Remove(student);
            }
        }

        #endregion
    }
}
