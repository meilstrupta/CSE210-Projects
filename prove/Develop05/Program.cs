using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // ===== Base class =====
    abstract class Goal
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public int Score { get; protected set; }
        public bool IsComplete { get; protected set; }

        protected Goal(string title, string description, int score)
        {
            Title = title;
            Description = description;
            Score = score;
            IsComplete = false;
        }

        public virtual void InputGoal()
        {
            Console.Write("Enter title: ");
            Title = Console.ReadLine();

            Console.Write("Enter description: ");
            Description = Console.ReadLine();

            Console.Write("Enter base score (points): ");
            Score = int.Parse(Console.ReadLine() ?? "0");
        }

        public abstract int AddProgress();

        public abstract string DisplayScore();

        public abstract string GetStringRepresentation();

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

        protected string GetBaseString()
        {
            return $"{Title}|{Description}|{Score}|{IsComplete}";
        }
    }

    // ===== Simple goal =====
    class SimpleGoal : Goal
    {
        public string CheckScoreLabel { get; private set; } = "Completed once";

        public SimpleGoal() : base("", "", 0) { }

        public SimpleGoal(string title, string description, int score, bool isComplete)
            : base(title, description, score)
        {
            IsComplete = isComplete;
        }

        public override int AddProgress()
        {
            if (!IsComplete)
            {
                IsComplete = true;
                return Score;
            }
            else
            {
                Console.WriteLine("This simple goal is already complete.");
                return 0;
            }
        }

        public override string DisplayScore()
        {
            string status = IsComplete ? "[X]" : "[ ]";
            return $"{status} {Title} ({Description}) - {CheckScoreLabel}";
        }

        public override string GetStringRepresentation()
        {
            return $"Simple|{GetBaseString()}|{CheckScoreLabel}";
        }

        public static SimpleGoal FromParts(string[] parts)
        {
            string title = parts[1];
            string desc = parts[2];
            int score = int.Parse(parts[3]);
            bool isComplete = bool.Parse(parts[4]);
            string label = parts.Length > 5 ? parts[5] : "Completed once";

            var g = new SimpleGoal(title, desc, score, isComplete);
            g.CheckScoreLabel = label;
            return g;
        }
    }

    // ===== Eternal goal =====
    class EternalGoal : Goal
    {
        public int TimesCompleted { get; private set; }
        public DateTime? LastDate { get; private set; }

        public EternalGoal() : base("", "", 0) { }

        public EternalGoal(string title, string description, int score,
                           int timesCompleted, DateTime? lastDate)
            : base(title, description, score)
        {
            TimesCompleted = timesCompleted;
            LastDate = lastDate;
        }

        public override int AddProgress()
        {
            TimesCompleted++;
            LastDate = DateTime.Now;
            // NeverFinish() – eternal goals never set IsComplete = true
            return Score;
        }

        public override string DisplayScore()
        {
            string dateText = LastDate.HasValue ? LastDate.Value.ToShortDateString() : "never";
            return $"[∞] {Title} ({Description}) - Completed {TimesCompleted} times, last on {dateText}";
        }

        public override string GetStringRepresentation()
        {
            string dateStr = LastDate.HasValue ? LastDate.Value.ToString("o") : "";
            return $"Eternal|{GetBaseString()}|{TimesCompleted}|{dateStr}";
        }

        public static EternalGoal FromParts(string[] parts)
        {
            string title = parts[1];
            string desc = parts[2];
            int score = int.Parse(parts[3]);
            bool isComplete = bool.Parse(parts[4]); // should always be false, but read it anyway
            int times = int.Parse(parts[5]);
            DateTime? date = string.IsNullOrWhiteSpace(parts[6])
                ? (DateTime?)null
                : DateTime.Parse(parts[6]);

            var g = new EternalGoal(title, desc, score, times, date);
            g.IsComplete = isComplete;
            return g;
        }
    }

    // ===== Checklist goal =====
    class ChecklistGoal : Goal
    {
        public int CompletionTotal { get; private set; }
        public int CompletionCount { get; private set; }
        public double BonusScore { get; private set; }
        public string CheckDescription { get; private set; }
        public DateTime? LastDate { get; private set; }

        public ChecklistGoal() : base("", "", 0) { }

        public ChecklistGoal(string title, string description, int score,
                             int completionTotal, int completionCount,
                             double bonusScore, string checkDescription,
                             DateTime? lastDate, bool isComplete)
            : base(title, description, score)
        {
            CompletionTotal = completionTotal;
            CompletionCount = completionCount;
            BonusScore = bonusScore;
            CheckDescription = checkDescription;
            LastDate = lastDate;
            IsComplete = isComplete;
        }

        public override void InputGoal()
        {
            base.InputGoal();

            Console.Write("Enter how many times to complete this goal: ");
            CompletionTotal = int.Parse(Console.ReadLine() ?? "1");

            Console.Write("Enter bonus score when fully completed: ");
            BonusScore = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter checklist description (e.g., 'Temple trips'): ");
            CheckDescription = Console.ReadLine();
        }

        public override int AddProgress()
        {
            if (IsComplete)
            {
                Console.WriteLine("Checklist goal already fully completed.");
                return 0;
            }

            CompletionCount++;
            LastDate = DateTime.Now;

            int points = Score;

            if (CompletionCount >= CompletionTotal)
            {
                IsComplete = true;
                points += (int)BonusScore;
                Console.WriteLine("Checklist goal completed! Bonus awarded.");
            }

            return points;
        }

        public override string DisplayScore()
        {
            string status = IsComplete ? "[X]" : "[ ]";
            string dateText = LastDate.HasValue ? LastDate.Value.ToShortDateString() : "never";
            return $"{status} {Title} ({Description}) - {CheckDescription}, Completed {CompletionCount}/{CompletionTotal} times, last on {dateText}";
        }

        public override string GetStringRepresentation()
        {
            string dateStr = LastDate.HasValue ? LastDate.Value.ToString("o") : "";
            return $"Checklist|{GetBaseString()}|{CompletionTotal}|{CompletionCount}|{BonusScore}|{CheckDescription}|{dateStr}";
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

    // ===== Program (controller) =====
    class Program
    {
        private static List<Goal> _goals = new List<Goal>();
        private static int _totalScore = 0;

        private static int Level => _totalScore / 1000 + 1;

        static void Main(string[] args)
        {
            StartAll();
        }

        // Start ALL()
        private static void StartAll()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== Eternal Quest ===");
                Console.WriteLine($"Score: {_totalScore}   Level: {Level}");
                Console.WriteLine("1. View goals");
                Console.WriteLine("2. Create new goal");
                Console.WriteLine("3. Record event");
                Console.WriteLine("4. Save goals");
                Console.WriteLine("5. Load goals");
                Console.WriteLine("6. Quit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ViewGoals();
                        break;
                    case "2":
                        CreateGoal();
                        break;
                    case "3":
                        RecordEvent();
                        break;
                    case "4":
                        SaveAll();
                        break;
                    case "5":
                        LoadAll();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // View Goals()
        private static void ViewGoals()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals yet.");
                return;
            }

            Console.WriteLine("Your goals:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].DisplayScore()}");
            }
        }

        private static void CreateGoal()
        {
            Console.WriteLine("Select goal type:");
            Console.WriteLine("1. Simple goal");
            Console.WriteLine("2. Eternal goal");
            Console.WriteLine("3. Checklist goal");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            Goal goal = null;

            switch (choice)
            {
                case "1":
                    goal = new SimpleGoal();
                    goal.InputGoal();
                    break;
                case "2":
                    goal = new EternalGoal();
                    goal.InputGoal();
                    break;
                case "3":
                    goal = new ChecklistGoal();
                    goal.InputGoal();
                    break;
                default:
                    Console.WriteLine("Invalid type.");
                    return;
            }

            _goals.Add(goal);
            Console.WriteLine("Goal created.");
        }

        // Record an event (AddProgress)
        private static void RecordEvent()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals to record.");
                return;
            }

            ViewGoals();
            Console.Write("Which goal did you accomplish? (number): ");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            index -= 1;
            if (index < 0 || index >= _goals.Count)
            {
                Console.WriteLine("Invalid goal number.");
                return;
            }

            Goal goal = _goals[index];
            int points = goal.AddProgress();
            _totalScore += points;

            Console.WriteLine($"You earned {points} points! Total score: {_totalScore}");
        }

        // Save ALL()
        private static void SaveAll()
        {
            Console.Write("Enter filename to save to: ");
            string filename = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_totalScore);
                foreach (Goal g in _goals)
                {
                    writer.WriteLine(g.GetStringRepresentation());
                }
            }

            Console.WriteLine("Goals saved.");
        }

        // Load ALL()
        private static void LoadAll()
        {
            Console.Write("Enter filename to load from: ");
            string filename = Console.ReadLine();

            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.");
                return;
            }

            _goals.Clear();

            string[] lines = File.ReadAllLines(filename);
            if (lines.Length == 0)
            {
                Console.WriteLine("File is empty.");
                return;
            }

            _totalScore = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                Goal g = Goal.FromString(lines[i]);
                _goals.Add(g);
            }

            Console.WriteLine("Goals loaded.");
        }
    }
}