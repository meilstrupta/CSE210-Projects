using System;

class Program
{
    static void Main(string[] args)
    {
        WelcomeMessage();

        string userName = UserName();
        int userNumber = UserNumber();

        int squaredNumber = SquareNumber(userNumber);

        int birthYear;
        UserBirthYear(out birthYear);


        Result(userName, squaredNumber, birthYear);
    }

    static void WelcomeMessage()
    {
        Console.WriteLine("Welcome to the program!");
    }

    static string UserName()
    {
        Console.Write("What is your name: ");
        string name = Console.ReadLine();

        return name;
    }

    static int UserNumber()
    {
        Console.WriteLine("Please enter your favorite number: ");
        int number = int.Parse(Console.ReadLine());

        return number;
    }
    
    static void UserBirthYear(out int birthYear)
    {
        Console.WriteLine($"Please enter the year you were born: ");
        birthYear = int.Parse(Console.ReadLine());

    }

    static int SquareNumber(int number)
    {
        int square = number * number;
        return square;
    }

    static void Result(string name, int square, int birthYear)
    {
        Console.WriteLine($"{name}, the square of your number is {square}.");
        Console.WriteLine($"{name}, you will turn {2025 - birthYear} years old this year.");
    }
}