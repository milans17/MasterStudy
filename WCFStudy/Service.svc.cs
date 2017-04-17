using System.Linq;
using System.Collections.Generic;
using Repository;
using Repository.Model;
using System;
using System.ServiceModel;

namespace WCFStudy
{
    public class Service : IService
    {
        #region Students
        public IEnumerable<Student> GetAllStudents()
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbStudents = dbContext.StudentView.ToList();
                List<Student> students = new List<Student>(dbStudents.Count);

                foreach (var dbStudent in dbStudents)
                {
                    Student student = new Student();
                    student.StudentId = dbStudent.StudentId;
                    student.DepartmentId = dbStudent.DepartmentId;
                    student.StudyProgramId = dbStudent.StudyProgramId;
                    student.NameAndSurname = dbStudent.NameAndSurname;
                    student.Address = dbStudent.Address;
                    student.Balance = dbStudent.Balance;
                    student.BirthDate = dbStudent.BirthDate;
                    student.BirthPlace = dbStudent.BirthPlace;
                    student.StudyProgramName = dbStudent.StudyProgramName;
                    student.DepartmentName = dbStudent.DepartmantName;
                    student.Username = dbStudent.Username;
                    student.Password = dbStudent.Password;
                    student.Phone = dbStudent.Phone;
                    student.StudyYear = dbStudent.StudyYear;

                    students.Add(student);
                }

