using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

    public const int NumWorlds = 3;
    public const int NumLevels = 20;
    public List<World> AllWorlds;
    public World CurrWorld
    {
        set { currWorld = value; Debug.Log("Current world set."); }
        get
        {
            if (currWorld != null)
                return currWorld;
            else
            {
                Debug.Log("Can't retrieve current world. Value is null.");
                return null;
            }
        }
    }
    public Level CurrLevel
    {
        set { currLevel = value; Debug.Log("Current level set."); }
        get
        {
            if (currLevel != null)
                return currLevel;
            else
            {
                Debug.Log("Cannot retrieve currLevel, value is null.");
                return null;
            }
        }
    }
    public Game CurrentGame;

    [SerializeField]
    Game gamePrefab;

    private Level currLevel;
    private World currWorld;
	// Use this for initialization
	void Start ()
    {
        AllWorlds = new List<World>();
        for (int i = 0; i < NumWorlds; i++)
        {
            AllWorlds.Add(new World());
        }

	}

    // Update is called once per frame
    void Update()
    {
        if (CurrentGame != null)
        {
            CheckForCurrentGameOver();
        }
	}

    void CheckForCurrentGameOver()
    {
        if (CurrentGame.currLevelObj == null && CurrentGame.gameOver)
        {
            Debug.Log("Game Over!");
        }
    }
    public void StartGame(int level, int world)
    {
        CurrentGame = Instantiate(gamePrefab) as Game;
        CurrentGame.gameObject.name = "Game";

        if (world == 1)
            level += 20;
        else if (world == 2)
            level += 40;

        CurrentGame.InitLevel(level, 3);
    }
}
