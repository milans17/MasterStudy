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
        private int counter = 0;

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

        private void dgExamResults_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                btnConfirmResults.IsEnabled = false;
                counter++;
            }

            if (e.Action == ValidationErrorEventAction.Removed)
            {
                counter--;
                if (counter == 0)
                    btnConfirmResults.IsEnabled = true;
            }
        }
    }
}
