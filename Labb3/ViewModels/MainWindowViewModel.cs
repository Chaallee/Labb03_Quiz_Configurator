using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Labb3.Command;
using Labb3.Models;
using Labb3.Views;

namespace Labb3.ViewModels
{
     class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionPackViewModel> Packs { get; } = new();

		private QuestionPackViewModel _activePack;

        private Visibility _visibilityConfigurationView;
        private Visibility _visiblePlayerView;


        public QuestionPackViewModel ActivePack
		{
			get => _activePack;
			set {
				_activePack = value;
				RaisePropertyChanged();
                PlayerViewModel?.RaisePropertyChanged(nameof(PlayerViewModel.ActivePack));
			}
		}

        public DelegateCommand ShowPlayerViewCommand { get; }
        public DelegateCommand ShowConfigurationViewCommand { get; }

        public Visibility VisiblePlayerView
        {
            get => _visiblePlayerView;
            set
            {
                _visiblePlayerView = value;
                RaisePropertyChanged();
            }
        }
          
        public Visibility VisibilityConfigurationView 
        {
            get => _visibilityConfigurationView;
            set
            {
                _visibilityConfigurationView = value;
                RaisePropertyChanged();
            }
        }

        public PlayerViewModel? PlayerViewModel { get; }
        public ConfigurationViewModel? ConfigurationViewModel { get; }
        
        public MainWindowViewModel()
        {
            PlayerViewModel = new PlayerViewModel(this);
            ConfigurationViewModel = new ConfigurationViewModel(this);

            VisibilityConfigurationView = Visibility.Hidden;
            VisiblePlayerView = Visibility.Hidden;

            ShowPlayerViewCommand = new DelegateCommand(ShowPlayerView);
            ShowConfigurationViewCommand = new DelegateCommand(ShowConfigurationView);

            var pack = new QuestionPack("Mina frågor");
            ActivePack = new QuestionPackViewModel(pack);
            ActivePack.Questions.Add(new Question($"Vad är 1+1", "2", "3", "1", "4"));
            ActivePack.Questions.Add(new Question($"Vad heter sveriges huvudstad?", "Stockholm", "Oslo", "London", "Göteborg"));
        }

        
        private void ShowPlayerView(object? obj)
        {
            VisiblePlayerView = Visibility.Visible;
            VisibilityConfigurationView = Visibility.Hidden;
        }

        private void ShowConfigurationView(object? obj)
        {
            VisibilityConfigurationView = Visibility.Visible;
            VisiblePlayerView = Visibility.Hidden;
        }
    }
}
