using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    // Base class
    abstract class Activity
    {
        private string _name;
        private string _description;
        private int _duration; // in seconds

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public void Run()
        {
            ShowStartingMessage();
            PerformActivity();
            ShowEndingMessage();
        }

        protected abstract void PerformActivity();

        private void ShowStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to the {_name} Activity.\n");
            Console.WriteLine(_description);
            Console.Write("\nHow long, in seconds, would you like your session to last? ");

            while (!int.TryParse(Console.ReadLine(), out _duration) || _duration <= 0)
            {
                Console.Write("Please enter a positive integer for seconds: ");
            }

            Console.WriteLine("\nGet ready to begin...");
            ShowSpinner(3);
        }

        private void ShowEndingMessage()
        {
            Console.WriteLine("\nWell done! You’ve completed the activity.");
            ShowSpinner(3);
            Console.WriteLine($"\nYou have completed the {_name} Activity for {_duration} seconds.");
            ShowSpinner(3);
        }

        protected int GetDuration()
        {
            return _duration;
        }

        protected void ShowSpinner(int seconds)
        {
            char[] sequence = { '|', '/', '-', '\\' };
            DateTime end = DateTime.Now.AddSeconds(seconds);
            int index = 0;

            while (DateTime.Now < end)
            {
                Console.Write(sequence[index]);
                Thread.Sleep(200);
                Console.Write('\b');
                index = (index + 1) % sequence.Length;
            }
        }

        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }
    }

    // Breathing Activity
    class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base(
                "Breathing",
                "This activity will help you relax by walking you through breathing in and out slowly. " +
                "Clear your mind and focus on your breathing.")
        {
        }

        protected override void PerformActivity()
        {
            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            while (DateTime.Now < endTime)
            {
                Console.Write("\nBreathe in... ");
                ShowCountdown(4);

                if (DateTime.Now >= endTime) break;

                Console.Write("\nBreathe out... ");
                ShowCountdown(4);
            }
        }
    }

    // Reflection Activity
    class ReflectionActivity : Activity
    {
        private readonly List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private readonly List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private readonly Random _random = new Random();

        public ReflectionActivity()
            : base(
                "Reflection",
                "This activity will help you reflect on times in your life when you have shown strength and resilience. " +
                "This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
        }

        protected override void PerformActivity()
        {
            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            Console.WriteLine();
            Console.WriteLine("Consider the following prompt:");
            Console.WriteLine();

            string prompt = _prompts[_random.Next(_prompts.Count)];
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine("\nWhen you have something in mind, press Enter to continue.");
            Console.ReadLine();

            Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
            Console.WriteLine("You may begin in:");
            ShowCountdown(5);

            Console.Clear();
            Console.WriteLine($"Prompt: {prompt}\n");

            while (DateTime.Now < endTime)
            {
                string question = _questions[_random.Next(_questions.Count)];
                Console.WriteLine($"> {question}");
                ShowSpinner(6);
                Console.WriteLine();
            }
        }
    }

    // Listing Activity
    class ListingActivity : Activity
    {
        private readonly List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private readonly Random _random = new Random();

        public ListingActivity()
            : base(
                "Listing",
                "This activity will help you reflect on the good things in your life by having you list " +
                "as many things as you can in a certain area.")
        {
        }

        protected override void PerformActivity()
        {
            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            Console.WriteLine();
            string prompt = _prompts[_random.Next(_prompts.Count)];
            Console.WriteLine("List as many responses as you can to the following prompt:");
            Console.WriteLine();
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine("\nYou may begin in:");
            ShowCountdown(5);

            Console.WriteLine("\nStart listing items. Press Enter after each one.");
            int count = 0;

            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                // Use a timed check: if time expires mid-input, we still accept that last one
                if (Console.KeyAvailable)
                {
                    // Not strictly needed, but keeps input responsive
                }

                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    count++;
                }

                if (DateTime.Now >= endTime)
                {
                    break;
                }
            }

            Console.WriteLine($"\nYou listed {count} items!");
        }
    }

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
                Console.WriteLine("4. Quit");
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