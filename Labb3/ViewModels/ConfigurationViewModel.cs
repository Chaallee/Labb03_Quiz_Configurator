using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Labb3.Command;

namespace Labb3.ViewModels
{
    class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        public DelegateCommand AddQuestionCommand { get; private set; }
        public DelegateCommand PackOptionsCommand { get; private set; }
        public DelegateCommand RemoveQuestionCommand { get; private set; }

        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            AddQuestionCommand = new DelegateCommand(AddQuestion);

            PackOptionsCommand = new DelegateCommand(PackOptions);

            RemoveQuestionCommand = new DelegateCommand(RemoveQuestion);

        }

        private void RemoveQuestion(object? obj)
        {
            throw new NotImplementedException();
        }

        private void PackOptions(object? obj)
        {
            throw new NotImplementedException();
        }

        private void AddQuestion(object? obj)
        {
            throw new NotImplementedException();
        }

        
        
    }
}
