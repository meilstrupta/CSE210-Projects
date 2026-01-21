using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade? ");
        string grade_input = Console.ReadLine();
        int percentage = int.Parse(grade_input);

        string Grade = "";

        if (percentage >= 90)
        {
            Grade = "A";
        }
        else if (percentage >= 80)
        {
            Grade = "B";
        }
        else if (percentage >= 70)
        {
            Grade = "C";
        }
        else if (percentage >= 60)
        {
            Grade = "D";
        }
        else
        {
            Grade = "F";
        }

        Console.WriteLine($"Your grade is: {Grade}");
        
        if (percentage >= 70)
        {
            Console.WriteLine("You passed!");
        }
        else
        {
            Console.WriteLine("Better luck next time!");
        }
    }
}