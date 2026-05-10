using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;

class Statistics
{
    public int Words;
    public int Lines;
    public int Punctuation;
}

class Program
{
    static Statistics stats = new Statistics();

    static char[] punctuationMarks = new char[]
    {
        '.', ',', ';', ':', '–', '—', '‒', '…',
        '!', '?', '"', '\'',
        '«', '»',
        '(', ')',
        '{', '}',
        '[', ']',
        '<', '>',
        '/'
    };

    static void AnalyzeFile(string filePath)
    {
        string content = File.ReadAllText(filePath);

        int words = CountWords(content);
        int lines = CountLines(content);
        int punctuation = CountPunctuation(content);

        Interlocked.Add(ref stats.Words, words);
        Interlocked.Add(ref stats.Lines, lines);
        Interlocked.Add(ref stats.Punctuation, punctuation);

        Console.WriteLine($"Analyzed: {Path.GetFileName(filePath)}");
    }

    static int CountWords(string content)
    {
        string[] words = content.Split(
            new char[] { ' ', '\n', '\r', '\t' },
            StringSplitOptions.RemoveEmptyEntries
        );

        return words.Length;
    }

    static int CountLines(string content)
    {
        string[] lines = content.Split(
            new char[] { '\n' },
            StringSplitOptions.RemoveEmptyEntries
        );

        return lines.Length;
    }

    static int CountPunctuation(string content)
    {
        int count = 0;

        foreach (char c in content)
        {
            if (Array.Exists(punctuationMarks, p => p == c))
            {
                count++;
            }
        }

        return count;
    }

    static void Main()
    {
        Console.WriteLine("Enter directory path:");

        string directoryPath = Console.ReadLine();

        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Directory does not exist.");
            return;
        }

        string[] files = Directory.GetFiles(directoryPath, "*.txt");

        if (files.Length == 0)
        {
            Console.WriteLine("No text files found.");
            return;
        }

        List<Thread> threads = new List<Thread>();

        foreach (string file in files)
        {
            Thread thread = new Thread(() => AnalyzeFile(file));

            threads.Add(thread);

            thread.Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine();
        Console.WriteLine("===== TOTAL STATISTICS =====");
        Console.WriteLine($"Total words: {stats.Words}");
        Console.WriteLine($"Total lines: {stats.Lines}");
        Console.WriteLine($"Total punctuation marks: {stats.Punctuation}");
    }
}

// \i guess bro 💔
