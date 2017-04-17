using System.Windows;
using WPFStudy.Common;
using WPFStudy.ServiceReference;
using WPFStudy.ViewModels;

namespace WPFStudy.Views
{
    /// <summary>
    /// Interaction logic for AddExamPeriodView.xaml
    /// </summary>
    public partial class AddExamPeriodView : Window
    {
        public AddExamPeriodView(ExamPeriod ep = null)
        {
            InitializeComponent();
            DataContext = new AddExamPeriodViewModel(this, ep, ApplicationService.Instance.EventAggregator);
        }
    }
}
