using System;
using WPFStudy.ServiceReference;

namespace WPFStudy.Events
{
    public class StudyProgramEventArgs : EventArgs
    {
        private StudyProgram sp;

        public StudyProgramEventArgs(StudyProgram sp)
        {
            this.sp = sp;
        }

        public StudyProgram StudyProgram { get { return sp; } }
    }
}
