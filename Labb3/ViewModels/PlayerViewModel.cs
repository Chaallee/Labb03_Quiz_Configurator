using Labb3.Command;
using Labb3.Models;
using Labb3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Labb3.ViewModels
{
    class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;
        public DelegateCommand SetPackNameCommand { get; }
        public QuestionPackViewModel? ActivePack { get => _mainWindowViewModel?.ActivePack; }


        public string Answer1 { get; private set; }
        public string Answer2 { get; private set; }
        public string Answer3 { get; private set; }
        public string Answer4 { get; private set; }


        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this._mainWindowViewModel = mainWindowViewModel;

            SetPackNameCommand = new DelegateCommand(SetPackName, CanSetPackName);
            DemoText = string.Empty;


            TimeRemaining = 30;




            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1.0);
            timer.Tick += Timer_Tick;
            timer.Start();
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

            {
                if (TimeRemaining > 0)
                    TimeRemaining--;
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

            RaisePropertyChanged(nameof(Answer1));
            RaisePropertyChanged(nameof(Answer2));
            RaisePropertyChanged(nameof(Answer3));
            RaisePropertyChanged(nameof(Answer4));
        }


        private string _demoText;

        public string DemoText
        {
            get { return _demoText; }
            set
            {
                _demoText = value;
                RaisePropertyChanged();
                SetPackNameCommand.RaiseCanExecuteChanged();
            }
        }


        private bool CanSetPackName(object? arg)
        {
            return DemoText.Length > 0;
        }

        private void SetPackName(object? obj)
        {
            ActivePack.Name = DemoText;
        }

       
    }
}
