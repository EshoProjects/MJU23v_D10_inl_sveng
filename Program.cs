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
                        // FIXME: No error handling if the specified file does not exist
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
                    else if (argument.Length == 1)
                    {
                        // FIXME: No error handling if the default file does not exist
                        using (StreamReader sr = new StreamReader(defaultFile))
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
                        // FIXME: No input validation, assumes valid input
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(s, e));
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        int index = -1;

                        // FIXME: No error handling if the specified words are not found
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        int index = -1;

                        // FIXME: No error handling if the specified words are not found
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == s && gloss.word_eng == e)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                else if (command == "translate")
                {
                    
                   if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string s = Console.ReadLine().ToLower();  // Convert to lowercase for case-insensitive comparison

                        foreach (SweEngGloss gloss in dictionary)
                        {
                            // Use StringComparison.OrdinalIgnoreCase for case-insensitive comparison
                            if (gloss.word_swe.Equals(s, StringComparison.OrdinalIgnoreCase))
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");

                            if (gloss.word_eng.Equals(s, StringComparison.OrdinalIgnoreCase))
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }

                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            } while (true);
        }
    }
}
