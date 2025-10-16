namespace P9_Schimb_Valutar;

public class Table(int tblPadding)
{
    public int TblPadding { get; } = tblPadding;

    public void PrintCell(object value) => Console.Write("|" + $"{value}".PadLeft(TblPadding));
    public void EndLine() => Console.WriteLine("|");
}