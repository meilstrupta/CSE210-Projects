using System;

namespace EternalQuest
{
    class SimpleGoal : Goal
    {
        private string _checkScoreLabel = "Completed once";

        public SimpleGoal() : base("", "", 0)
        {
        }

        private SimpleGoal(string title, string description, int score, bool isComplete, string label)
            : base(title, description, score)
        {
            SetIsComplete(isComplete);
            _checkScoreLabel = label;
        }

        public override int AddProgress()
        {
            if (!GetIsComplete())
            {
                SetIsComplete(true);
                return GetScore();
            }
            else
            {
                Console.WriteLine("This simple goal is already complete.");
                return 0;
            }
        }

        public override string DisplayScore()
        {
            string status = GetIsComplete() ? "[X]" : "[ ]";
            return $"{status} {GetTitle()} ({GetDescription()}) - {_checkScoreLabel}";
        }

        public override string GetStringRepresentation()
        {
            return $"Simple|{GetBaseString()}|{_checkScoreLabel}";
        }

        public static SimpleGoal FromParts(string[] parts)
        {
            string title = parts[1];
            string desc = parts[2];
            int score = int.Parse(parts[3]);
            bool isComplete = bool.Parse(parts[4]);
            string label = parts.Length > 5 ? parts[5] : "Completed once";

            return new SimpleGoal(title, desc, score, isComplete, label);
        }
    }
}