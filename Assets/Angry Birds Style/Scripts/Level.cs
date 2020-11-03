using UnityEngine;
using System.Collections;

public class Level
{
    public Level()
    {
        highscore = 0;
        currentscore = 0;
        unlocked = false;
        OneStarReq = 10000;
        TwoStarReq = 20000;
        ThreeStarReq = 30000;
    }

    public int OneStarReq, TwoStarReq, ThreeStarReq;
    public bool Defeated
    {
        get
        {
            if (highscore > OneStarReq)
                return true;
            return false;
        }
    }
    public bool CurrentDefeated
    {
        get
        {
            if (currentscore >= OneStarReq)
                return true;
            return false;
        }
    }
    public int CurrStarScore
    {
        get
        {
            if (currentscore >= ThreeStarReq)
                return 3;
            if (currentscore >= TwoStarReq)
                return 2;
            if (currentscore >= OneStarReq)
                return 1;
            return 0;
        }
    }
    public int HighStarScore
    {
        get
        {
            if (highscore >= ThreeStarReq)
                return 3;
            if (highscore >= TwoStarReq)
                return 2;
            if (highscore >= OneStarReq)
                return 1;
            return 0;
        }
    }


    private int highscore, currentscore;
    private bool unlocked;

    public int Currentscore
    {
        get { return currentscore; }
        set { currentscore = value; }
    }

    public int Highscore
    {
        get { return highscore; }
        set { highscore = value; }
    }

    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }

}
