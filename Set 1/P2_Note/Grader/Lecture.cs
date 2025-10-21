namespace P2_Note.Grader;

public class Lecture(string lectureName) : ILecture
{
    public string LectureName { get; } = lectureName;
    private readonly List<double> _grades = [];

    public void AddGrade(double grade)
    {
        if (grade < 0 || grade > 100)
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 0 and 100.");
        _grades.Add(grade);
    }

    public List<double> GetGrades() => [.._grades];

    public override string ToString() =>
        _grades.Count == 0 ? "No grades for lecture..." : $"Final grade for {LectureName}: {_grades.Average()}";
}