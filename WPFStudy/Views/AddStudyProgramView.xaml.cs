using System.Windows;
using WPFStudy.Common;
using WPFStudy.ServiceReference;
using WPFStudy.ViewModels;

namespace WPFStudy.Views
{
    /// <summary>
    /// Interaction logic for AddStudyProgramView.xaml
    /// </summary>
    public partial class AddStudyProgramView : Window
    {
        public AddStudyProgramView(StudyProgram sp = null)
        {
            InitializeComponent();
            DataContext = new AddStudyProgramViewModel(this, sp, ApplicationService.Instance.EventAggregator);
        }
    }
}
