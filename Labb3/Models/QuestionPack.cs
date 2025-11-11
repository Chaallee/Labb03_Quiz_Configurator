using System;
using System.Collections.Generic;
namespace Labb3.Models;

public enum Difficulty { Easy, Medium, Hard }  

public class QuestionPack 
{
    public QuestionPack(string packName, Difficulty difficulty = Difficulty.Medium, int timeLimitInSeconds = 30)
    {
        PackName = packName;
        Difficulty = difficulty;
        TimeLimitInSeconds = timeLimitInSeconds;
        Questions = new List<Question>();
    }

    public string PackName { get; set; }
    public Difficulty Difficulty { get; set; }
    public int TimeLimitInSeconds { get; set; }

    public List<Question> Questions { get; set; }
}
