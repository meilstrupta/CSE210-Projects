using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    class Program
    {
        private static List<Goal> _goals = new List<Goal>();
        private static int _totalScore = 0;

        private static int Level => _totalScore / 1000 + 1;

        static void Main(string[] args)
        {
            StartAll();
        }

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
                Console.WriteLine("6. View Stats Dashboard");
                Console.WriteLine("7. Quit");
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
                        StatsTracker.DisplayStats();
                        break;

                    case "7":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

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
                    StatsTracker.RecordGoalCreated("Simple");
                    break;

                case "2":
                    goal = new EternalGoal();
                    goal.InputGoal();
                    StatsTracker.RecordGoalCreated("Eternal");
                    break;

                case "3":
                    goal = new ChecklistGoal();
                    goal.InputGoal();
                    StatsTracker.RecordGoalCreated("Checklist");
                    break;

                default:
                    Console.WriteLine("Invalid type.");
                    return;
            }

            _goals.Add(goal);
            Console.WriteLine("Goal created.");
        }

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

            StatsTracker.RecordGoalCompleted(goal.GetTitle(), points);

            Console.WriteLine($"You earned {points} points! Total score: {_totalScore}");
        }

        private static void SaveAll()
        {
            Console.Write("Enter filename to save to: ");
            string filename = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_totalScore);

                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }

            Console.WriteLine("Goals saved.");
        }

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
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                Goal goal = Goal.FromString(lines[i]);
                _goals.Add(goal);
            }

            Console.WriteLine("Goals loaded.");
        }
    }
}