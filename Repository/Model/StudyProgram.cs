using System.Runtime.Serialization;

namespace Repository.Model
{
    [DataContract]
    public class StudyProgram
    {
        [DataMember]
        public int StudyProgramId { get; set; }
        [DataMember]
        public int DepartmentId { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int? BudgetPlaces { get; set; }
        [DataMember]
        public int? SelffinancedPlaces { get; set; }
        [DataMember]
        public int? Tuition { get; set; }
    }
}
