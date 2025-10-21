using P2_Note.Grader;

namespace P2_Note;

public class CatalogController
{
    private readonly IGrader _lectureGrades = new LectureGrades();

    public bool TryAddGrade(string lecture, string gradeInput, out string message)
    {
        if (string.IsNullOrWhiteSpace(lecture))
        {
            message = "Lecture name is required.";
            return false;
        }

        if (!double.TryParse(gradeInput, out var grade))
        {
            message = "Enter a valid grade.";
            return false;
        }

        try
        {
            _lectureGrades.AddGrade(lecture, grade);
            message = $"Added grade {grade} to lecture '{lecture}'.";
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }

    public IReadOnlyList<double> GetGrades(string lecture)
    {
        return _lectureGrades.GetGrades(lecture);
    }

    public string GetSummary()
    {
        return _lectureGrades.ToString();
    }
}