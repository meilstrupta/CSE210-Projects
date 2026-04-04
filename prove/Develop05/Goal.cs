using System;

namespace EternalQuest
{
    abstract class Goal
    {
        private string _title;
        private string _description;
        private int _score;
        private bool _isComplete;

        protected Goal(string title, string description, int score)
        {
            _title = title;
            _description = description;
            _score = score;
            _isComplete = false;
        }

        public virtual void InputGoal()
        {
            Console.Write("Enter title: ");
            _title = Console.ReadLine();

            Console.Write("Enter description: ");
            _description = Console.ReadLine();

            Console.Write("Enter base score (points): ");
            _score = int.Parse(Console.ReadLine() ?? "0");
        }

        public abstract int AddProgress();

        public abstract string DisplayScore();

        public virtual string GetStringRepresentation()
        {
            return $"{GetTypeName()}|{GetBaseString()}";
        }

        protected string GetTypeName()
        {
            return GetType().Name.Replace("Goal", "");
        }

        protected string GetBaseString()
        {
            return $"{_title}|{_description}|{_score}|{_isComplete}";
        }

        public string GetTitle()
        {
            return _title;
        }

        protected string GetDescription()
        {
            return _description;
        }

        protected int GetScore()
        {
            return _score;
        }

        protected bool GetIsComplete()
        {
            return _isComplete;
        }

        protected void SetIsComplete(bool value)
        {
            _isComplete = value;
        }

        public static Goal FromString(string line)
        {
            string[] parts = line.Split('|');
            string type = parts[0];

            switch (type)
            {
                case "Simple":
                    return SimpleGoal.FromParts(parts);
                case "Eternal":
                    return EternalGoal.FromParts(parts);
                case "Checklist":
                    return ChecklistGoal.FromParts(parts);
                default:
                    throw new Exception("Unknown goal type in file.");
            }
        }
    }
}