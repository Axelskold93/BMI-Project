using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BMI_Project
{
    public class Person
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public List<double> BMIS = new List<double>();
        public List<Person> people = new List<Person>();
        public void SetPersonValues()
        {
            Console.Clear();
            Console.WriteLine("Please enter name:");
            Name = Console.ReadLine();
            Console.WriteLine("Please enter weight:");
            Weight = double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter height:");
            Height = double.Parse(Console.ReadLine());
            people.Add(new Person { Name = Name, Weight = Weight, Height = Height});
        }
        public void CalculateBMI()
        {
            Console.Clear();
            double currentBMI = Weight / (Height * Height);
            Console.WriteLine($"Current BMI: {currentBMI:N1}.");
            BMIS.Add(currentBMI);
            Console.ReadKey();
        }
        public void ShowBMI()
        {
            Console.Clear();
            Console.WriteLine($"All registred BMI values:");
            foreach (double currentBMI in BMIS)
            {               
                Console.Write($"{currentBMI:N1}\n");
            }
            Console.ReadKey();
        }
        public void ClearList()
        {
            BMIS.Clear();
        }
    }
    public class Program
    {
        
      
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            Person person = new Person();
            Console.WriteLine("Welcome!");
            bool running = true;
            while (running)
            {

                Console.Clear();
                int option = ShowMenu("Please choose:", new[]
                {
                "Set weight and height",
                "Calculate BMI",
                "Show previous BMI values",
                "Remove all BMI values",
                "Exit"
                });
                if (option == 0)
                {
                    person.SetPersonValues();
                            
                }
                else if (option == 1)
                {
                    person.CalculateBMI();
                }
                else if (option == 2)
                {
                    person.ShowBMI();
                }
                else if (option == 3)
                {
                    person.ClearList();
                }
                else if (option == 4)
                {
                    Console.WriteLine("Thank you for using BMI Tracker!");
                    Environment.Exit(0);
                }
            }
        }
        #region
        public static int ShowMenu(string prompt, IEnumerable<string> options)
        {
            if (options == null || options.Count() == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty list of options.");
            }

            Console.WriteLine(prompt);

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            // Calculate the width of the widest option so we can make them all the same width later.
            int width = options.MaxBy(option => option.Length).Length;

            int selected = 0;
            int top = Console.CursorTop;
            for (int i = 0; i < options.Count(); i++)
            {
                // Start by highlighting the first option.
                if (i == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var option = options.ElementAt(i);
                // Pad every option to make them the same width, so the highlight is equally wide everywhere.
                Console.WriteLine("- " + option.PadRight(width));

                Console.ResetColor();
            }
            Console.CursorLeft = 0;
            Console.CursorTop = top - 1;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(intercept: true).Key;

                // First restore the previously selected option so it's not highlighted anymore.
                Console.CursorTop = top + selected;
                string oldOption = options.ElementAt(selected);
                Console.Write("- " + oldOption.PadRight(width));
                Console.CursorLeft = 0;
                Console.ResetColor();

                // Then find the new selected option.
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Count() - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }

                // Finally highlight the new selected option.
                Console.CursorTop = top + selected;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                string newOption = options.ElementAt(selected);
                Console.Write("- " + newOption.PadRight(width));
                Console.CursorLeft = 0;
                // Place the cursor one step above the new selected option so that we can scroll and also see the option above.
                Console.CursorTop = top + selected - 1;
                Console.ResetColor();
            }

            // Afterwards, place the cursor below the menu so we can see whatever comes next.
            Console.CursorTop = top + options.Count();

            // Show the cursor again and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }
        #endregion
    }

    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ExampleTest()
        {
            using FakeConsole console = new FakeConsole("First input", "Second input");
            Program.Main();
            Assert.AreEqual("Hello!", console.Output);
        }
    }
}
