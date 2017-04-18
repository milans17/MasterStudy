using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    public class ResultsViewModel : ViewModelBase
    {
        #region Fields

        private ResultsView view;
        private ObservableCollection<ExamRegistration> examRegs;
        private List<ExamResult> resultModels;
        private const string firstTest = "First Test";
        private const string secondTest = "Second Test";
        private const string termPaper = "Term Paper";
        private const string writenExam = "Writen Exam";
        private ICommand confirmResults;

        #endregion

        #region Constructor

        public ResultsViewModel(ResultsView view, ObservableCollection<ExamRegistration> examRegs)
        {
            this.view = view;
            this.examRegs = examRegs;

            resultModels = new List<ExamResult>();
            ProvideItemsSource(view, examRegs);
            view.OnCellEditEnding = dgExamResults_CellEditEnding;
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

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
            try
            {
                if (Validation.GetHasError(e.Row))
                    return;

                var dataRowView = view.dgExamResults.SelectedItem as ExamResult;
                string id = (view.dgExamResults.SelectedCells[0].Column.GetCellContent(dataRowView) as TextBlock).Text;

                if (string.IsNullOrEmpty(id))
                    return;

                var header = e.Column.Header as string;
                var resultModel = resultModels.FirstOrDefault(x => x.ExamRegistrationId == int.Parse(id));

                int examPoints;
                if (!int.TryParse(((e.EditingElement as TextBox).Text), out examPoints))
                {
                    return;
                }

                switch (header)
                {
                    case firstTest:
                        resultModel.FirstTest = examPoints;
                        break;

                    case secondTest:
                        resultModel.SecondTest = examPoints;
                        break;

                    case termPaper:
                        resultModel.TermPaper = examPoints;
                        break;

                    case writenExam:
                        resultModel.WritenExam = examPoints;
                        break;
                }

                resultModel.Total = resultModel.FirstTest + resultModel.SecondTest + resultModel.TermPaper + resultModel.WritenExam;

                if (resultModel.Total > 100)
                {
                    view.btnConfirmResults.IsEnabled = false;
                }
                else
                {
                    view.btnConfirmResults.IsEnabled = true;
                }

                SetEvaluation(resultModel);
            }
            catch (Exception)
            {
                MessageBox.Show("Incorect format! Please try again", "Validation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void SetEvaluation(ExamResult resultModel)
        {
            if (resultModel.Total < 55)
                resultModel.Evaluation = 5;
            else if (resultModel.Total >= 55 && resultModel.Total < 65)
                resultModel.Evaluation = 6;
            else if (resultModel.Total >= 65 && resultModel.Total < 75)
                resultModel.Evaluation = 7;
            else if (resultModel.Total >= 75 && resultModel.Total < 85)
                resultModel.Evaluation = 8;
            else if (resultModel.Total >= 85 && resultModel.Total < 95)
                resultModel.Evaluation = 9;
            else if (resultModel.Total >= 95 && resultModel.Total <= 100)
                resultModel.Evaluation = 10;
        }

        #endregion

        #region Commands

        public ICommand ConfirmResults
        {
            get
            {
                return confirmResults ?? (confirmResults = new RelayCommand(p => ExecuteConfirmResults(p), p => CanExecuteConfirmResults()));
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

        private bool CanExecuteConfirmResults()
        {
            return true;
        }

        #endregion
    }

    public class CellValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                int intValue;
                if (!int.TryParse(value.ToString(), out intValue))
                {
                    return new ValidationResult(false, string.Format("'{0}' is not valid number.", value.ToString()));
                }
            }

            return new ValidationResult(true, null);
        }
    }

}
