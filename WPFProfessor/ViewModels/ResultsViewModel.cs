using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFProfessor.Views;
using WPFStudy.Common;
using WPFStudy.DataProvider;
using WPFStudy.ServiceReference;

namespace WPFProfessor.ViewModels
{
    public class ResultsViewModel
    {
        private ResultsView view;
        private ObservableCollection<ExamRegistration> examRegs;
        private List<ExamResult> resultModels;
        private const string firstTest = "First Test";
        private const string secondTest = "Second Test";
        private const string termPaper = "Term Paper";
        private const string writenExam = "Writen Exam";
        private ICommand confirmResults;
       
        public ResultsViewModel(ResultsView view, ObservableCollection<ExamRegistration> examRegs)
        {
            this.view = view;
            this.examRegs = examRegs;

            resultModels = new List<ExamResult>();

            ProvideItemsSource(view, examRegs);

            view.OnCellEditEnding = dgExamResults_CellEditEnding;
        }

        private void ProvideItemsSource(ResultsView view, ObservableCollection<ExamRegistration> examRegs)
        {
            foreach (var exam in examRegs)
            {
                ExamResult examResult = new ExamResult()
                {
                    ExamRegistrationId = exam.ExamRegistrationId,
                    StudentNameAndSurname = exam.StudentNameAndSurname
                };

                resultModels.Add(examResult);
            }

            view.dgExamResults.ItemsSource = resultModels;
        }

        private void dgExamResults_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var dataRowView = view.dgExamResults.SelectedItem as ExamResult;
            string id = (view.dgExamResults.SelectedCells[0].Column.GetCellContent(dataRowView) as TextBlock).Text;
            var header = e.Column.Header as string;
            var resultModel = resultModels.FirstOrDefault(x => x.ExamRegistrationId == int.Parse(id));
            var result = int.Parse((e.EditingElement as TextBox).Text);

            switch (header)
            {
                case firstTest:
                    resultModel.FirstTest = result;
                    break;

                case secondTest:
                    resultModel.SecondTest = result;
                    break;

                case termPaper:
                    resultModel.TermPaper = result;
                    break;

                case writenExam:
                    resultModel.WritenExam = result;
                    break;
            }
        }

        public ICommand ConfirmResults
        {
            get
            {
                return confirmResults ?? (confirmResults = new RelayCommand(p => ExecuteConfirmResults(p)));
            }
        }

        private void ExecuteConfirmResults(object p)
        {
            try
            {
                if (ServiceDataProvider.SetExamResults(resultModels))
                {
                    MessageBox.Show(string.Format("Exam results are stored in database"));
                }
            }
            catch (FaultException fe)
            {
                MessageBox.Show(string.Format("Error occured during transaction! Details: {0}", fe.Message));
            }
        }
    }
}
