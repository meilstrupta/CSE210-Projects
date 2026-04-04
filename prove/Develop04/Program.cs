using System;

namespace MindfulnessApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. View Stats Dashboard");
                Console.WriteLine("5. Quit");
                Console.Write("\nSelect a choice from the menu: ");

                string choice = Console.ReadLine();
                Activity activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;

                    case "2":
                        activity = new ReflectionActivity();
                        break;

                    case "3":
                        activity = new ListingActivity();
                        break;

                    case "4":
                        StatsTracker.DisplayStats();
                        continue;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        continue;
                }

                activity.Run();

                Console.WriteLine("\nPress Enter to return to the main menu.");
                Console.ReadLine();
            }
        }
    }
}