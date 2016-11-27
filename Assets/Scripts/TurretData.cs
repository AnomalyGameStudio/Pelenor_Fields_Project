using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretData : MonoBehaviour
{
    [System.Serializable]
    public class TurretLevel
    {
        public int cost;
        public GameObject visualization;
    }

    public List<TurretLevel> levels;

    private TurretLevel currentLevel;

    public TurretLevel CurrentLevel
    {
        get
        {
            return currentLevel;
        }

        set
        {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel);

            GameObject levelVisualization = levels[currentLevelIndex].visualization;
            for(int i = 0; i < levels.Count; i++)
            {
                if(levelVisualization != null)
                {
                    if (i== currentLevelIndex)
                    {
                        levels[i].visualization.SetActive(true);
                    }
                    else
                    {
                        levels[i].visualization.SetActive(false);
                    }
                }
            }
        }
    }
    
    void OnEnable()
    {
        CurrentLevel = levels[0];
    }


    public TurretLevel getNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;

        if(currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void increaseLevel()
    {
        // Gets the index of the Next Level
        // TODO: Change this to get from the UI input
        int currentLevelIndex = levels.IndexOf(currentLevel);

        // Check if there is a level to go up
        if(currentLevelIndex < levels.Count - 1)
        {
            // Gets the next level
            TurretLevel nextLevel = levels[currentLevelIndex + 1];

            // Sends the Negative cost to the ScoreManager. The routines returns True if there is enough money for the upgrade
            if(ScoreManager.Instance.addMoney( - nextLevel.cost))
            {
                CurrentLevel = nextLevel;
            }
        }
    }
}
