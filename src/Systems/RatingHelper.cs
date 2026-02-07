public class RatingHelper
{
    public static string GetRating(float score)
    {
        if (score >= 0.9f) return "S";
        if (score >= 0.8f) return "A";
        if (score >= 0.7f) return "B";
        return "C";
    }
}
