using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> allnumbers = new List<int>();
        
        int Number = -1;
        while (Number != 0)
        {
            Console.Write("Enter a list of numbers: (0 will close the program): ");
            
            string Response = Console.ReadLine();
            Number = int.Parse(Response);
            
            if (Number != 0)
            {
                allnumbers.Add(Number);
            }
        }


        int sum = 0;
        foreach (int newnumber in allnumbers)
        {
            sum += newnumber;
        }

        Console.WriteLine($"The sum is: {sum}");

float average = ((float)sum) / allnumbers.Count;
        Console.WriteLine($"The average is: {average}");


        int max = allnumbers[0];

        foreach (int newnumber in allnumbers)
        {
            if (newnumber > max)
            {
                max = newnumber;
            }
        }

        Console.WriteLine($"The max is: {max}");
    }
}