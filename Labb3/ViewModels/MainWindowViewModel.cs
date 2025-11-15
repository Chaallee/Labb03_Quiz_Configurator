using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Labb3.Command;
using Labb3.Models;
using Labb3.Views;
using Labb3.Services;

namespace Labb3.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private readonly QuestionPackService _questionPackService;

        public ObservableCollection<QuestionPackViewModel> Packs { get; } = new();

        private QuestionPackViewModel _activePack;
        private Visibility _visibilityConfigurationView;
        private Visibility _visiblePlayerView;
        private Visibility _visibilityQuizCompleteView;

        public QuestionPackViewModel ActivePack
        {
            get => _activePack;
            set
            {
                _activePack = value;
                RaisePropertyChanged();
                PlayerViewModel?.RaisePropertyChanged(nameof(PlayerViewModel.ActivePack));
            }
        }

        public DelegateCommand ShowPlayerViewCommand { get; }
        public DelegateCommand ShowConfigurationViewCommand { get; }
        public DelegateCommand NewQuestionPackCommand { get; }
        public DelegateCommand SelectPackCommand { get; }
        public DelegateCommand DeletePackCommand { get; }
        public DelegateCommand ToggleFullscreenCommand { get; }
        public DelegateCommand ExitCommand { get; }
        public DelegateCommand ShowQuizCompleteViewCommand { get; }

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

        public Visibility VisibilityQuizCompleteView
        {
            get => _visibilityQuizCompleteView;
            set
            {
                _visibilityQuizCompleteView = value;
                RaisePropertyChanged();
            }
        }

        public PlayerViewModel? PlayerViewModel { get; }
        public ConfigurationViewModel? ConfigurationViewModel { get; }

        public MainWindowViewModel()
        {
            _questionPackService = new QuestionPackService();

            PlayerViewModel = new PlayerViewModel(this);
            ConfigurationViewModel = new ConfigurationViewModel(this);

            VisibilityConfigurationView = Visibility.Visible;
            VisiblePlayerView = Visibility.Hidden;
            VisibilityQuizCompleteView = Visibility.Hidden; 

            ShowPlayerViewCommand = new DelegateCommand(ShowPlayerView);
            ShowConfigurationViewCommand = new DelegateCommand(ShowConfigurationView);
            NewQuestionPackCommand = new DelegateCommand(async _ => await CreateNewQuestionPack());
            SelectPackCommand = new DelegateCommand(SelectPack);
            DeletePackCommand = new DelegateCommand(async parameter => await DeletePack(parameter));
            ToggleFullscreenCommand = new DelegateCommand(ToggleFullscreen);
            ExitCommand = new DelegateCommand(ExitApplication);
            ShowQuizCompleteViewCommand = new DelegateCommand(ShowQuizCompleteView); 

            var pack = new QuestionPack("Mina frågor");
            ActivePack = new QuestionPackViewModel(pack);
            ActivePack.Questions.Add(new Question($"Vad är 1+1", "2", "3", "1", "4"));
            ActivePack.Questions.Add(new Question($"Vad heter sveriges huvudstad?", "Stockholm", "Oslo", "London", "Göteborg"));

            Packs.Add(ActivePack);

            _ = LoadAllPacks();
        }

        private void SelectPack(object? parameter)
        {
            if (parameter is QuestionPackViewModel selectedPack)
            {
                ActivePack = selectedPack;
                ShowConfigurationView(null);
            }
        }

        private async Task DeletePack(object? parameter)
        {
            if (parameter is QuestionPackViewModel packToDelete)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete '{packToDelete.Name}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    await _questionPackService.DeleteQuestionPackAsync(packToDelete.Name);

                    Packs.Remove(packToDelete);

                    if (ActivePack == packToDelete)
                    {
                        ActivePack = Packs.FirstOrDefault();
                    }
                }
            }
        }

        private async Task CreateNewQuestionPack()
        {
            var dialog = new NewQuestionPack
            {
                Owner = Application.Current.MainWindow,
                PackName = "New Question Pack",
                Difficulty = Difficulty.Medium,
                TimeLimitInSeconds = 30
            };

            if (dialog.ShowDialog() == true)
            {
                var newPack = new QuestionPack(
                    dialog.PackName,
                    dialog.Difficulty,
                    dialog.TimeLimitInSeconds
                );

                var packViewModel = new QuestionPackViewModel(newPack);
                Packs.Add(packViewModel);
                ActivePack = packViewModel;

                await _questionPackService.SaveQuestionPackAsync(newPack);

                ShowConfigurationView(null);
            }
        }

        private async Task LoadAllPacks()
        {
            var loadedPacks = await _questionPackService.LoadAllQuestionPacksAsync();

            foreach (var pack in loadedPacks)
            {
                if (!Packs.Any(p => p.Name == pack.PackName))
                {
                    var packViewModel = new QuestionPackViewModel(pack);
                    Packs.Add(packViewModel);
                }
            }
        }

        private void ShowQuizCompleteView(object? obj)
        {
            VisibilityQuizCompleteView = Visibility.Visible;
            VisiblePlayerView = Visibility.Hidden;
            VisibilityConfigurationView = Visibility.Hidden;
        }

        private void ShowPlayerView(object? obj)
        {
            VisiblePlayerView = Visibility.Visible;
            VisibilityConfigurationView = Visibility.Hidden;
            VisibilityQuizCompleteView = Visibility.Hidden;

            PlayerViewModel?.StartGame();
        }

        private void ShowConfigurationView(object? obj)
        {
            VisibilityConfigurationView = Visibility.Visible;
            VisiblePlayerView = Visibility.Hidden;
            VisibilityQuizCompleteView = Visibility.Hidden;
        }

        private void ToggleFullscreen(object? obj)
        {
            var window = Application.Current.MainWindow;

            if (window != null)
            {
                if (window.WindowState == WindowState.Normal)
                {
                    window.WindowState = WindowState.Maximized;
                    window.WindowStyle = WindowStyle.None;
                }
                else
                {
                    window.WindowState = WindowState.Normal;
                    window.WindowStyle = WindowStyle.SingleBorderWindow;
                }
            }
        }

        private void ExitApplication(object? obj)
        {
            Application.Current.Shutdown();
        }
    }
}
