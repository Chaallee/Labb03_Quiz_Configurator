using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3.Views;

namespace Labb3.ViewModels
{
     class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;

        public QuestionPackViewModel? ActivePack { get => _mainWindowViewModel?.ActivePack; }
           
        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this._mainWindowViewModel = mainWindowViewModel;
        }
    }
}
