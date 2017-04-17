using System.Windows;
using WPFStudy.Common;
using WPFStudy.ServiceReference;
using WPFStudy.ViewModels;

namespace WPFStudy
{
    /// <summary>
    /// Interaction logic for AddStudentView.xaml
    /// </summary>
    public partial class AddStudentView : Window
    {
        public AddStudentView(Student editStudent = null)
        {
            InitializeComponent();
            DataContext = new AddStudentViewModel(this, editStudent, ApplicationService.Instance.EventAggregator);
        }
    }
}
