using System;
using System.Collections.Generic;
using System.Linq;

namespace MindfulnessApp
{
    static class StatsTracker
    {
        private static int _totalMeditationSeconds = 0;
        private static Dictionary<string, int> _activityCounts = new Dictionary<string, int>
        {
            { "Breathing", 0 },
            { "Reflection", 0 },
            { "Listing", 0 }
        };

        public static void RecordSession(string activityName, int duration)
        {
            _totalMeditationSeconds += duration;

            if (_activityCounts.ContainsKey(activityName))
            {
                _activityCounts[activityName]++;
            }
        }

        public static void DisplayStats()
        {
            Console.Clear();
            Console.WriteLine("=== Mindfulness Stats Dashboard ===\n");

            Console.WriteLine($"Total Time Meditating: {_totalMeditationSeconds} seconds");

            string mostUsed = _activityCounts
                .OrderByDescending(a => a.Value)
                .First().Key;

            Console.WriteLine($"Most Used Activity: {mostUsed}");

            Console.WriteLine("\nActivity Breakdown:");
            foreach (var pair in _activityCounts)
            {
                Console.WriteLine($"- {pair.Key}: {pair.Value} sessions");
            }

            Console.WriteLine("\nPress Enter to return to the main menu.");
            Console.ReadLine();
        }
    }
}