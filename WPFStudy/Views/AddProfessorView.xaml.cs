using System.Windows;
using WPFStudy.Common;
using WPFStudy.ServiceReference;
using WPFStudy.ViewModels;

namespace WPFStudy.Views
{
    /// <summary>
    /// Interaction logic for AddProfessorView.xaml
    /// </summary>
    public partial class AddProfessorView : Window
    {
        public AddProfessorView(Professor p = null)
        {
            InitializeComponent();
            DataContext = new AddProfessorViewModel(this, p, ApplicationService.Instance.EventAggregator);
        }
    }
}
