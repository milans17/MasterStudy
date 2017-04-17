using System.Windows;
using System.Windows.Input;
using WPFStudy.ViewModels;

namespace WPFStudy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewViewModel();
        }

        private void dgStudents_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //DependencyObject dep = (DependencyObject)e.OriginalSource;
            //while ((dep != null) && !(dep is DataGridCell))
            //{
            //    dep = VisualTreeHelper.GetParent(dep);
            //}
            //if (dep == null) return;

            //if (dep is DataGridCell)
            //{
            //    DataGridCell cell = dep as DataGridCell;
            //    cell.Focus();

            //    while ((dep != null) && !(dep is DataGridRow))
            //    {
            //        dep = VisualTreeHelper.GetParent(dep);
            //    }
            //    DataGridRow row = dep as DataGridRow;
            //    dgStudents.SelectedItem = row.DataContext;
            //}
        }
    }
}
