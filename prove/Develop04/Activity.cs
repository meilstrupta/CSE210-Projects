using System;
using System.Threading;

namespace MindfulnessApp
{
    abstract class Activity
    {
        private string _name;
        private string _description;
        private int _duration;

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public void Run()
        {
            ShowStartingMessage();
            PerformActivity();

            StatsTracker.RecordSession(_name, _duration);

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
}