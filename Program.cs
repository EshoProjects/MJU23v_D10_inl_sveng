using System;
using System.Collections.Generic;
using System.IO;

namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;

        class SweEngGloss
        {
            public string word_swe, word_eng;

            // Constructor for initializing with provided Swedish and English words
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe;
                this.word_eng = word_eng;
            }

            // Constructor for initializing from a line in the file
            public SweEngGloss(string line)
            {
                // FIXME: Potential issue if the line does not contain '|' or if words[1] doesn't exist
                string[] words = line.Split('|');
                this.word_swe = words[0];
                this.word_eng = words[1];
            }
        }

        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");

            do
            {
                try
                {
                    Console.Write("> ");
                    string[] argument = Console.ReadLine().Split();
                    string command = argument[0];

                    if (command == "quit")
                    {
                        Console.WriteLine("Goodbye!");
                    }
                    else if (command == "load")
                    {
                        if (argument.Length == 2)
                        {
                            try
                            {
                                using (StreamReader sr = new StreamReader(argument[1]))
                                {
                                    dictionary = new List<SweEngGloss>(); // Empty it!
                                    string line = sr.ReadLine();
                                    while (line != null)
                                    {
                                        SweEngGloss gloss = new SweEngGloss(line);
                                        dictionary.Add(gloss);
                                        line = sr.ReadLine();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error loading file: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid 'load' command syntax");
                        }
                    }
                    else if (command == "list")
                    {
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                        }
                    }
                    else if (command == "new")
                    {
                        if (argument.Length == 3)
                        {
                            try
                            {
                                dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error adding new glossary entry: {ex.Message}");
                            }
                        }
                        else if (argument.Length == 1)
                        {
                            try
                            {
                                Console.WriteLine("Write word in Swedish: ");
                                string sa = Console.ReadLine();
                                Console.Write("Write word in English: ");
                                string ea = Console.ReadLine();
                                dictionary.Add(new SweEngGloss(sa, ea));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error adding new glossary entry: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid 'new' command syntax");
                        }
                    }
                    else if (command == "delete")
                    {
                        if (argument.Length == 1)
                        {
                            try
                            {
                                Console.WriteLine("Write word in Swedish: ");
                                string sa = Console.ReadLine();
                                Console.Write("Write word in English: ");
                                string ea = Console.ReadLine();
                                int index = -1;

                                for (int i = 0; i < dictionary.Count; i++)
                                {
                                    SweEngGloss gloss = dictionary[i];
                                    if (gloss.word_swe == sa && gloss.word_eng == ea)
                                        index = i;
                                }
                                dictionary.RemoveAt(index);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error deleting glossary entry: {ex.Message}");
                            }
                        }
                    }
                    else if (command == "translate")
                    {
                        if (argument.Length == 1)
                        {
                            try
                            {
                                Console.WriteLine("Write word to be translated: ");
                                string sa = Console.ReadLine().ToLower();  // Convert to lowercase for case-insensitive comparison

                                bool translationFound = false;

                                foreach (SweEngGloss gloss in dictionary)
                                {
                                    if (gloss.word_swe.Equals(sa, StringComparison.OrdinalIgnoreCase))
                                    {
                                        Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                                        translationFound = true;
                                    }

                                    if (gloss.word_eng.Equals(sa, StringComparison.OrdinalIgnoreCase))
                                    {
                                        Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                                        translationFound = true;
                                    }
                                }

                                if (!translationFound)
                                {
                                    Console.WriteLine("Translation not found.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error translating word: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Unknown command: '{command}'");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            } while (true);
        }
    }
}
