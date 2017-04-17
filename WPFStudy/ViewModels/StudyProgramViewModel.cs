using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;
using WPFStudy.Common;
using WPFStudy.DataProvider;
using WPFStudy.Events;
using WPFStudy.ServiceReference;
using WPFStudy.Views;

namespace WPFStudy.ViewModels
{
    public class StudyProgramViewModel : ViewModelBase, ICommanding
    {
        #region Fields

        private ObservableCollection<StudyProgram> studyPrograms;
        private ICommand openView;
        private ICommand editView;
        private ICommand removeElement;
        private readonly IEventAggregator eventArggr;

        #endregion

        #region Constructor

        public StudyProgramViewModel(IEventAggregator eventArggr)
        {
            this.eventArggr = eventArggr;
            StudyPrograms = new ObservableCollection<StudyProgram>(ServiceDataProvider.GetAllStudyPrograms());
            ServiceDataProvider.AddStudyProgramNotification += ServiceDataProvider_AddStudyProgramNotification;
            SubscribeToParentTableNameChange(eventArggr);
        }

        #endregion

        #region Properties

        public ObservableCollection<StudyProgram> StudyPrograms
        {
            get { return studyPrograms; }
            set
            {
                studyPrograms = value;
                OnPropertyChanged("StudyPrograms");
            }
        }

        #endregion

        #region Methods

        private void SubscribeToParentTableNameChange(IEventAggregator eventArggr)
        {
            eventArggr.GetEvent<DepartmentEvent>().Subscribe(p =>
            {
                var toEdit = StudyPrograms.Where(x => x.DepartmentId == p.DepartmentId);

                foreach (var item in toEdit)
                {
                    if (item.DepartmentName != p.Name)
                    {
                        item.DepartmentName = p.Name;
                    }
                }
            });
        }

        private void ServiceDataProvider_AddStudyProgramNotification(object sender, Events.StudyProgramEventArgs e)
        {
            StudyPrograms.Add(e.StudyProgram);
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
            AddStudyProgramView studyProgramView = new AddStudyProgramView();
            studyProgramView.ShowDialog();
        }

        private void ExecuteEdit(object p)
        {
            if (p != null && p is StudyProgram)
            {
                var studyProgram = p as StudyProgram;

                AddStudyProgramView studyProgramView = new AddStudyProgramView(studyProgram);
                studyProgramView.ShowDialog();
            }
        }

        private void ExecuteRemove(object p)
        {
            if (p != null && p is StudyProgram)
            {
                try
                {
                    var studyProgram = p as StudyProgram;

                    ServiceDataProvider.DeleteStudyProgram(studyProgram.StudyProgramId);
                    StudyPrograms.Remove(studyProgram);
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
