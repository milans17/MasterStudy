using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;
using WPFStudy.Common;
using WPFStudy.DataProvider;
using WPFStudy.ServiceReference;
using WPFStudy.Views;

namespace WPFStudy.ViewModels
{
    public class ExamPeriodViewModel : ViewModelBase, ICommanding
    {
        #region Fields

        private ICommand openView;
        private ICommand editView;
        private ICommand removeElement;

        #endregion

        #region Constructor

        public ExamPeriodViewModel()
        {
            ExamPeriods = new ObservableCollection<ExamPeriod>(ServiceDataProvider.GetAllExamPeriods());
            ServiceDataProvider.AddExamPeriodNotification += ServiceDataProvider_AddExamPeriodNotification;
        }

        #endregion

        #region Properties

        public ObservableCollection<ExamPeriod> ExamPeriods { get; set; }

        #endregion

        #region Methods

        private void ServiceDataProvider_AddExamPeriodNotification(object sender, Events.ExamPeriodEventArgs e)
        {
            ExamPeriods.Add(e.ExamPeriod);
        }

        #endregion Methods

        #region ICommanding

        public ICommand OpenView
        {
            get
            {
                return openView ?? (openView = new RelayCommand(p => ExecuteOpen()));
            }
        }

        public ICommand EditView
        {
            get
            {
                return editView ?? (editView = new RelayCommand(p => ExecuteEdit(p)));
            }
        }

        public ICommand RemoveElement
        {
            get
            {
                return removeElement ?? (removeElement = new RelayCommand(p => ExecuteRemove(p)));
            }
        }

        private void ExecuteOpen()
        {
            AddExamPeriodView view = new AddExamPeriodView();
            view.ShowDialog();
        }

        private void ExecuteEdit(object p)
        {
            if (p != null && p is ExamPeriod)
            {
                var examPeriod = p as ExamPeriod;

                AddExamPeriodView view = new AddExamPeriodView(examPeriod);
                view.ShowDialog();
            }
        }

        private void ExecuteRemove(object p)
        {
            if (p != null && p is ExamPeriod)
            {
                try
                {
                    var examPeriod = p as ExamPeriod;

                    ServiceDataProvider.DeleteExamPeriod(examPeriod.ExamPeriodId);
                    ExamPeriods.Remove(examPeriod);
                }
                catch (FaultException<DeleteFault> fe)
                {
                    MessageBox.Show(string.Format("{0} {1}", fe.Detail.Message, fe.Detail.Description));
                }
            }
        }

        #endregion
    }
}
