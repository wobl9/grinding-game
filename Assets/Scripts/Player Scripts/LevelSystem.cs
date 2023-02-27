using UnityEngine;
using System;

public class LevelSystem
{
    public LevelSystem() { }

    public event EventHandler OnLevelUp;

    private const int NEXT_LEVEL_EXPIRIENCE_MULTIPLIER = 110;
    private int maxLevel = 100;
    private int currentExpirience = 0;
    private int currentLevel = 1;

    public void GainExpirience(int expirience)
    {
        currentExpirience += expirience;
        Debug.Log($"exp gained {expirience}");
        if (currentExpirience > (currentLevel * NEXT_LEVEL_EXPIRIENCE_MULTIPLIER))
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        SetCurrentLevel(GetCurrentLevel() + 1);
        Debug.Log($"level up. current level is {GetCurrentLevel()}");
        OnLevelUp?.Invoke(this, EventArgs.Empty);
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
