﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using WPFProfessor.Views;
using WPFStudy.Common;
using WPFStudy.DataProvider;
using WPFStudy.ServiceReference;

namespace WPFProfessor.ViewModels
{
    public class MainViewViewModel : ViewModelBase
    {
        #region Fields

        private MainWindow view;
        private int courseId;
        private ObservableCollection<ExamRegistration> students;
        private ObservableCollection<TabItem> examPeriodTabs;
        private ICommand setResults;

        #endregion

        #region Constructor

        public MainViewViewModel(MainWindow view)
        {
            this.view = view;
           
            examPeriodTabs = new ObservableCollection<TabItem>();

            foreach (var activeExamPeriod in ServiceDataProvider.GetActiveExamPeriods())
            {
                examPeriodTabs.Add(new TabItem() { Header = activeExamPeriod.Name, Tag = activeExamPeriod.ExamPeriodId });
            }

            view.Tabcontrol.SelectedIndex = 0;
            Courses = ServiceDataProvider.GetProfessorCourses(4);
            students = new ObservableCollection<ExamRegistration>();
            view.OnSelectionComboBoxChanged = CbxCourses_SelectionChanged;
            view.OnSelectionTabControlChanged = Tabcontrol_SelectionChanged;
        }

        #endregion

        #region Properties

        public TabItem ExamPeriodTab { get; set; }
        
        public ObservableCollection<TabItem> ExamPeriodTabs
        {
            get { return examPeriodTabs; }
            set
            {
                examPeriodTabs = value;
                OnPropertyChanged("ExamPeriodTabs");
            }
        }

        public IEnumerable<Course> Courses { get; set; }

        public int CourseId
        {
            get { return courseId; }
            set
            {
                courseId = value;
                OnPropertyChanged("CourseId");
            }
        }

        public ObservableCollection<ExamRegistration> Students
        {
            get { return students; }
            set
            {
                students = value;
                OnPropertyChanged("Students");
            }
        }

        #endregion

        #region Methods

        private int GetExamPeriodIdFromTag()
        {
            if (view != null && view.Tabcontrol.SelectedItem != null)
            {
                return (int)(view.Tabcontrol.SelectedItem as TabItem).Tag;
            }
            else
            {
                return 0;
            }
        }

        private void CbxCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Students = new ObservableCollection<ExamRegistration>(ServiceDataProvider.GetRegistredStudentsForExam(CourseId, GetExamPeriodIdFromTag()));
        }

        private void Tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CourseId == 0)
                return;

            Students = new ObservableCollection<ExamRegistration>(ServiceDataProvider.GetRegistredStudentsForExam(CourseId, GetExamPeriodIdFromTag()));
        }

        #endregion

        #region Commands

        public ICommand SetResults
        {
            get
            {
                return setResults ?? (setResults = new RelayCommand(p => ExecuteSetResults(p), p => CanExecuteResults()));
            }
        }

        private void ExecuteSetResults(object p)
        {
            ResultsView resultsView = new ResultsView(Students);
            resultsView.ShowDialog();
        }

        private bool CanExecuteResults()
        {
            return Students.Count > 0;
        }

        #endregion
    }
}
