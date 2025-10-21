using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2_Note.Grader;

namespace P2_Note;

public enum CatalogActions
{
    AddGrade,
    ShowSummary,
    Exit
}

public class Catalog
{
    private bool _running = true;
    private readonly CatalogController _controller = new();

    public void Start()
    {
        Console.WriteLine("Catalog started (a - add grades to lecture, m - show summary, - to exit)");
        while (_running)
        {
            if (!TakeInput("operation", out var operationInput))
                continue;
            try
            {
                var action = GetAction(operationInput);
                switch (action)
                {
                    case CatalogActions.ShowSummary:
                        Console.WriteLine(_controller.GetSummary());
                        continue;
                    case CatalogActions.AddGrade:
                        if (!TakeInput("lecture", out var lecture))
                            continue;
                        AddGrades(lecture);
                        continue;
                    case CatalogActions.Exit:
                        StopProgram();
                        continue;
                    default:
                        throw new InvalidOperationException("Invalid catalog action");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Console.WriteLine(_controller.GetSummary());
    }

    private void AddGrades(string lecture)
    {
        Console.WriteLine($"Enter grades for lecture '{lecture}' (enter '-' to stop adding grades)");
        while (true)
        {
            var nextIndex = _controller.GetGrades(lecture).Count + 1;
            if (!TakeInput($"grade {nextIndex}", out var input, false))
                break;
            
            if (!_controller.TryAddGrade(lecture, input, out var message))
            {
                Console.WriteLine($"ERROR: {message}");
                continue;
            }

            Console.WriteLine(message);
        }
    }

    private CatalogActions GetAction(string input)
    {
        return input.ToLower() switch
        {
            "a" => CatalogActions.AddGrade,
            "m" => CatalogActions.ShowSummary,
            "-" => CatalogActions.Exit,
            _ => throw new InvalidOperationException("Invalid catalog action.")
        };
    }


    private bool TakeInput(string variable, out string input, bool stopProgram = true)
    {
        if (!InputHelper.TakeInput(variable, out input))
            return false;

        if (CheckProgramEnd(input, stopProgram))
            return false;
        return true;
    }

    private bool CheckProgramEnd(string input, bool stopProgram)
    {
        if (input != "-") return false;
        if (stopProgram)
            StopProgram();
        return true;
    }

    private void StopProgram()
    {
        _running = false;
    }
}