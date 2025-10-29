namespace Labb3.Models
{
    enum Difficulty { Easy, Medium, Hard }
    internal class QuestionPack
    {
        public QuestionPack(string name, Difficulty diffuculty = Difficulty.Medium, int timeLimitInSeconds = 30)
        {
            Name = name;
            Diffuculty = diffuculty;
            TimeLimitInSeconds = timeLimitInSeconds;
            Questions = new List<Question>();
        }

        public string Name { get; set; }
        public Difficulty Diffuculty { get; set; }

        public int TimeLimitInSeconds { get; set; }

        public List<Question> Questions { get; set; }
    }
}
