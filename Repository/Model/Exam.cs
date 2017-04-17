using System.Runtime.Serialization;

namespace Repository.Model
{
    [DataContract]
    public class Exam
    {
        [DataMember]
        public int ExamId { get; set; }
        [DataMember]
        public int ExamPeriodId { get; set; }
        [DataMember]
        public string ExamPeriodName { get; set; }
        [DataMember]
        public int CourseId { get; set; }
        [DataMember]
        public string CourseName { get; set; }
        [DataMember]
        public System.DateTime DateAndTime { get; set; }
        [DataMember]
        public string Place { get; set; }
        [DataMember]
        public bool? IsPassed { get; set; }
        [DataMember]
        public int? Price { get; set; }
    }
}
