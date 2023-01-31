using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager: MonoBehaviour
{

    private const int NEXT_LEVEL_EXPIRIENCE_MULTIPLIER = 110;
    private int maxLevel = 100;
    private int currentExpirience = 0;
    private int currentLevel = 1;
    protected Dictionary<int, int> expirienceForLevel = new();

    public void OnExpirienceGained(int expirience)
    {
        currentExpirience += expirience;
        Debug.Log($"current exp is {currentExpirience}");
        if (currentExpirience > (currentLevel * NEXT_LEVEL_EXPIRIENCE_MULTIPLIER))
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        SetCurrentLevel(GetCurrentLevel() + 1);
        Debug.Log($"Level up! current level is {currentLevel}");
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void IncreaseMaxLevel(int value)
    {
        maxLevel += value;
    }

    private void SetCurrentLevel(int level)
    {
        if (level > maxLevel)
        {
            currentLevel = maxLevel;
        }
        else
        {
            currentLevel = level;
        }
    }
}
