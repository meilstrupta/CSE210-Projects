using System;

namespace EternalQuest
{
    class EternalGoal : Goal
    {
        private int _timesCompleted;
        private DateTime? _lastDate;

        public EternalGoal() : base("", "", 0)
        {
        }

        private EternalGoal(string title, string description, int score,
                            int timesCompleted, DateTime? lastDate, bool isComplete)
            : base(title, description, score)
        {
            _timesCompleted = timesCompleted;
            _lastDate = lastDate;
            SetIsComplete(isComplete); // should always be false, but preserved
        }

        public override int AddProgress()
        {
            _timesCompleted++;
            _lastDate = DateTime.Now;
            return GetScore();
        }

        public override string DisplayScore()
        {
            string dateText = _lastDate.HasValue ? _lastDate.Value.ToShortDateString() : "never";
            return $"[∞] {GetTitle()} ({GetDescription()}) - Completed {_timesCompleted} times, last on {dateText}";
        }

        public override string GetStringRepresentation()
        {
            string dateStr = _lastDate.HasValue ? _lastDate.Value.ToString("o") : "";
            return $"Eternal|{GetBaseString()}|{_timesCompleted}|{dateStr}";
        }

        public static EternalGoal FromParts(string[] parts)
        {
            string title = parts[1];
            string desc = parts[2];
            int score = int.Parse(parts[3]);
            bool isComplete = bool.Parse(parts[4]);
            int times = int.Parse(parts[5]);
            DateTime? date = string.IsNullOrWhiteSpace(parts[6])
                ? (DateTime?)null
                : DateTime.Parse(parts[6]);

            return new EternalGoal(title, desc, score, times, date, isComplete);
        }
    }
}