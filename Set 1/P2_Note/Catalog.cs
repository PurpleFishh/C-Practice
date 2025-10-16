using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2_Note.Grader;

namespace P2_Note
{
    public class Catalog
    {
        private bool _running = true;
        private readonly IGrader _lectureGrades = new LectureGrades();

        public void Start()
        {
            Console.WriteLine("Enter the grade, when done enter -");
            while (_running)
            {
                if (!TakeInput("lecture", out var lecture))
                    continue;

                if (lecture == "-")
                {
                    Console.WriteLine(_lectureGrades);
                    _running = false;
                    continue;
                }

                Console.WriteLine($"Enter grades for lecture {lecture} (- to stop)");
                while (true)
                {
                    var gradesCount = _lectureGrades.GetGrades(lecture).Count;
                    if (!TakeInput($"grade {gradesCount + 1}", out var input))
                        continue;
                    if (input == "-")
                        break;

                    if (!ValidateGrade(input, out var grade)) continue;
                    try
                    {
                        _lectureGrades.AddGrade(lecture, grade);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private bool ValidateGrade(string input, out double grade)
        {
            if (double.TryParse(input, out grade)) return true;

            Console.WriteLine("Enter a valid grade");
            return false;
        }

        private bool TakeInput(string param, out string input)
        {
            Console.WriteLine($"Enter a {param}: ");
            var userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine($"Please enter a valid {param}!");
                input = string.Empty;
                return false;
            }

            input = userInput;
            return true;
        }
    }
}