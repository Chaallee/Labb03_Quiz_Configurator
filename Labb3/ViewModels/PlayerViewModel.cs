using Labb3.Command;
using Labb3.Models;
using Labb3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Labb3.ViewModels
{
    class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;
        private DispatcherTimer _timer;
        private DispatcherTimer _IconTimer;
        private List<Question> _shuffledQuestions; 

        public DelegateCommand AnswerCommand { get; }
        
        public QuestionPackViewModel? ActivePack { get => _mainWindowViewModel?.ActivePack; }

        private int _playerScore = 0;
        public int PlayerScore
        {
            get => _playerScore;
            private set
            {
                _playerScore = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ScoreDisplay));
            }
        }

        public string ScoreDisplay => $"Score: {PlayerScore}";

        private int _currentQuestionIndex = 0;
        
        private Question? _currentQuestion;

        public Question? CurrentQuestion
        {
            get => _currentQuestion;
            private set
            {
                _currentQuestion = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(CurrentQuestionText));
                RaisePropertyChanged(nameof(QuestionProgress));
            }
        }

        public string CurrentQuestionText => CurrentQuestion?.Query ?? "";

        public string QuestionProgress => $"Question {_currentQuestionIndex + 1} of {_shuffledQuestions?.Count ?? 0}";

        private Visibility _answer1IconVisibility = Visibility.Collapsed;
        public Visibility Answer1IconVisibility
        {
            get => _answer1IconVisibility;
            set
            {
                _answer1IconVisibility = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _answer2IconVisibility = Visibility.Collapsed;
        public Visibility Answer2IconVisibility
        {
            get => _answer2IconVisibility;
            set
            {
                _answer2IconVisibility = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _answer3IconVisibility = Visibility.Collapsed;
        public Visibility Answer3IconVisibility
        {
            get => _answer3IconVisibility;
            set
            {
                _answer3IconVisibility = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _answer4IconVisibility = Visibility.Collapsed;
        public Visibility Answer4IconVisibility
        {
            get => _answer4IconVisibility;
            set
            {
                _answer4IconVisibility = value;
                RaisePropertyChanged();
            }
        }

        private string? _answer1Icon = null;
        public string? Answer1Icon
        {
            get => _answer1Icon;
            set
            {
                _answer1Icon = value;
                RaisePropertyChanged();
            }
        }

        private string? _answer2Icon = null;
        public string? Answer2Icon
        {
            get => _answer2Icon;
            set
            {
                _answer2Icon = value;
                RaisePropertyChanged();
            }
        }

        private string? _answer3Icon = null;
        public string? Answer3Icon
        {
            get => _answer3Icon;
            set
            {
                _answer3Icon = value;
                RaisePropertyChanged();
            }
        }

        private string? _answer4Icon = null;
        public string? Answer4Icon
        {
            get => _answer4Icon;
            set
            {
                _answer4Icon = value;
                RaisePropertyChanged();
            }
        }

        private string _answer1 = "";
        public string Answer1 
        { 
            get => _answer1;
            private set
            {
                _answer1 = value;
                RaisePropertyChanged();
            }
        }

        private string _answer2 = "";
        public string Answer2 
        { 
            get => _answer2;
            private set
            {
                _answer2 = value;
                RaisePropertyChanged();
            }
        }

        private string _answer3 = "";
        public string Answer3 
        { 
            get => _answer3;
            private set
            {
                _answer3 = value;
                RaisePropertyChanged();
            }
        }

        private string _answer4 = "";
        public string Answer4 
        { 
            get => _answer4;
            private set
            {
                _answer4 = value;
                RaisePropertyChanged();
            }
        }

        private string _completionMessage = "";
        public string CompletionMessage
        {
            get => _completionMessage;
            set
            {
                _completionMessage = value;
                RaisePropertyChanged();
            }
        }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this._mainWindowViewModel = mainWindowViewModel;

            AnswerCommand = new DelegateCommand(OnAnswerSelected);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1.0);
            _timer.Tick += Timer_Tick;
            
            _IconTimer = new DispatcherTimer();
            _IconTimer.Interval = TimeSpan.FromSeconds(1.5);
            _IconTimer.Tick += IconTimer_Tick;

            _shuffledQuestions = new List<Question>();
        }

        public void StartGame()
        {
            _currentQuestionIndex = 0;
            PlayerScore = 0;
            
            ShuffleQuestions();
            
            HideAnswerIcons();
            LoadQuestion(0);
            _timer.Start();
        }

        private void ShuffleQuestions()
        {
            if (ActivePack?.Questions != null)
            {
                var random = new Random();
                _shuffledQuestions = ActivePack.Questions.OrderBy(_ => random.Next()).ToList();
            }
        }

        private void HideAnswerIcons()
        {
            Answer1IconVisibility = Visibility.Collapsed;
            Answer2IconVisibility = Visibility.Collapsed;
            Answer3IconVisibility = Visibility.Collapsed;
            Answer4IconVisibility = Visibility.Collapsed;
            
            Answer1Icon = null;
            Answer2Icon = null;
            Answer3Icon = null;
            Answer4Icon = null;
        }

        private void LoadQuestion(int index)
        {
            if (_shuffledQuestions != null && index >= 0 && index < _shuffledQuestions.Count)
            {
                _currentQuestionIndex = index;
                CurrentQuestion = _shuffledQuestions[index];
                ShuffleAnswers(CurrentQuestion);
                ResetTimer();
                
                HideAnswerIcons();
            }
        }

        private void ResetTimer()
        {
            TimeRemaining = ActivePack?.TimeLimitInSeconds ?? 30;
        }

        private int _timeRemaining;

        public int TimeRemaining
        {
            get => _timeRemaining;
            set
            {
                _timeRemaining = value;
                RaisePropertyChanged();
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining--;
            }
            else
            {
                MoveToNextQuestion();
            }
        }

        private void IconTimer_Tick(object? sender, EventArgs e)
        {
            _IconTimer.Stop();
            
            HideAnswerIcons();

            MoveToNextQuestion();
        }

        private void OnAnswerSelected(object? selectedAnswer)
        {
            if (selectedAnswer is string answer && CurrentQuestion != null)
            {
                _timer.Stop();
                
                bool isCorrect = answer == CurrentQuestion.CorrectAnswer;

                HideAnswerIcons();
              
                if (answer == Answer1)
                {
                    Answer1IconVisibility = Visibility.Visible;
                    Answer1Icon = isCorrect ? "/Images/correctanswer.png" : "/Images/wronganswer.png";
                }
                else if (answer == Answer2)
                {
                    Answer2IconVisibility = Visibility.Visible;
                    Answer2Icon = isCorrect ? "/Images/correctanswer.png" : "/Images/wronganswer.png";
                }
                else if (answer == Answer3)
                {
                    Answer3IconVisibility = Visibility.Visible;
                    Answer3Icon = isCorrect ? "/Images/correctanswer.png" : "/Images/wronganswer.png";
                }
                else if (answer == Answer4)
                {
                    Answer4IconVisibility = Visibility.Visible;
                    Answer4Icon = isCorrect ? "/Images/correctanswer.png" : "/Images/wronganswer.png";
                }

                if (!isCorrect)
                {
                    if (CurrentQuestion.CorrectAnswer == Answer1)
                    {
                        Answer1IconVisibility = Visibility.Visible;
                        Answer1Icon = "/Images/correctanswer.png";
                    }
                    else if (CurrentQuestion.CorrectAnswer == Answer2)
                    {
                        Answer2IconVisibility = Visibility.Visible;
                        Answer2Icon = "/Images/correctanswer.png";
                    }
                    else if (CurrentQuestion.CorrectAnswer == Answer3)
                    {
                        Answer3IconVisibility = Visibility.Visible;
                        Answer3Icon = "/Images/correctanswer.png";
                    }
                    else if (CurrentQuestion.CorrectAnswer == Answer4)
                    {
                        Answer4IconVisibility = Visibility.Visible;
                        Answer4Icon = "/Images/correctanswer.png";
                    }
                }

                if (isCorrect) PlayerScore++;

                _IconTimer.Start();
            }
        }

        private void MoveToNextQuestion()
        {
            int nextIndex = _currentQuestionIndex + 1;

            if (_shuffledQuestions != null && nextIndex < _shuffledQuestions.Count)
            {
                LoadQuestion(nextIndex);
                _timer.Start();
            }
            else
            {
                _timer.Stop();
                
                CompletionMessage = $"You got {PlayerScore} out of {_shuffledQuestions?.Count ?? 0} correct!";
                
                _mainWindowViewModel?.ShowQuizCompleteViewCommand.Execute(null);
            }
        }

        private void ShuffleAnswers(Question q)
        {
            var answersList = new List<string>
            {
                q.CorrectAnswer,
                q.IncorrectAnswers[0],
                q.IncorrectAnswers[1],
                q.IncorrectAnswers[2]
            };

            var shuffleAnswers = new Random();
            answersList = answersList.OrderBy(_ => shuffleAnswers.Next()).ToList();

            Answer1 = answersList[0];
            Answer2 = answersList[1];
            Answer3 = answersList[2];
            Answer4 = answersList[3];
        }
    }
}
