using Repository.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WCFStudy
{
    [ServiceContract]
    public interface IService
    {
        #region Students
        [OperationContract]
        IEnumerable<Student> GetAllStudents();

        [OperationContract]
        Student AddStudent(Student newStudent);

        [OperationContract]
        void EditStudent(Student editedStudent);

        [OperationContract]
        void DeleteStudent(int studentId);
        #endregion

        #region StudyPrograms
        [OperationContract]
        IEnumerable<StudyProgram> GetAllStudyPrograms();

        [OperationContract]
        StudyProgram AddStudyProgram(StudyProgram newStudyProgram);

        [OperationContract]
        void EditStudyProgram(StudyProgram editedStudyProgram);

        [OperationContract]
        [FaultContract(typeof(DeleteFault))]
        void DeleteStudyProgram(int studyProgramId);
        #endregion

        #region Departments
        [OperationContract]
        IEnumerable<Department> GetAllDepartments();

        [OperationContract]
        Department AddDepartment(Department newDepartment);

        [OperationContract]
        void EditDepartment(Department editedDepartment);

        [OperationContract]
        [FaultContract(typeof(DeleteFault))]
        void DeleteDepartment(int departmentId);

        [OperationContract]
        IEnumerable<StudyProgram> GetAllSPForDepartmentId(int departmentId);
        #endregion

        #region Professors
        [OperationContract]
        IEnumerable<Professor> GetAllProfessors();

        [OperationContract]
        Professor AddProfessor(Professor newProfessor);

        [OperationContract]
        void EditProfessor(Professor editedProfessor);

        [OperationContract]
        [FaultContract(typeof(DeleteFault))]
        void DeleteProfessor(int professorId);
        #endregion

        #region Courses
        [OperationContract]
        IEnumerable<Course> GetAllCourses();

        [OperationContract]
        Course AddCourse(Course newCourse);

        [OperationContract]
        void EditCourse(Course editedCourse);

        [OperationContract]
        void DeleteCourse(int courseId);
        #endregion

        #region ExamPeriod
        [OperationContract]
        IEnumerable<ExamPeriod> GetAllExamPeriods();

        [OperationContract]
        ExamPeriod AddExamPeriod(ExamPeriod newExamPeriod);

        [OperationContract]
        void EditExamPeriod(ExamPeriod editedExamPeriod);

        [OperationContract]
        [FaultContract(typeof(DeleteFault))]
        void DeleteExamPeriod(int examPeriodId);
        #endregion

        #region Exams
        [OperationContract]
        IEnumerable<Exam> GetAllExams();

        [OperationContract]
        Exam AddExam(Exam newExam);

        [OperationContract]
        void EditExam(Exam editedExam);

        [OperationContract]
        void DeleteExam(int examId);
        #endregion

        #region WPFProfessor

        [OperationContract]
        IEnumerable<ExamPeriod> GetActiveExamPeriods();

        [OperationContract]
        IEnumerable<Course> GetProfessorCourses(int professorId);

        [OperationContract]
        IEnumerable<ExamRegistration> GetRegistredStudentsForExam(int courseId, int examPeriodId);

        [OperationContract]
        bool SetExamResults(List<ExamResult> examResults);

        [OperationContract]
        bool ValidateExamPeriodActivity(DateTime startDate);

        [OperationContract]
        bool ValidateExamInExamPeriod(int examPeriodId, int courseId);

        #endregion
    }
}
