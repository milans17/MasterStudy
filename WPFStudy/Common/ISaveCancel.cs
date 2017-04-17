using System.Windows.Input;

namespace WPFStudy.Common
{
    public interface ISaveCancel
    {
        ICommand Save { get; }
        ICommand Cancel { get; }
    }
}
