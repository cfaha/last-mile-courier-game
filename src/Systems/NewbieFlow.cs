using UnityEngine;

public class NewbieFlow : MonoBehaviour
{
    public TutorialSteps Steps;
    public TutorialScript Script;

    public void StartFlow(int levelId)
    {
        if (levelId == 1)
        {
            Steps?.StartSteps();
        }
        else
        {
            Script?.ShowForLevel(levelId);
        }
    }
}
