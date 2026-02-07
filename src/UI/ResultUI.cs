using UnityEngine;

public class ResultUI : MonoBehaviour
{
    public void ShowResult(float score, int coins)
    {
        string rating = RatingHelper.GetRating(score);
        Debug.Log($"Result score={score:F2} rating={rating} coins={coins}");
        // TODO: show score and rewards
    }
}
