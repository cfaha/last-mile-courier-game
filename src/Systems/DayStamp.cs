using System;

public class DayStamp
{
    public static int Today()
    {
        var now = DateTime.UtcNow.Date;
        return now.Year * 1000 + now.DayOfYear;
    }
}
