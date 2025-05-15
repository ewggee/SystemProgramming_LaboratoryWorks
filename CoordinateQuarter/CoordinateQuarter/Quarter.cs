namespace CoordinateQuarter;

class Quarter
{
    public int Number { get; }
    public int PointsCount { get; private set; }
    public (int x, int y) MinPoint { get; private set; }
    public int MinR { get; private set; } = int.MaxValue;

    public Quarter(int number)
    {
        Number = number;
    }

    public void AddPoint(int x, int y)
    {
        PointsCount++;
        var r = Math.Min(Math.Abs(x), Math.Abs(y));

        if (r < MinR || (r == MinR && MinPoint.x == 0 && MinPoint.y == 0))
        {
            MinR = r;
            MinPoint = (x, y);
        }
    }
}
