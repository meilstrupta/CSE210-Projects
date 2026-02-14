using System;
using System.Collections.Generic;
using System.Linq;

// Scripture memorizer Program 
// This program allows the user to input a scripture reference and verse, and then hides words in the verse one by one until the entire verse is hidden. 
// Program Requirements: 
// 1. Store a scripture, including both the reference (for example "John 3:16") and the text of the scripture. 
// 2. Accommodate scriptures with multiple verses, such as "Proverbs 3:5-6". 
// 3. Clear the console screen and display the complete scripture, including the reference and the text. 
// 4. Prompt the user to press the enter key or type quit. 
// 5. If the user types quit, the program should end. 
// // 6. If the user presses the enter key (without typing quit), the program should hide a few random words in the scripture, clear the console screen, and display the scripture again. 
// // 7. The program should continue prompting the user and hiding more words until all words in the scripture are hidden. 
// // 8. When all words in the scripture are hidden, the program should end. 
// // 9. As a stretch challenge, try to randomly select from only those words that are not already hidden.


// =====================
// Word Class
// =====================
public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }
}

// =====================
// Reference Class
// =====================
public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    // Single verse constructor
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse;
    }

    // Verse range constructor
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (_startVerse == _endVerse)
            return $"{_book} {_chapter}:{_startVerse}";
        else
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
    }
}

// =====================
// Scripture Class
// =====================
public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(" ")
                     .Select(word => new Word(word))
                     .ToList();
    }

    public void HideRandomWords(int count)
    {
        var visibleWords = _words.Where(w => !w.IsHidden()).ToList();

        count = Math.Min(count, visibleWords.Count);

        for (int i = 0; i < count; i++)
        {
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public bool AllWordsHidden()
    {
        return _words.All(w => w.IsHidden());
    }

    public string GetDisplayText()
    {
        string referenceText = _reference.GetDisplayText();
        string wordsText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{referenceText}\n{wordsText}";
    }
}

// =====================
// Program Class
// =====================
class Program
{
    static void Main()
    {
        // Scripture library
        var scriptures = new List<Scripture>
        {
            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."
            ),

            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life."
            ),

            new Scripture(
                new Reference("Moses", 7, 28),
                "And it came to pass that the God of heaven looked upon the residue of the people, and he wept; and Enoch bore record of it, saying: How is it that the heavens weep, and shed forth their tears as the rain upon the mountains?"
            )
        };

        // Scripture selection menu
        Console.WriteLine("Choose a scripture to memorize:\n");

        for (int i = 0; i < scriptures.Count; i++)
        {
            string referenceOnly = scriptures[i].GetDisplayText().Split('\n')[0];
            Console.WriteLine($"{i + 1}. {referenceOnly}");
        }

        Console.Write("\nEnter number: ");
        int choice = int.Parse(Console.ReadLine()) - 1;

        Scripture scripture = scriptures[choice];

        // Memorization loop
        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress ENTER to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords(3);

            if (scripture.AllWordsHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nAll words hidden. Program ending.");
                break;
            }
        }
    }
}