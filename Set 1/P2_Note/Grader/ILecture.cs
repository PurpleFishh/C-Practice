namespace P2_Note.Grader;

public interface ILecture
{
    void AddGrade(double grade);
    List<double> GetGrades();
    string ToString();
}