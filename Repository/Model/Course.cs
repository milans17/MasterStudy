using System.Runtime.Serialization;

namespace Repository.Model
{
    [DataContract]
    public class Course
    {
        [DataMember]
        public int CourseId { get; set; }
        [DataMember]
        public int StudyProgramId { get; set; }
        [DataMember]
        public string StudyProgramName { get; set; }
        [DataMember]
        public int ProfessorId { get; set; }
        [DataMember]
        public string ProfessorName { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Assistant { get; set; }
        [DataMember]
        public int? ETCS { get; set; }
    }
}
