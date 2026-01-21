using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello! What is your first name?");
        string first_name = Console.ReadLine();

        Console.WriteLine("What is your last name?");
        string last_name = Console.ReadLine();

        Console.WriteLine($"Hello, your name is {last_name}, {first_name} {last_name}!");
        Console.WriteLine("Thank You!");

    }
}