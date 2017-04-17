using System.Windows;
using System.Windows.Controls;
using WPFProfessor.ViewModels;

namespace WPFProfessor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e);
        public delegate void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e);

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewViewModel(this);
        }

        public ComboBoxSelectionChanged OnSelectionComboBoxChanged { get; set; }
        public TabControlSelectionChanged OnSelectionTabControlChanged { get; set; }

        private void CbxCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectionComboBoxChanged?.Invoke(sender, e);
        }

        private void Tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectionTabControlChanged?.Invoke(sender, e);
        }
    }
}
