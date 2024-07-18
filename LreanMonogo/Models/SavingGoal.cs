namespace LearnMongo.Models
{
    public class SavingGoal
    {
        public string GoalId { get; set; }

        public string UserId { get; set; }

        public string GoalName { get; set; }

        public double TargetAmount { get; set; }

        public double CurrentAmount { get; set; }

        public DateTime Deadline { get; set; }
    }
}
