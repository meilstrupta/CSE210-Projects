using System;

class Program
{
    static void Main(string[] args)
    {

        Random randomGenerator = new Random();
        int Number = randomGenerator.Next(1, 101);

        int guess = -1;
        Console.WriteLine("Please guess a number 1 - 100!");

        while (guess != Number)
        {
            Console.WriteLine("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            if (Number > guess)
            {
                Console.WriteLine("Higher");
            }
            else if (Number < guess)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("Congrats! You got it!");
            }

        }                    
    }
}