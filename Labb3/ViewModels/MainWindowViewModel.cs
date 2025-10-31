using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3.Models;

namespace Labb3.ViewModels
{
     class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionPackViewModel> Packs { get; } = new();

		private QuestionPackViewModel _activePack;

		public QuestionPackViewModel ActivePack
		{
			get => _activePack;
			set {
				_activePack = value;
				RaisePropertyChanged();
                PlayerViewModel.RaisePropertyChanged(nameof(PlayerViewModel.ActivePack));
			}
		}

        public PlayerViewModel? PlayerViewModel { get; set; }
        public ConfigurationViewModel? ConfigurationViewModel { get; set; }
        public MainWindowViewModel()
        {
            PlayerViewModel =  new PlayerViewModel(this);
            ConfigurationViewModel = new ConfigurationViewModel(this);
			
            var pack = new QuestionPack("MyQuestionPack");
            ActivePack = new QuestionPackViewModel(pack);
            ActivePack.Questions.Add(new Question($"Exempel på fråga här","1", "2", "3", "4"));
            ActivePack.Questions.Add(new Question($"Exempel på fråga2 här", "svar1", "svar2", "svar3", "svar4"));
       



        }

    }
}



