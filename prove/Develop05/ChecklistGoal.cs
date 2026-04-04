using System;

namespace EternalQuest
{
    class ChecklistGoal : Goal
    {
        private int _completionTotal;
        private int _completionCount;
        private double _bonusScore;
        private string _checkDescription;
        private DateTime? _lastDate;

        public ChecklistGoal() : base("", "", 0)
        {
        }

        private ChecklistGoal(string title, string description, int score,
                              int completionTotal, int completionCount,
                              double bonusScore, string checkDescription,
                              DateTime? lastDate, bool isComplete)
            : base(title, description, score)
        {
            _completionTotal = completionTotal;
            _completionCount = completionCount;
            _bonusScore = bonusScore;
            _checkDescription = checkDescription;
            _lastDate = lastDate;
            SetIsComplete(isComplete);
        }

        public override void InputGoal()
        {
            base.InputGoal();

            Console.Write("Enter how many times to complete this goal: ");
            _completionTotal = int.Parse(Console.ReadLine() ?? "1");

            Console.Write("Enter bonus score when fully completed: ");
            _bonusScore = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter checklist description (e.g., 'Temple trips'): ");
            _checkDescription = Console.ReadLine();
        }

        public override int AddProgress()
        {
            if (GetIsComplete())
            {
                Console.WriteLine("Checklist goal already fully completed.");
                return 0;
            }

            _completionCount++;
            _lastDate = DateTime.Now;

            int points = GetScore();

            if (_completionCount >= _completionTotal)
            {
                SetIsComplete(true);
                points += (int)_bonusScore;
                Console.WriteLine("Checklist goal completed! Bonus awarded.");
            }

            return points;
        }

        public override string DisplayScore()
        {
            string status = GetIsComplete() ? "[X]" : "[ ]";
            string dateText = _lastDate.HasValue ? _lastDate.Value.ToShortDateString() : "never";
            return $"{status} {GetTitle()} ({GetDescription()}) - {_checkDescription}, Completed {_completionCount}/{_completionTotal} times, last on {dateText}";
        }

        public override string GetStringRepresentation()
        {
            string dateStr = _lastDate.HasValue ? _lastDate.Value.ToString("o") : "";
            return $"Checklist|{GetBaseString()}|{_completionTotal}|{_completionCount}|{_bonusScore}|{_checkDescription}|{dateStr}";
        }

        public static ChecklistGoal FromParts(string[] parts)
        {
            string title = parts[1];
            string desc = parts[2];
            int score = int.Parse(parts[3]);
            bool isComplete = bool.Parse(parts[4]);
            int total = int.Parse(parts[5]);
            int count = int.Parse(parts[6]);
            double bonus = double.Parse(parts[7]);
            string checkDesc = parts[8];
            DateTime? date = parts.Length > 9 && !string.IsNullOrWhiteSpace(parts[9])
                ? DateTime.Parse(parts[9])
                : (DateTime?)null;

            return new ChecklistGoal(title, desc, score, total, count, bonus, checkDesc, date, isComplete);
        }
    }
}