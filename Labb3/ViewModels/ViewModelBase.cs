using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Labb3.ViewModels;

internal class ViewModelBase : INotifyPropertyChanged //INotifyPropertyChanged ser till att settings man ändrar i sitt user interface uppdaterar "data", t.ex time limit, difficulty osv. 
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

