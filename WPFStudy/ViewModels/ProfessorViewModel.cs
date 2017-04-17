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
    public class ProfessorViewModel : ViewModelBase, ICommanding
    {
        #region Fields

        private ObservableCollection<Professor> professors;
        private ICommand openView;
        private ICommand editView;
        private ICommand removeElement;

        #endregion

        #region Constructor

        public ProfessorViewModel()
        {
            Professors = new ObservableCollection<Professor>(ServiceDataProvider.GetAllProfessors());
            ServiceDataProvider.AddProfessorNotification += ServiceDataProvider_AddProfessorNotification;
        }

        #endregion

        #region Properties

        public ObservableCollection<Professor> Professors
        {
            get { return professors; }
            set
            {
                professors = value;
                OnPropertyChanged("Professors");
            }
        }

        #endregion

        #region Methods

        private void ServiceDataProvider_AddProfessorNotification(object sender, Events.ProfessorEventArgs e)
        {
            Professors.Add(e.Professor);
        }

        #endregion

        #region ICommanding

        public ICommand OpenView
        {
            get
            {
                return openView ?? (openView = new RelayCommand(p => ExecuteOpening(p)));
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

        private void ExecuteOpening(object p)
        {
            AddProfessorView profView = new AddProfessorView();
            profView.ShowDialog();
        }

        private void ExecuteEdit(object p)
        {
            if (p != null && p is Professor)
            {
                var professor = p as Professor;

                AddProfessorView profView = new AddProfessorView(professor);
                profView.ShowDialog();
            }
        }

        private void ExecuteRemove(object p)
        {
            if (p != null && p is Professor)
            {
                try
                {
                    var professor = p as Professor;

                    ServiceDataProvider.DeleteProfessor(professor.ProfessorId);
                    Professors.Remove(professor);
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
