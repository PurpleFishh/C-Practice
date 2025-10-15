namespace P6_Inmultire
{
    public class Table
    {
        public int TblPadding { get; }

        public Table(int tblPadding)
        {
            this.TblPadding = tblPadding;
        }

        public void PrintCell(object value) => Console.Write("|" + $"{value}".PadLeft(TblPadding));
        public void EndLine() => Console.WriteLine("|");
    }
}