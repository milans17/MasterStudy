using System.Windows;
using WPFStudy.Common;
using WPFStudy.ServiceReference;
using WPFStudy.ViewModels;

namespace WPFStudy.Views
{
    /// <summary>
    /// Interaction logic for AddCourseView.xaml
    /// </summary>
    public partial class AddCourseView : Window
    {
        public AddCourseView(Course course = null)
        {
            InitializeComponent();
            DataContext = new AddCourseViewModel(this, course, ApplicationService.Instance.EventAggregator);
        }
    }
}
