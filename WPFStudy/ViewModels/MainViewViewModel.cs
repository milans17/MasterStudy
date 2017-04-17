using WPFStudy.Common;

namespace WPFStudy.ViewModels
{
    public class MainViewViewModel
    {
        #region Constructor

        public MainViewViewModel()
        {
            StudentViewModel = new StudentViewModel(ApplicationService.Instance.EventAggregator);
            DepartmentViewModel = new DepartmentViewModel();
            StudyProgramViewModel = new StudyProgramViewModel(ApplicationService.Instance.EventAggregator);
            ProfessorViewModel = new ProfessorViewModel();
            CourseViewModel = new CourseViewModel(ApplicationService.Instance.EventAggregator);
            ExamPeriodViewModel = new ExamPeriodViewModel();
            ExamViewModel = new ExamViewModel(ApplicationService.Instance.EventAggregator);
        }

        #endregion

        #region Properties

        public StudentViewModel StudentViewModel { get; set; }
        public DepartmentViewModel DepartmentViewModel { get; set; }
        public StudyProgramViewModel StudyProgramViewModel { get; set; }
        public ProfessorViewModel ProfessorViewModel { get; set; }
        public CourseViewModel CourseViewModel { get; set; }
        public ExamPeriodViewModel ExamPeriodViewModel { get; set; }
        public ExamViewModel ExamViewModel { get; set; }

        #endregion
    }
}
