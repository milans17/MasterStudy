using System.Windows.Input;

namespace WPFStudy.Common
{
    public interface ICommanding
    {
        ICommand OpenView { get; }
        ICommand EditView { get; }
        ICommand RemoveElement { get; }
    }
}
