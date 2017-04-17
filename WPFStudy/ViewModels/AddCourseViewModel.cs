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
    public class AddCourseViewModel : ViewModelBase, ISaveCancel, IDataErrorInfo
    {
        #region Fields

        private Course editCourse;
        private AddCourseView view;
        private ICommand save;
        private ICommand cancel;
        private string name;
        private int studyProgramId;
        private int professorId;
        private string assistant;
        private int? etcs;
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Constructor

        public AddCourseViewModel(AddCourseView view, Course editCourse, IEventAggregator eventAggregator)
        {
            this.view = view;
            this.editCourse = editCourse;
            this.eventAggregator = eventAggregator;

            if (editCourse != null)
            {
                Name = editCourse.Name;
                StudyProgramId = editCourse.StudyProgramId;
                ProfessorId = editCourse.ProfessorId;
                Assistant = editCourse.Assistant;
                ETCS = editCourse.ETCS;
            }

            StudyPrograms = new ObservableCollection<StudyProgram>(ServiceDataProvider.GetAllStudyPrograms());
            Professors = new ObservableCollection<Professor>(ServiceDataProvider.GetAllProfessors());
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int StudyProgramId
        {
            get { return studyProgramId; }
            set { studyProgramId = value; }
        }

        public int ProfessorId
        {
            get { return professorId; }
            set { professorId = value; }
        }

        public string Assistant
        {
            get { return assistant; }
            set { assistant = value; }
        }

        public int? ETCS
        {
            get { return etcs; }
            set { etcs = value; }
        }

        public ObservableCollection<StudyProgram> StudyPrograms { get; set; }

        public ObservableCollection<Professor> Professors { get; set; }

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
                if (editCourse != null)
                {
                    editCourse.Name = Name;
                    editCourse.StudyProgramId = StudyProgramId;
                    editCourse.StudyProgramName = view.cbxStudyPrograms.Text;
                    editCourse.ProfessorId = ProfessorId;
                    editCourse.ProfessorName = view.cbxProfessors.Text;
                    editCourse.Assistant = Assistant;
                    editCourse.ETCS = ETCS;

                    ServiceDataProvider.EditCourse(editCourse);
                    eventAggregator.GetEvent<CourseEvent>().Publish(editCourse);
                }
                else
                {
                    Course newCourse = new Course()
                    {
                        Name = Name,
                        StudyProgramId = StudyProgramId,
                        ProfessorId = ProfessorId,
                        Assistant = Assistant,
                        ETCS = ETCS
                    };

                    ServiceDataProvider.AddCourse(newCourse);
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
            if (string.IsNullOrEmpty(Name) || ProfessorId == 0 || StudyProgramId == 0)
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
                        return "Enter Course name!";
                    }
                    if (Name.Length > 40)
                    {
                        return "Too long name!";
                    }
                }
                else if (propertyName.Equals(nameof(ETCS)) && ETCS != null)
                {
                    if (Regex.IsMatch(ETCS.ToString(), @"/^(\s*|\d+)$/"))
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
