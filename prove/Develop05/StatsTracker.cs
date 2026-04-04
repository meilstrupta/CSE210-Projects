using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalQuest
{
    static class StatsTracker
    {
        private static int _totalPointsEarned = 0;
        private static int _totalGoalsCreated = 0;
        private static int _totalGoalsCompleted = 0;

        private static Dictionary<string, int> _goalTypeCounts = new Dictionary<string, int>
        {
            { "Simple", 0 },
            { "Eternal", 0 },
            { "Checklist", 0 }
        };

        private static string _lastCompletedGoal = "None";

        public static void RecordGoalCreated(string typeName)
        {
            _totalGoalsCreated++;

            if (_goalTypeCounts.ContainsKey(typeName))
            {
                _goalTypeCounts[typeName]++;
            }
        }

        public static void RecordGoalCompleted(string title, int points)
        {
            _totalPointsEarned += points;
            _lastCompletedGoal = title;
            _totalGoalsCompleted++;
        }

        public static void DisplayStats()
        {
            Console.Clear();
            Console.WriteLine("=== Eternal Quest Stats Dashboard ===\n");

            Console.WriteLine($"Total Points Earned: {_totalPointsEarned}");
            Console.WriteLine($"Total Goals Created: {_totalGoalsCreated}");
            Console.WriteLine($"Total Goals Completed: {_totalGoalsCompleted}");
            Console.WriteLine($"Most Recently Completed Goal: {_lastCompletedGoal}\n");

            Console.WriteLine("Goal Type Breakdown:");
            foreach (var pair in _goalTypeCounts)
            {
                Console.WriteLine($"- {pair.Key}: {pair.Value}");
            }

            string mostUsed = _goalTypeCounts
                .OrderByDescending(x => x.Value)
                .First().Key;

            Console.WriteLine($"\nMost Frequently Created Goal Type: {mostUsed}");

            Console.WriteLine("\nPress Enter to return to the main menu.");
            Console.ReadLine();
        }
    }
}