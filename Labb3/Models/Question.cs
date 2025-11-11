using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Labb3.Models;


public class Question : INotifyPropertyChanged  
{
    public Question(string query, string correctAnswer,
        string incorrectAnswer1, string incorrectAnswer2, string incorrectAnswer3)
    {
        _query = query;
        _correctAnswer = correctAnswer;
        IncorrectAnswers = [incorrectAnswer1, incorrectAnswer2, incorrectAnswer3];
    }

    private string _query;
    public string Query
    {
        get => _query;
        set
        {
            _query = value;
            OnPropertyChanged();
        }
    }

    private string _correctAnswer;
    public string CorrectAnswer
    {
        get => _correctAnswer;
        set
        {
            _correctAnswer = value;
            OnPropertyChanged();
        }
    }

    public string[] IncorrectAnswers { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
