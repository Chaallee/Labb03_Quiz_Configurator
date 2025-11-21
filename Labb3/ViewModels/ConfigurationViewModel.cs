using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using Labb3.Command;
using Labb3.Models;
using Labb3.Views;

namespace Labb3.ViewModels
{
    class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        public DelegateCommand AddQuestionCommand { get; private set; }
        public DelegateCommand PackOptionsCommand { get; private set; }
        public DelegateCommand RemoveQuestionCommand { get; private set; }
        public DelegateCommand SaveQuestionCommand { get; private set; }

        private Question? _selectedQuestion;
        public Question? SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsQuestionSelected)); 
                LoadQuestionToEditor();
                RemoveQuestionCommand.RaiseCanExecuteChanged();
                SaveQuestionCommand.RaiseCanExecuteChanged();
            }
        }

        private string _editQuery = string.Empty;
        public string EditQuery
        {
            get => _editQuery;
            set
            {
                _editQuery = value;
                RaisePropertyChanged();
                SaveQuestionCommand.RaiseCanExecuteChanged(); 
            }
        }

        private string _editCorrectAnswer = string.Empty;
        public string EditCorrectAnswer
        {
            get => _editCorrectAnswer;
            set
            {
                _editCorrectAnswer = value;
                RaisePropertyChanged();
                SaveQuestionCommand.RaiseCanExecuteChanged();
            }
        }

        private string _editIncorrectAnswer1 = string.Empty;
        public string EditIncorrectAnswer1
        {
            get => _editIncorrectAnswer1;
            set
            {
                _editIncorrectAnswer1 = value;
                RaisePropertyChanged();
                SaveQuestionCommand.RaiseCanExecuteChanged(); 
            }
        }

        private string _editIncorrectAnswer2 = string.Empty;
        public string EditIncorrectAnswer2
        {
            get => _editIncorrectAnswer2;
            set
            {
                _editIncorrectAnswer2 = value;
                RaisePropertyChanged();
                SaveQuestionCommand.RaiseCanExecuteChanged(); 
            }
        }

        private string _editIncorrectAnswer3 = string.Empty;
        public string EditIncorrectAnswer3
        {
            get => _editIncorrectAnswer3;
            set
            {
                _editIncorrectAnswer3 = value;
                RaisePropertyChanged();
                SaveQuestionCommand.RaiseCanExecuteChanged(); 
            }
        }

        public QuestionPackViewModel? ActivePack => mainWindowViewModel?.ActivePack;

        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            AddQuestionCommand = new DelegateCommand(AddQuestion, CanAddQuestion);
            PackOptionsCommand = new DelegateCommand(PackOptions);
            RemoveQuestionCommand = new DelegateCommand(RemoveQuestion, CanRemoveQuestion);
            SaveQuestionCommand = new DelegateCommand(SaveQuestion, CanSaveQuestion);

            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.PropertyChanged += OnMainWindowPropertyChanged;
            }
        }

        private void OnMainWindowPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowViewModel.ActivePack))
            {
                RaisePropertyChanged(nameof(ActivePack));
                AddQuestionCommand.RaiseCanExecuteChanged();
                RemoveQuestionCommand.RaiseCanExecuteChanged();
                SaveQuestionCommand.RaiseCanExecuteChanged();
            }
        }

        private void LoadQuestionToEditor()
        {
            if (SelectedQuestion != null)
            {
                EditQuery = SelectedQuestion.Query;
                EditCorrectAnswer = SelectedQuestion.CorrectAnswer;
                EditIncorrectAnswer1 = SelectedQuestion.IncorrectAnswers[0];
                EditIncorrectAnswer2 = SelectedQuestion.IncorrectAnswers[1];
                EditIncorrectAnswer3 = SelectedQuestion.IncorrectAnswers[2];
            }
            else
            {
                EditQuery = string.Empty;
                EditCorrectAnswer = string.Empty;
                EditIncorrectAnswer1 = string.Empty;
                EditIncorrectAnswer2 = string.Empty;
                EditIncorrectAnswer3 = string.Empty;
            }
        }

        private bool CanSaveQuestion(object? obj)
        {
            if (SelectedQuestion == null || mainWindowViewModel?.ActivePack == null)
                return false;


            bool isTextBoxModified = EditQuery != SelectedQuestion.Query ||
                              EditCorrectAnswer != SelectedQuestion.CorrectAnswer ||
                              EditIncorrectAnswer1 != SelectedQuestion.IncorrectAnswers[0] ||
                              EditIncorrectAnswer2 != SelectedQuestion.IncorrectAnswers[1] ||
                              EditIncorrectAnswer3 != SelectedQuestion.IncorrectAnswers[2];

            return isTextBoxModified;
        }

        private void SaveQuestion(object? obj)
        {
            if (string.IsNullOrWhiteSpace(EditQuery))
            {
                MessageBox.Show("Question text cannot be empty.", "Empty text box error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(EditCorrectAnswer))
            {
                MessageBox.Show("Please write a correct answer.", "Empty text box error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(EditIncorrectAnswer1))
            {
                MessageBox.Show("First incorrect answer box is still empty.", "Empty text box error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(EditIncorrectAnswer2))
            {
                MessageBox.Show("Second incorrect answer box is still empty.", "Empty text box error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(EditIncorrectAnswer3))
            {
                MessageBox.Show("Third incorrect answer box is still empty.", "Empty text box error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedQuestion!.Query = EditQuery;
            SelectedQuestion.CorrectAnswer = EditCorrectAnswer;
            SelectedQuestion.IncorrectAnswers[0] = EditIncorrectAnswer1;
            SelectedQuestion.IncorrectAnswers[1] = EditIncorrectAnswer2;
            SelectedQuestion.IncorrectAnswers[2] = EditIncorrectAnswer3;

            SaveQuestionCommand.RaiseCanExecuteChanged();
        }

        private bool CanRemoveQuestion(object? obj)
        {
            return SelectedQuestion != null && mainWindowViewModel?.ActivePack != null;
        }

        private void RemoveQuestion(object? obj)
        {
            mainWindowViewModel!.ActivePack.Questions.Remove(SelectedQuestion!);
            SelectedQuestion = null;
        }

        private void PackOptions(object? obj)
        {
            if (mainWindowViewModel?.ActivePack != null)
            {
                var dialog = new PackOptions
                {
                    DataContext = mainWindowViewModel.ActivePack,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Application.Current.MainWindow
                };
                dialog.ShowDialog();
            }
        }

        private void AddQuestion(object? obj)
        {
            if (mainWindowViewModel?.ActivePack == null)
            {
                MessageBox.Show("Please create a question pack first.", "No Active pack", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var newQuestion = new Question(
                query: "Write a new question here",
                correctAnswer: string.Empty,
                incorrectAnswer1: string.Empty,
                incorrectAnswer2: string.Empty,
                incorrectAnswer3: string.Empty
            );

            mainWindowViewModel.ActivePack.Questions.Add(newQuestion);
            SelectedQuestion = newQuestion;
        }

        private bool CanAddQuestion(object? obj)
        {
            return mainWindowViewModel?.ActivePack != null;
        }

        public bool IsQuestionSelected => SelectedQuestion != null;
    }
}
