namespace P2_Note.Grader;

public interface IGrader
{
    void AddGrade(string lecture, double grade);
    List<double> GetGrades(string lectureName);
    string ToString();
}