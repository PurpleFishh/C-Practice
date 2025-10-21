namespace P2_Note.Grader;

public class LectureGrades : IGrader
{
    private readonly Dictionary<string, ILecture> _lectures = new();

    public void AddGrade(string lecture, double grade)
    {
        if (!_lectures.ContainsKey(lecture))
            _lectures[lecture] = new Lecture(lecture);
        _lectures[lecture].AddGrade(grade);
    }

    public List<double> GetGrades(string lectureName)
    {
        return _lectures.TryGetValue(lectureName, out var lecture) ? lecture.GetGrades() : [];
    }

    public override string ToString()
    {
        if (_lectures.Count == 0)
            return "No lectures recorded...";

        return string.Join('\n', _lectures.Values);
    }
}