                return students;
            }
        }

        public Student AddStudent(Student newStudent)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var addStudnet = new tblStudents()
                {
                    NameAndSurname = newStudent.NameAndSurname,
                    StudyProgramId = newStudent.StudyProgramId,
                    Address = newStudent.Address,
                    BirthDate = newStudent.BirthDate,
                    Balance = newStudent.Balance,
                    BirthPlace = newStudent.BirthPlace,
                    Phone = newStudent.Phone,
                    StudyYear = newStudent.StudyYear,
                    Username = newStudent.Username,
                    Password = newStudent.Password
                };

                dbContext.tblStudents.Add(addStudnet);
                dbContext.SaveChanges();

                var newStudentId = dbContext.StudentView.SingleOrDefault(x => x.StudentId == addStudnet.StudentId);

                return new Student()
                {
                    StudentId = newStudentId.StudentId,
                    NameAndSurname = newStudentId.NameAndSurname,
                    StudyProgramId = newStudentId.StudyProgramId,
                    StudyProgramName = newStudentId.StudyProgramName,
                    DepartmentId = newStudentId.DepartmentId,
                    DepartmentName = newStudentId.DepartmantName,
                    Address = newStudentId.Address,
                    Balance = newStudentId.Balance,
                    BirthDate = newStudentId.BirthDate,
                    BirthPlace = newStudentId.BirthPlace,
                    StudyYear = newStudentId.StudyYear,
                    Phone = newStudentId.Phone,
                    Username = newStudentId.Username,
                    Password = newStudentId.Password
                };
            }
        }

        public void EditStudent(Student editStudent)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbStudent = dbContext.tblStudents.SingleOrDefault(x => x.StudentId == editStudent.StudentId);

                dbStudent.NameAndSurname = editStudent.NameAndSurname;
                dbStudent.StudyProgramId = editStudent.StudyProgramId;
                dbStudent.Address = editStudent.Address;
                dbStudent.Balance = editStudent.Balance;
                dbStudent.BirthDate = editStudent.BirthDate;
                dbStudent.BirthPlace = editStudent.BirthPlace;
                dbStudent.Phone = editStudent.Phone;
                dbStudent.StudyYear = editStudent.StudyYear;
                dbStudent.Username = editStudent.Username;
                dbStudent.Password = editStudent.Password;

                dbContext.SaveChanges();
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var findStudent = dbContext.tblStudents.Find(studentId);
                if (findStudent != null)
                {
                    dbContext.tblStudents.Remove(findStudent);
                    dbContext.SaveChanges();
                }
            }
        }
        #endregion

        #region StudyPrograms
        public IEnumerable<StudyProgram> GetAllStudyPrograms()
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbStudyPrograms = dbContext.StudyProgramView.ToList();
                List<StudyProgram> studyPrograms = new List<StudyProgram>(dbStudyPrograms.Count);

                foreach (var dbStudyProgram in dbStudyPrograms)
                {
                    StudyProgram studyProgram = new StudyProgram();
                    studyProgram.StudyProgramId = dbStudyProgram.StudyProgramId;
                    studyProgram.Name = dbStudyProgram.Name;
                    studyProgram.DepartmentId = dbStudyProgram.DepartmentId;
                    studyProgram.DepartmentName = dbStudyProgram.DepartmentName;
                    studyProgram.SelffinancedPlaces = dbStudyProgram.SelffinancedPlaces;
                    studyProgram.BudgetPlaces = dbStudyProgram.BudgetPlaces;
                    studyProgram.Tuition = dbStudyProgram.Tuition;

                    studyPrograms.Add(studyProgram);
                }

                return studyPrograms;
            }
        }

        public StudyProgram AddStudyProgram(StudyProgram newStudyProgram)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var addStudyProgram = new tblStudyPrograms()
                {
                    Name = newStudyProgram.Name,
                    DepartmentId = newStudyProgram.DepartmentId,
                    BudgetPlaces = newStudyProgram.BudgetPlaces,
                    SelffinancedPlaces = newStudyProgram.SelffinancedPlaces,
                    Tuition = newStudyProgram.Tuition
                };

                dbContext.tblStudyPrograms.Add(addStudyProgram);
                dbContext.SaveChanges();

                var newSPId = dbContext.StudyProgramView.SingleOrDefault(x => x.StudyProgramId == addStudyProgram.StudyProgramId);

                return new StudyProgram()
                {
                    StudyProgramId = newSPId.StudyProgramId,
                    Name = newSPId.Name,
                    DepartmentId = newSPId.DepartmentId,
                    DepartmentName = newSPId.DepartmentName,
                    BudgetPlaces = newSPId.BudgetPlaces,
                    SelffinancedPlaces = newSPId.SelffinancedPlaces,
                    Tuition = newSPId.Tuition
                };
            }
        }

        public void EditStudyProgram(StudyProgram editedStudyProgram)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbStudyProgram = dbContext.tblStudyPrograms.SingleOrDefault(x => x.StudyProgramId == editedStudyProgram.StudyProgramId);

                dbStudyProgram.Name = editedStudyProgram.Name;
                dbStudyProgram.DepartmentId = editedStudyProgram.DepartmentId;
                dbStudyProgram.BudgetPlaces = editedStudyProgram.BudgetPlaces;
                dbStudyProgram.SelffinancedPlaces = editedStudyProgram.SelffinancedPlaces;
                dbStudyProgram.Tuition = editedStudyProgram.Tuition;

                dbContext.SaveChanges();
            }
        }

        public void DeleteStudyProgram(int studyProgramId)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var findStudyProgram = dbContext.tblStudyPrograms.Find(studyProgramId);

                if (dbContext.tblStudents.Any(x => x.StudyProgramId == studyProgramId))
                {
                    DeleteFault fault = new DeleteFault()
                    {
                        Result = false,
                        Message = "Unable to delete!",
                        Description = "Study Program is referenced in Student."
                    };

                    throw new FaultException<DeleteFault>(fault);
                }

                if (findStudyProgram != null)
                {
                    dbContext.tblStudyPrograms.Remove(findStudyProgram);
                    dbContext.SaveChanges();
                }
            }
        }
        #endregion

        #region Departments
        public IEnumerable<Department> GetAllDepartments()
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbDepartments = dbContext.tblDepartments.ToList();
                List<Department> departments = new List<Department>(dbDepartments.Count);

                foreach (var dbDepartment in dbDepartments)
                {
                    Department department = new Department()
                    {
                        DepartmentId = dbDepartment.DepartmentId,
                        Name = dbDepartment.Name,
                        FoundationYear = dbDepartment.FoundationYear,
                        DepartmentHead = dbDepartment.DepartmentHead,
                        Website = dbDepartment.Website
                    };

                    departments.Add(department);
                }

                return departments;
            }
        }

        public Department AddDepartment(Department newDepartment)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var addDepartment = new tblDepartments()
                {
                    Name = newDepartment.Name,
                    FoundationYear = newDepartment.FoundationYear,
                    DepartmentHead = newDepartment.DepartmentHead,
                    Website = newDepartment.Website
                };

                dbContext.tblDepartments.Add(addDepartment);
                dbContext.SaveChanges();

                var newDepartmentId = dbContext.tblDepartments.SingleOrDefault(x => x.DepartmentId == addDepartment.DepartmentId);

                return new Department()
                {
                    DepartmentId = newDepartmentId.DepartmentId,
                    Name = newDepartmentId.Name,
                    FoundationYear = newDepartmentId.FoundationYear,
                    DepartmentHead = newDepartmentId.DepartmentHead,
                    Website = newDepartmentId.Website
                };
            }
        }

        public void EditDepartment(Department editedDepartment)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbDepartment = dbContext.tblDepartments.Find(editedDepartment.DepartmentId);

                dbDepartment.Name = editedDepartment.Name;
                dbDepartment.DepartmentHead = editedDepartment.DepartmentHead;
                dbDepartment.FoundationYear = editedDepartment.FoundationYear;
                dbDepartment.Website = editedDepartment.Website;

                dbContext.SaveChanges();
            }
        }

        public void DeleteDepartment(int departmentId)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var findDepartment = dbContext.tblDepartments.Find(departmentId);

                if (dbContext.tblStudyPrograms.Any(x => x.DepartmentId == departmentId))
                {
                    DeleteFault fault = new DeleteFault()
                    {
                        Result = false,
                        Message = "Unable to delete!",
                        Description = "Department is referenced in Study Program."
                    };

                    throw new FaultException<DeleteFault>(fault);
                }

                if (findDepartment != null)
                {
                    dbContext.tblDepartments.Remove(findDepartment);
                    dbContext.SaveChanges();
                }
            }
        }

        public IEnumerable<StudyProgram> GetAllSPForDepartmentId(int departmentId)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var spList = (from sp in dbContext.tblStudyPrograms where sp.DepartmentId == departmentId select sp).ToList();

                ICollection<StudyProgram> studyPrograms = new List<StudyProgram>(spList.Count);

                foreach (var sp in spList)
                {
                    StudyProgram departmentSP = new StudyProgram()
                    {
                        StudyProgramId = sp.StudyProgramId,
                        DepartmentId = sp.DepartmentId,
                        Name = sp.Name
                    };

                    studyPrograms.Add(departmentSP);
                }

                return studyPrograms;
            }
        }
        #endregion

        #region Professors
        public IEnumerable<Professor> GetAllProfessors()
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbProfessors = dbContext.tblProfessors.ToList();
                List<Professor> professors = new List<Professor>(dbProfessors.Count);

                foreach (var dbProfessor in dbProfessors)
                {
                    Professor prof = new Professor()
                    {
                        ProfessorId = dbProfessor.ProfessorId,
                        NameAndSurname = dbProfessor.NameAndSurname,
                        Address = dbProfessor.Address,
                        BirthDate = dbProfessor.BirthDate,
                        BirthPlace = dbProfessor.BirthPlace,
                        Education = dbProfessor.Education,
                        Phone = dbProfessor.Phone,
                        IsAdmin = dbProfessor.IsAdmin,
                        Username = dbProfessor.Username,
                        Password = dbProfessor.Password
                    };

                    professors.Add(prof);
                }

                return professors;
            }
        }

        public Professor AddProfessor(Professor newProfessor)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var addProfessor = new tblProfessors()
                {
                    NameAndSurname = newProfessor.NameAndSurname,
                    Address = newProfessor.Address,
                    BirthDate = newProfessor.BirthDate,
                    BirthPlace = newProfessor.BirthPlace,
                    Education = newProfessor.Education,
                    IsAdmin = newProfessor.IsAdmin,
                    Phone = newProfessor.Phone,
                    Username = newProfessor.Username,
                    Password = newProfessor.Password
                };

                dbContext.tblProfessors.Add(addProfessor);
                dbContext.SaveChanges();

                var newProfId = dbContext.tblProfessors.SingleOrDefault(x => x.ProfessorId == addProfessor.ProfessorId);

                return new Professor()
                {
                    ProfessorId = newProfId.ProfessorId,
                    NameAndSurname = newProfId.NameAndSurname,
                    Address = newProfId.Address,
                    BirthDate = newProfId.BirthDate,
                    BirthPlace = newProfId.BirthPlace,
                    Education = newProfId.Education,
                    IsAdmin = newProfId.IsAdmin,
                    Phone = newProfId.Phone,
                    Username = newProfId.Username,
                    Password = newProfId.Password
                };
            }
        }

        public void EditProfessor(Professor editedProfessor)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbProfessor = dbContext.tblProfessors.Find(editedProfessor.ProfessorId);

                dbProfessor.NameAndSurname = editedProfessor.NameAndSurname;
                dbProfessor.Address = editedProfessor.Address;
                dbProfessor.BirthDate = editedProfessor.BirthDate;
                dbProfessor.BirthPlace = editedProfessor.BirthPlace;
                dbProfessor.Education = editedProfessor.Education;
                dbProfessor.IsAdmin = editedProfessor.IsAdmin;
                dbProfessor.Phone = editedProfessor.Phone;
                dbProfessor.Username = editedProfessor.Username;
                dbProfessor.Password = editedProfessor.Password;

                dbContext.SaveChanges();
            }
        }

        public void DeleteProfessor(int professorId)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var findProfessor = dbContext.tblProfessors.Find(professorId);

                if (dbContext.tblCourses.Any(x => x.ProfessorId == professorId))
                {
                    DeleteFault fault = new DeleteFault()
                    {
                        Result = false,
                        Message = "Unable to delete!",
                        Description = "Professor is referenced in Course."
                    };

                    throw new FaultException<DeleteFault>(fault);
                }

                if (findProfessor != null)
                {
                    dbContext.tblProfessors.Remove(findProfessor);
                    dbContext.SaveChanges();
                }
            }
        }
        #endregion

        #region Courses
        public IEnumerable<Course> GetAllCourses()
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbCourses = dbContext.CourseView.ToList();
                List<Course> courses = new List<Course>(dbCourses.Count);

                foreach (var dbCourse in dbCourses)
                {
                    Course course = new Course()
                    {
                        CourseId = dbCourse.CourseId,
                        Name = dbCourse.Name,
                        StudyProgramId = dbCourse.StudyProgramId,
                        StudyProgramName = dbCourse.StudyProgramName,
                        ProfessorId = dbCourse.ProfessorId,
                        ProfessorName = dbCourse.ProfessorName,
                        Assistant = dbCourse.Assistant,
                        ETCS = dbCourse.ETCS
                    };

                    courses.Add(course);
                }

                return courses;
            }
        }

        public Course AddCourse(Course newCourse)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var addCourse = new tblCourses()
                {
                    Name = newCourse.Name,
                    StudyProgramId = newCourse.StudyProgramId,
                    ProfessorId = newCourse.ProfessorId,
                    Assistant = newCourse.Assistant,
                    ETCS = newCourse.ETCS
                };

                dbContext.tblCourses.Add(addCourse);
                dbContext.SaveChanges();

                var newCourseId = dbContext.CourseView.SingleOrDefault(x => x.CourseId == addCourse.CourseId);

                return new Course()
                {
                    CourseId = newCourseId.CourseId,
                    Name = newCourseId.Name,
                    StudyProgramId = newCourseId.StudyProgramId,
                    StudyProgramName = newCourseId.StudyProgramName,
                    ProfessorId = newCourseId.ProfessorId,
                    ProfessorName = newCourseId.ProfessorName,
                    Assistant = newCourseId.Assistant,
                    ETCS = newCourseId.ETCS
                };
            }
        }

        public void EditCourse(Course editedCourse)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbCourse = dbContext.tblCourses.SingleOrDefault(x => x.CourseId == editedCourse.CourseId);

                dbCourse.Name = editedCourse.Name;
                dbCourse.StudyProgramId = editedCourse.StudyProgramId;
                dbCourse.ProfessorId = editedCourse.ProfessorId;
                dbCourse.Assistant = editedCourse.Assistant;
                dbCourse.ETCS = editedCourse.ETCS;

                dbContext.SaveChanges();
            }
        }

        public void DeleteCourse(int courseId)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var findCourse = dbContext.tblCourses.Find(courseId);
                if (findCourse != null)
                {
                    dbContext.tblCourses.Remove(findCourse);
                    dbContext.SaveChanges();
                }
            }
        }
        #endregion

        #region ExamPeriods
        public IEnumerable<ExamPeriod> GetAllExamPeriods()
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbExamPeriods = dbContext.tblExamPeriods.ToList();
                List<ExamPeriod> examPeriods = new List<ExamPeriod>(dbExamPeriods.Count);

                foreach (var dbExamPeriod in dbExamPeriods)
                {
                    ExamPeriod examPeriod = new ExamPeriod()
                    {
                        ExamPeriodId = dbExamPeriod.ExamPeriodId,
                        Name = dbExamPeriod.Name,
                        StartDate = dbExamPeriod.StartDate,
                        EndDate = dbExamPeriod.EndDate,
                        SchoolYear = dbExamPeriod.SchoolYear,
                        IsActive = dbExamPeriod.IsActive,
                        IsApsolvent = dbExamPeriod.IsApsolvent
                    };

                    examPeriods.Add(examPeriod);
                }

                return examPeriods;
            }
        }

        public ExamPeriod AddExamPeriod(ExamPeriod newExamPeriod)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var addExamPeriod = new tblExamPeriods()
                {
                    Name = newExamPeriod.Name,
                    StartDate = newExamPeriod.StartDate,
                    EndDate = newExamPeriod.EndDate,
                    SchoolYear = newExamPeriod.SchoolYear,
                    IsActive = newExamPeriod.IsActive,
                    IsApsolvent = newExamPeriod.IsApsolvent
                };

                dbContext.tblExamPeriods.Add(addExamPeriod);
                dbContext.SaveChanges();

                var newExamPeriodId = dbContext.tblExamPeriods.SingleOrDefault(x => x.ExamPeriodId == addExamPeriod.ExamPeriodId);

                return new ExamPeriod()
                {
                    ExamPeriodId = newExamPeriodId.ExamPeriodId,
                    Name = newExamPeriodId.Name,
                    StartDate = newExamPeriodId.StartDate,
                    EndDate = newExamPeriodId.EndDate,
                    SchoolYear = newExamPeriodId.SchoolYear,
                    IsActive = newExamPeriodId.IsActive,
                    IsApsolvent = newExamPeriodId.IsApsolvent
                };
            }
        }

        public void EditExamPeriod(ExamPeriod editedExamPeriod)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbExamPeriod = dbContext.tblExamPeriods.Find(editedExamPeriod.ExamPeriodId);

                dbExamPeriod.Name = editedExamPeriod.Name;
                dbExamPeriod.StartDate = editedExamPeriod.StartDate;
                dbExamPeriod.EndDate = editedExamPeriod.EndDate;
                dbExamPeriod.SchoolYear = editedExamPeriod.SchoolYear;
                dbExamPeriod.IsActive = editedExamPeriod.IsActive;
                dbExamPeriod.IsApsolvent = editedExamPeriod.IsApsolvent;

                dbContext.SaveChanges();
            }
        }

        public void DeleteExamPeriod(int examPeriodId)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var findExamPeriod = dbContext.tblExamPeriods.Find(examPeriodId);

                if (dbContext.tblExams.Any(x => x.ExamPeriodId == examPeriodId))
                {
                    DeleteFault fault = new DeleteFault()
                    {
                        Result = false,
                        Message = "Unable to delete!",
                        Description = "Exam Period is referenced in Exam."
                    };

                    throw new FaultException<DeleteFault>(fault);
                }

                if (findExamPeriod != null)
                {
                    dbContext.tblExamPeriods.Remove(findExamPeriod);
                    dbContext.SaveChanges();
                }
            }
        }
        #endregion

        #region Exams
        public IEnumerable<Exam> GetAllExams()
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbExams = dbContext.ExamView.ToList();
                List<Exam> exams = new List<Exam>(dbExams.Count);

                foreach (var dbExam in dbExams)
                {
                    Exam exam = new Exam()
                    {
                        ExamId = dbExam.ExamId,
                        ExamPeriodId = dbExam.ExamPeriodId,
                        ExamPeriodName = dbExam.ExamPeriodName,
                        CourseId = dbExam.CourseId,
                        CourseName = dbExam.CourseName,
                        DateAndTime = dbExam.DateAndTime,
                        Place = dbExam.Place,
                        Price = dbExam.Price,
                        IsPassed = dbExam.IsPassed
                    };

                    exams.Add(exam);
                }

                return exams;
            }
        }

        public Exam AddExam(Exam newExam)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var addExam = new tblExams()
                {
                    ExamPeriodId = newExam.ExamPeriodId,
                    CourseId = newExam.CourseId,
                    DateAndTime = newExam.DateAndTime,
                    Place = newExam.Place,
                    Price = newExam.Price,
                    IsPassed = newExam.IsPassed
                };

                dbContext.tblExams.Add(addExam);
                dbContext.SaveChanges();

                var newExamId = dbContext.ExamView.SingleOrDefault(x => x.ExamId == addExam.ExamId);

                return new Exam()
                {
                    ExamId = newExamId.ExamId,
                    ExamPeriodId = newExamId.ExamPeriodId,
                    ExamPeriodName = newExamId.ExamPeriodName,
                    CourseId = newExamId.CourseId,
                    CourseName = newExamId.CourseName,
                    DateAndTime = newExamId.DateAndTime,
                    Place = newExamId.Place,
                    Price = newExamId.Price,
                    IsPassed = newExamId.IsPassed
                };
            }
        }

        public void EditExam(Exam editedExam)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var dbExam = dbContext.tblExams.Find(editedExam.ExamId);

                dbExam.ExamPeriodId = editedExam.ExamPeriodId;
                dbExam.CourseId = editedExam.CourseId;
                dbExam.DateAndTime = editedExam.DateAndTime;
                dbExam.Place = editedExam.Place;
                dbExam.Price = editedExam.Price;
                dbExam.IsPassed = editedExam.IsPassed;

                dbContext.SaveChanges();
            }
        }

        public void DeleteExam(int examId)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var findExam = dbContext.tblExams.Find(examId);
                if (findExam != null)
                {
                    dbContext.tblExams.Remove(findExam);
                    dbContext.SaveChanges();
                }
            }
        }
        #endregion

        #region WPFProfessor

        public IEnumerable<ExamPeriod> GetActiveExamPeriods()
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                List<ExamPeriod> examPeriods = new List<ExamPeriod>();

                var activeExamPeriods = dbContext.tblExamPeriods.Where(x => x.IsActive == true);
                var date = DateTime.Now.ToLocalTime();

                foreach (var activeExamPeriod in activeExamPeriods)
                {
                    ExamPeriod ep = new ExamPeriod()
                    {
                        ExamPeriodId = activeExamPeriod.ExamPeriodId,
                        Name = activeExamPeriod.Name,
                        StartDate = activeExamPeriod.StartDate,
                        EndDate = activeExamPeriod.EndDate
                    };

                    examPeriods.Add(ep);
                }

                return examPeriods;
            }
        }

        public IEnumerable<Course> GetProfessorCourses(int professorId)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var profCourses = dbContext.tblCourses.Where(x => x.ProfessorId == professorId);
                List<Course> courses = new List<Course>();

                foreach (var item in profCourses)
                {
                    Course c = new Course()
                    {
                        CourseId = item.CourseId,
                        Name = item.Name
                    };

                    courses.Add(c);
                }

                return courses;
            }
        }

        public IEnumerable<ExamRegistration> GetRegistredStudentsForExam(int courseId, int examPeriodId)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                List<ExamRegistration> examRegistrations = new List<ExamRegistration>();

                var exam = dbContext.tblExams.SingleOrDefault(x => x.ExamPeriodId == examPeriodId && x.CourseId == courseId);

                if (exam == null)
                    return new List<ExamRegistration>();

                var dbStudents = (from s in dbContext.tblStudents select s).ToList();
                var examQuery = (from examReg in dbContext.tblExamRegistrations
                                 where examReg.ExamId == exam.ExamId && examReg.IsRegistred == true
                                 select examReg).ToList();

                for (int i = 0; i < examQuery.Count(); i++)
                {
                    for (int j = 0; j < dbStudents.Count(); j++)
                    {
                        if (examQuery[i].StudentId == dbStudents[j].StudentId)
                        {
                            ExamRegistration er = new ExamRegistration()
                            {
                                ExamRegistrationId = examQuery[i].ExamRegistrationId,
                                StudentNameAndSurname = dbStudents[j].NameAndSurname
                            };

                            examRegistrations.Add(er);
                        }
                    }
                }

                return examRegistrations;
            }
        }

        public bool SetExamResults(List<ExamResult> examResults)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                try
                {
                    foreach (var examResult in examResults)
                    {
                        var examResultForInsert = new tblExamResults()
                        {
                            ExamRegistrationId = examResult.ExamRegistrationId,
                            FirstTest = examResult.FirstTest,
                            SecondTest = examResult.SecondTest,
                            TermPaper = examResult.TermPaper,
                            WritenExam = examResult.WritenExam,
                            Total = examResult.Total,
                            Evaluation = examResult.Evaluation
                        };

                        dbContext.tblExamResults.Add(examResultForInsert);
                    }

                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    e.StackTrace.ToString();
                    return false;
                }
            }
        }

        public bool ValidateExamPeriodActivity(DateTime startDate)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var activeEPs = (from ep in dbContext.tblExamPeriods where ep.IsActive == true select ep);

                foreach (var activeEP in activeEPs)
                {
                    if (startDate > activeEP.StartDate && startDate < activeEP.EndDate)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public bool ValidateExamInExamPeriod(int examPeriodId, int courseId)
        {
            using (StudentDbEntities dbContext = new StudentDbEntities())
            {
                var activeExams = (from exam in dbContext.tblExams
                                   where exam.ExamPeriodId == examPeriodId && exam.CourseId == courseId
                                   select exam).Count();

                if (activeExams == 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion
    }
}
