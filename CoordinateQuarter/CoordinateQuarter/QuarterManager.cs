namespace CoordinateQuarter;

class QuarterManager
{
    private readonly Quarter[] _quarters;

    public QuarterManager()
    {
        _quarters = new Quarter[4];
        for (int i = 0; i < 4; i++)
            _quarters[i] = new Quarter(i + 1);
    }

    public void ProcessPoint(int x, int y)
    {
        var quarterIndex = GetQuarterIndex(x, y);
        _quarters[quarterIndex].AddPoint(x, y);
    }

    private int GetQuarterIndex(int x, int y)
    {
        if (x > 0 && y > 0) return 0; // 1 четверть
        if (x < 0 && y > 0) return 1; // 2 четверть
        if (x < 0 && y < 0) return 2; // 3 четверть
        return 3; // 4 четверть
    }

    public Quarter GetBestQuarter()
    {
        Quarter best = _quarters[0];

        for (int i = 1; i < _quarters.Length; i++)
        {
            var current = _quarters[i];

            if (current.PointsCount > best.PointsCount)
            {
                best = current;
            }
            else if (current.PointsCount == best.PointsCount)
            {
                if (current.MinR < best.MinR ||
                    (current.MinR == best.MinR && current.Number < best.Number))
                {
                    best = current;
                }
            }
        }

        return best;
    }
}
