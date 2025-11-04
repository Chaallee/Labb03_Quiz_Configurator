
namespace Labb3.Models;

internal class Question
{

    
    public Question(string query, string correctAnswer,
        string incorrectAnswer1, string incorrectAnswer2, string incorrectAnswer3)
    {
        Query = query;
        CorrectAnswer = correctAnswer;
        IncorrectAnswers = [incorrectAnswer1, incorrectAnswer2, incorrectAnswer3];

    }
        

    public string Query { get; set; }         // byt namn sen

    public string CorrectAnswer { get; set; }

    public string[] IncorrectAnswers { get; set; }
}
