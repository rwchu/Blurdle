using System;
using System.Collections.Generic;

namespace Blurdle
{
    class Blurdle
    {

        const string letters = "abcdefghijklmnopqrstuvwxyz";

        static bool IsOnlyLetters(string input)
        {
            foreach (char c in input)
            {
                if (!Char.IsLetter(c))
                    return false;
            }

            return true;
        }

        static void PrintLetterUsage(int[] letter_usage)
        {
            for(int i = 0; i < 26; i++)
            {
                if (letter_usage[i] == 0)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                else if (letter_usage[i] == 1)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (letter_usage[i] == 2)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.Write(letters[i].ToString() + ' ');
               
            }
            Console.WriteLine();
        }
        
        static void Main(string[] args)
        {
            int round_counter = 1;
            int[] letter_usage = new int[26]; // 0 = not used yet, 1 = not in word, 2 = in word, wrong position, 3 = in word, right position
            string[] words = Properties.Resources.words_5_letters.Split(Environment.NewLine); // faster access O(1)
            HashSet<string> words_set = new HashSet<string>(words); // faster lookup O(1)
            string word, guess;

            Random rnd = new Random();

            Console.WriteLine("Welcome to Blurdle");
            Console.WriteLine("- Words are 5 letters long");
            Console.WriteLine("- Words do not contain duplicate letters");

            while (true) {
                // Select word and reset variables
                word = words[rnd.Next(1, words.Length)];
                round_counter = 0;
                Array.Clear(letter_usage, 0, letter_usage.Length);

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nEnter your guess: ");
                    guess = Console.ReadLine().Trim().ToLower();
                    if (String.IsNullOrEmpty(guess) || !IsOnlyLetters(guess))
                    {
                        Console.WriteLine("Your guess contains non-letters. Try again.");
                    }
                    else if (guess.Length != 5)
                    {
                        Console.WriteLine("Your guess is not 5 letters long. Try again.");
                    }
                    else if (!words_set.Contains(guess)) 
                    {
                        Console.WriteLine("Your guess was not found in the word database.");
                    }
                    else if (!String.Equals(guess, word))
                    {
                        round_counter++;
                        for (int i = 0; i < guess.Length; i++)
                        {
                            if (guess[i] == word[i])
                            {
                                // Right position
                                Console.ForegroundColor = ConsoleColor.Green;
                                letter_usage[letters.IndexOf(guess[i])] = 3;
                            }
                            else if (word.Contains(guess[i]))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                letter_usage[letters.IndexOf(guess[i])] = 2;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                letter_usage[letters.IndexOf(guess[i])] = 1;
                            }
                            Console.Write(guess[i]);
                        }
                        Console.WriteLine();
                    } 
                    else
                    {
                        round_counter++; 
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(word);
                        Console.WriteLine("CORRECT! Number of guesses: " + round_counter.ToString());
                        Console.WriteLine("Press any key to play again.");
                        Console.ReadKey();
                        break;
                    }

                    PrintLetterUsage(letter_usage);
                }
            }

        }
    }
}
