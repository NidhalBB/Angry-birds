using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World
{

    public World()
    {
        levels = new List<Level>();
        for (int i = 0; i < TotalLevels; i++)
        {
            levels.Add(new Level());
        }
    }


    private List<Level> levels;
    public List<Level> Levels { get { return levels; } set { levels = value; } }
    public int TotalLevels { get { return GameControl.NumLevels; } }
    public int TotalDefeated
    {
        get
        {
            int count = 0;
            for (int i = 0; i < TotalLevels; i++)
            {
                if (levels[i].Defeated)
                    count++;
            }
            return count;
        }
    }
    public int TotalStars
    {
        get
        {
            int count = 0;
            for (int i = 0; i < TotalLevels; i++)
            {
                count += levels[i].HighStarScore;
            }
            return count;
        }
    }
    public int TotalScore
    {
        get
        {
            int count = 0;
            for (int i = 0; i < TotalLevels; i++)
            {
                count += levels[i].Highscore;
            }
            return count;
        }
    }

}
