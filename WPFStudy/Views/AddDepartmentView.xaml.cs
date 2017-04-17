using System.Windows;
using WPFStudy.Common;
using WPFStudy.ServiceReference;
using WPFStudy.ViewModels;

namespace WPFStudy.Views
{
    /// <summary>
    /// Interaction logic for AddDepartmentView.xaml
    /// </summary>
    public partial class AddDepartmentView : Window
    {
        public AddDepartmentView(Department d = null)
        {
            InitializeComponent();
            DataContext = new AddDepartmentViewModel(this, d, ApplicationService.Instance.EventAggregator);
        }
    }
}
