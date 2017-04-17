using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WPFProfessor.ViewModels;
using WPFStudy.ServiceReference;

namespace WPFProfessor.Views
{
    /// <summary>
    /// Interaction logic for ResultsView.xaml
    /// </summary>
    public partial class ResultsView : Window
    {
        private ResultsViewModel viewModel;

        public ResultsView(ObservableCollection<ExamRegistration> students)
        {
            InitializeComponent();
            viewModel = new ResultsViewModel(this, students);
            DataContext = viewModel;
        }

        public delegate void CellEditEndingHandler(object sender, DataGridCellEditEndingEventArgs e);

        public CellEditEndingHandler OnCellEditEnding { get; set; }

        private void dgExamResults_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            OnCellEditEnding(sender, e);
        }
    }
}
