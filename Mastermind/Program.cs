using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {

            StartGame();
            
        }

        private static void StartGame()
        {
            //Generates the random list of numbers to match against
            int[] correctNumbers = GetRandomNumbers();


            //foreach (var item in correctNumbers)
            //Console.Write("{0}", item);

            Console.WriteLine("\nWelcome to Mastermind!");

            Console.WriteLine("Please place your first guess of four numbers between 1 and 6");

            bool gameWon = false;
            List<char> checkList = new List<char>();
            int attempts = 0;
            while (gameWon == false)
            {
                if (attempts > 0)
                {
                    Console.WriteLine("\nGuess again");
                }
                checkList.Clear();
                if (attempts == 10)
                {
                    Console.WriteLine("\nYou Lose!");
                    Console.WriteLine("\nCorrect Numbers: ");
                    foreach (var item in correctNumbers)
                        Console.Write("{0}", item);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                int[] result = ValidateInput();


                attempts++;
                Console.WriteLine("Attempts: " + attempts);

                int index = 0;
                foreach (int n in result)
                {

                    if (correctNumbers.Contains(n))
                    {
                        var indexes = correctNumbers.Select((value, index) => new { value, index })
                            .Where(x => x.value == n)
                            .Select(x => x.index)
                            .ToList();

                        if (indexes.Contains(index))
                        {
                            checkList.Add('+');
                        }
                        else
                        {
                            checkList.Add('-');
                        }
                    }
                    else
                    {
                        checkList.Add('x');
                    }
                    index++;
                }

                CheckWon(checkList);
                

                Random rnd = new Random();
                var shuffledList = checkList.OrderBy(x => (rnd.Next()));

                foreach (var item in shuffledList)
                    Console.Write("{0}", item);

            }
        }

        private static void CheckWon(List<char> checkList)
        {
            int exactMatchCount = checkList.Count(i => i == '+');
                if (exactMatchCount == 4)
                {
                    Console.WriteLine("YOU WON!!");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
        }

        private static int[] GetRandomNumbers()
        {
            List<int> answer = new List<int>();
            Random num = new Random();
            for (int i = 0; i < 4; i++)
            {
                int rand = num.Next(1, 7);
                answer.Add(rand);
            }
            int[] randArray = answer.ToArray();
            

            return randArray;
        }

        private static int[] ValidateInput()
        {
            while (true)
            {
                int result;
                
                string input = Console.ReadLine();
                if (!Int32.TryParse(input, out result))
                {
                    Console.WriteLine("Please enter only numbers");
                }
                else if (input.Length != 4)
                {
                    Console.WriteLine("Please enter four numbers");

                }

                else
                {
                    int[] newResult = result.ToString().Select(c => Convert.ToInt32(c.ToString())).ToArray();
                    bool isInRange = true;
                    foreach (int n in newResult)
                    {
                        if(n > 6 || n <1)
                        {
                            isInRange = false;
                        }
                    }
                    if(isInRange == false)
                    {
                        Console.WriteLine("Numbers must be between 1 and 6");
                    }
                    else
                    {
                        return newResult;
                    }
                }
                

            }
        }
    }
}
