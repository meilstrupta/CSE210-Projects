using System;

public class Entry
{
    private string _date;
    private string _prompt;
    private string _response;

    public string Date
    {
        get { return _date; }
        set { _date = value; }
    }

    public string Prompt
    {
        get { return _prompt; }
        set { _prompt = value; }
    }

    public string Response
    {
        get { return _response; }
        set { _response = value; }
    }

    public Entry(string date, string prompt, string response)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
    }

    public void Display()
    {
        Console.WriteLine($"Date: {Date}");
        Console.WriteLine($"Prompt: {Prompt}");
        Console.WriteLine($"Response: {Response}");
        Console.WriteLine();
    }

    public string ToFileFormat()
    {
        return $"{Date}|{Prompt}|{Response}";
    }

    public static Entry FromFileFormat(string line)
    {
        string[] parts = line.Split('|');
        return new Entry(parts[0], parts[1], parts[2]);
    }
}