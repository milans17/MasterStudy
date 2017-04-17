using Prism.Events;
using System;
using System.Collections.Generic;
using WPFStudy.Common;
using WPFStudy.Events;
using WPFStudy.ServiceReference;

namespace WPFStudy.DataProvider
{
    public class ServiceDataProvider
    {
        #region Fields

        private static ServiceClient proxy;
        private static readonly IEventAggregator eventArgs = ApplicationService.Instance.EventAggregator;

        #endregion

        #region Constructor

        static ServiceDataProvider()
        {
            proxy = new ServiceClient();
        }

        #endregion

        #region Events

        public static event EventHandler<StudentEventArgs> AddStudentNotification;
        public static event EventHandler<DepartmentEventArgs> AddDepartmentNotification;
        public static event EventHandler<StudyProgramEventArgs> AddStudyProgramNotification;
        public static event EventHandler<ProfessorEventArgs> AddProfessorNotification;
        public static event EventHandler<CourseEventArgs> AddCourseNotification;
        public static event EventHandler<ExamPeriodEventArgs> AddExamPeriodNotification;
        public static event EventHandler<ExamEventArgs> AddExamNotification;

        #endregion

        #region Students

        public static IEnumerable<Student> GetAllStudents()
        {
            return proxy.GetAllStudents();
        }

        public static void AddStudent(Student s)
        {
            var newStudent = proxy.AddStudent(s);
            AddStudentNotification?.Invoke(typeof(ServiceDataProvider), new StudentEventArgs(newStudent));
        }

        public static void EditStudent(Student editS)
        {
            proxy.EditStudent(editS);
        }

        public static void DeleteStudent(int id)
        {
            proxy.DeleteStudent(id);
        }
        #endregion

        #region Departments
        public static IEnumerable<Department> GetAllDepartments()
        {
            return proxy.GetAllDepartments();
        }

        public static void AddDepartment(Department d)
        {
            var newDepartment = proxy.AddDepartment(d);

            AddDepartmentNotification?.Invoke(typeof(ServiceDataProvider), new DepartmentEventArgs(newDepartment));
        }

        public static void EditDepartment(Department editD)
        {
            proxy.EditDepartment(editD);
        }

        public static void DeleteDepartment(int id)
        {
            proxy.DeleteDepartment(id);
        }

        public static IEnumerable<StudyProgram> GetAllSPForDepartmentId(int id)
        {
            return proxy.GetAllSPForDepartmentId(id);
        }
        #endregion

        #region StudyPrograms
        public static IEnumerable<StudyProgram> GetAllStudyPrograms()
        {
            return proxy.GetAllStudyPrograms();
        }

        public static void AddStudyProgram(StudyProgram sp)
        {
            var newStudyProgram = proxy.AddStudyProgram(sp);

            AddStudyProgramNotification?.Invoke(typeof(ServiceDataProvider), new StudyProgramEventArgs(newStudyProgram));
        }

        public static void EditStudyProgram(StudyProgram editSP)
        {
            proxy.EditStudyProgram(editSP);
        }

        public static void DeleteStudyProgram(int id)
        {
            proxy.DeleteStudyProgram(id);
        }
        #endregion

        #region Professors
        public static IEnumerable<Professor> GetAllProfessors()
        {
            return proxy.GetAllProfessors();
        }

        public static void AddProfessor(Professor p)
        {
            var newProfessor = proxy.AddProfessor(p);

            AddProfessorNotification?.Invoke(typeof(ServiceDataProvider), new ProfessorEventArgs(newProfessor));
        }

        public static void EditProfessor(Professor editP)
        {
            proxy.EditProfessor(editP);
        }

        public static void DeleteProfessor(int id)
        {
            proxy.DeleteProfessor(id);
        }
        #endregion

        #region Courses
        public static IEnumerable<Course> GetAllCourses()
        {
            return proxy.GetAllCourses();
        }

        public static void AddCourse(Course c)
        {
            var newCourse = proxy.AddCourse(c);

            AddCourseNotification?.Invoke(typeof(ServiceDataProvider), new CourseEventArgs(newCourse));
        }

        public static void EditCourse(Course editC)
        {
            proxy.EditCourse(editC);
        }

        public static void DeleteCourse(int id)
        {
            proxy.DeleteCourse(id);
        }
        #endregion

        #region ExamPeriods
        public static IEnumerable<ExamPeriod> GetAllExamPeriods()
        {
            return proxy.GetAllExamPeriods();
        }

        public static void AddExamPeriod(ExamPeriod ep)
        {
            var newExamPeriod = proxy.AddExamPeriod(ep);

            AddExamPeriodNotification?.Invoke(typeof(ServiceDataProvider), new ExamPeriodEventArgs(newExamPeriod));
        }

        public static void EditExamPeriod(ExamPeriod editEP)
        {
            proxy.EditExamPeriod(editEP);
        }

        public static void DeleteExamPeriod(int id)
        {
            proxy.DeleteExamPeriod(id);
        }
        #endregion

        #region Exams
        public static IEnumerable<Exam> GetAllExams()
        {
            return proxy.GetAllExams();
        }

        public static void AddExam(Exam e)
        {
            var newExam = proxy.AddExam(e);

            AddExamNotification?.Invoke(typeof(ServiceDataProvider), new ExamEventArgs(newExam));
        }

        public static void EditExam(Exam editE)
        {
            proxy.EditExam(editE);
        }

        public static void DeleteExam(int id)
        {
            proxy.DeleteExam(id);
        }
        #endregion

        #region WPFProfessor

        public static IEnumerable<ExamPeriod> GetActiveExamPeriods()
        {
            return proxy.GetActiveExamPeriods();
        }

        public static IEnumerable<Course> GetProfessorCourses(int professorId)
        {
            return proxy.GetProfessorCourses(professorId);
        }

        public static Dictionary<List<ExamRegistration>, List<object>> GetRegistredStudentsForExam(int courseId, int examPeriodId)
        {
            return proxy.GetRegistredStudentsForExam(courseId, examPeriodId);
        }

        public static bool SetExamResults(List<ExamResult> examResults)
        {
            return proxy.SetExamResults(examResults);
        }

        public static bool ValidateExamPeriodActivity(DateTime startDate)
        {
            return proxy.ValidateExamPeriodActivity(startDate);
        }

        public static bool ValidateExamInExamPeriod(int examPeriodId, int courseId)
        {
            return proxy.ValidateExamInExamPeriod(examPeriodId, courseId);
        }

        #endregion
    }
}
