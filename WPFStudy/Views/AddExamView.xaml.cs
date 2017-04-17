using System.Windows;
using WPFStudy.ServiceReference;
using WPFStudy.ViewModels;

namespace WPFStudy.Views
{
    /// <summary>
    /// Interaction logic for AddExamView.xaml
    /// </summary>
    public partial class AddExamView : Window
    {
        public AddExamView(Exam exam = null)
        {
            InitializeComponent();
            DataContext = new AddExamViewModel(this, exam);
        }
    }
}
