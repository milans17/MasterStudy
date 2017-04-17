using System.Runtime.Serialization;

namespace Repository.Model
{
    [DataContract]
    public class ExamResult
    {
        [DataMember]
        public int ExamResultsId { get; set; }
        [DataMember]
        public int ExamRegistrationId { get; set; }
        [DataMember]
        public string StudentNameAndSurname { get; set; }
        [DataMember]
        public int? TermPaper { get; set; }
        [DataMember]
        public int? FirstTest { get; set; }
        [DataMember]
        public int? SecondTest { get; set; }
        [DataMember]
        public int? WritenExam { get; set; }
        [DataMember]
        public int? Total { get; set; }
        [DataMember]
        public int? Evaluation { get; set; }
    }
}
