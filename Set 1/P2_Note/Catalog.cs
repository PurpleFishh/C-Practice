using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_Note
{
    public class Catalog
    {
        private bool _running = true;
        private Dictionary<string, List<double>> lectureGrades = new();

        public void Start()
        {
            Console.WriteLine("Enter the grade, when done enter -");
            while (_running)
            {
                Console.Write("Enter lecture: ");
                var lecture = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(lecture))
                {
                    Console.WriteLine("Please enter a valid lecture name!");
                    continue;
                }
                if (lecture == "-")
                {
                    GradesDisplay(lectureGrades);
                    _running = false;
                    continue;
                }

                if (!lectureGrades.ContainsKey(lecture))
                    lectureGrades[lecture] = new List<double>();

                Console.WriteLine($"Enter grades for lecture {lecture} (- to stop)");
                bool gradesRecording = true;
                while (gradesRecording)
                {
                    Console.Write($"Grade {lectureGrades[lecture].Count + 1}: ");
                    var input = Console.ReadLine() ?? "";
                    if (input == "-")
                        gradesRecording = false;

                    var grade = ValidateGrade(input);
                    if(grade.HasValue)
                        lectureGrades[lecture].Add(grade.Value);
                }
            }
        }

        private double? ValidateGrade(string input)
        {
            if (!double.TryParse(input, NumberStyles.Float, CultureInfo.CurrentCulture, out var grade))
            {
                Console.WriteLine("Enter a valid grade");
                return null;
            }
            if (grade < 0 || grade > 100)
            {
                Console.WriteLine("Please enter a grade between 0.0 and 100.0");
                return null;
            }
            return grade;
        }

        private void GradesDisplay(Dictionary<string, List<double>> lectureGrades)
        {
            if (lectureGrades.Count == 0)
                Console.WriteLine("No lecture recorded...");
            foreach (var grades in lectureGrades)
            {
                var finalGrade = grades.Value.Sum(x => x) / grades.Value.Count;
                Console.WriteLine($"Final grade for {grades.Key}: {finalGrade}");
            }
        }
    }
}
