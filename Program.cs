using System;
using System.Linq;
using System.Collections.Generic;

/*
Take an input of a paragraph:
Give the number of palindrome words
Give the no of palindrome sentences
List the unique words of a paragraph with the count of word instance
Let the user also input a letter at some point and list all words containig that letter 
*/
namespace StringPalindrom
{
    class Program
    {
        static void Main(string[] args)
        {
            #region user input
            Console.WriteLine();
            Console.WriteLine("Please type a paragraph(followed by Enter key): ");
            Console.WriteLine("Example: \n Do Geese See God?");
            // Example: 
            //     Do Geese See God?
            var paragraph = Console.ReadLine();
            Console.WriteLine();
            #endregion

            #region computed variables
            var wordSeparators = new char[]{' ', '.', ',', '?'};
            var words = SplitBySeparators(paragraph, wordSeparators);
            var sentenceSeparators = new char[]{'.', '?'};
            var sentences = SplitBySeparators(paragraph, sentenceSeparators);
            #endregion

            Print("Palindrome Words", Palindromes(items: words));

            Print("Palindrome Senttences", Palindromes(items: sentences));
            
            PrintWordCount(words);

            PrintSearchResults(words);      

            Console.WriteLine("Press Enter key to exit..");
            Console.Read();      
        }

        #region Private Methods

        private static List<string> SplitBySeparators(string paragraph, char[] separators)
        {
            return paragraph.Split(separators)
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Select(w => w.Trim()).ToList();
        }

        private static List<string> Palindromes(List<string> items)
        {
            return items.Where(i => (i.Length > 1)
                && i.Replace(" ", "").ToLower().SequenceEqual(
                    i.Replace(" ", "").ToLower().Reverse()))
                .Distinct().ToList();
        }

        private static void Print(string text, List<string> textItems)
        {
            if (!textItems.Any())
            {
                Console.WriteLine($"No {text} found.");
            }
            else
            {
                Console.WriteLine($"No. of {text}: {textItems.Count()}");
                textItems.ForEach(item => 
                    {
                        if(text.Contains("Words")) 
                        {   
                            Console.Write($"{item} \t"); 
                        }
                        else Console.WriteLine($"{item}");
                    });
                Console.WriteLine();
                Console.WriteLine("-------------------------------");
                
            }
        }
        
        private static void  PrintWordCount(List<string> words)
        {
            var wordsWithCount = words.GroupBy(w => w).Select(g => new { Text = g.Key, Count = g.Count() }).ToList();
            Console.WriteLine("Unique word Count:");
            wordsWithCount.ForEach(w => Console.WriteLine($"{w.Text} : {w.Count}"));
            Console.WriteLine();
            Console.WriteLine("-------------------------------");
        }
        
    
        private static void  PrintSearchResults(List<string> words)
        {
            Console.Write("Enter a character to search words: ");
            var searchChar = Console.ReadKey().KeyChar;
            var wordsWithText = 
                words.Distinct()
                .Where(w => w.IndexOf(searchChar, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
            Console.WriteLine();
            Print($"Words with '{searchChar}'", wordsWithText);
            Console.WriteLine();
        }
        #endregion
    }
}