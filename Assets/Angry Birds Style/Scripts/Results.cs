using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Results : MonoBehaviour {

    Text levelClearedText;
    Text scoreText, highscoreText;
    Button reset, back, next;
    GameObject stars;
    GameObject failed;
	// Use this for initialization
	void Start () 
    {
        levelClearedText = transform.Find("Header").Find("LevelClearedText").GetComponent<Text>();
        scoreText = levelClearedText.transform.Find("InfoPanel").Find("ScoreText").GetComponent<Text>();
        highscoreText = scoreText.transform.Find("BestScoreText").GetComponent<Text>();
        stars = levelClearedText.transform.Find("InfoPanel").Find("Stars").gameObject;
        failed = levelClearedText.transform.Find("InfoPanel").Find("Failed").gameObject;

        reset = transform.Find("ButtonsPanel").Find("ResetButton").GetComponent<Button>();
        reset.onClick.AddListener(()=> FindObjectOfType<UIManager>().Clicked(reset));

        back = transform.Find("ButtonsPanel").Find("BackButton").GetComponent<Button>();
        back.onClick.AddListener(() => FindObjectOfType<UIManager>().Clicked(back));

        next = transform.Find("ButtonsPanel").Find("NextButton").GetComponent<Button>();
        next.onClick.AddListener(() => FindObjectOfType<UIManager>().Clicked(next));

	}

    public void UpdateResults(UIManager manager)
    {
        Level currlevel = manager.Control.CurrLevel;
        if(manager.Control.CurrentGame.numProjectiles > 0)
            currlevel.Currentscore += (manager.Control.CurrentGame.numProjectiles * 10000);

        if (currlevel.CurrentDefeated)
        {
            stars.SetActive(true);
            failed.SetActive(false);
            next.interactable = true;
            levelClearedText.text = "LEVEL CLEARED!";

            if (manager.LevelIndex == GameControl.NumLevels - 1)
            {
                levelClearedText.text = "WORLD DEFEATED!";
                next.interactable = false;
            }
            else
            {
                manager.Control.CurrWorld.Levels[manager.LevelIndex + 1].Unlocked = true;
            }

            scoreText.text = "SCORE: " + currlevel.Currentscore;
            if (currlevel.Highscore < currlevel.Currentscore)
            {
                highscoreText.text = "new highscore!";
                currlevel.Highscore = currlevel.Currentscore;
            }
            else if (currlevel.Highscore >= currlevel.Currentscore)
            {
                highscoreText.text = "best " + currlevel.Highscore;
            }

            for (int i = 0; i < stars.transform.childCount; i++)
            {
                stars.transform.GetChild(i).gameObject.SetActive(false);
                if (currlevel.CurrStarScore > i)
                    StartCoroutine(ShowStar(i));
            }
        }
        else
        {
            levelClearedText.text = "Level Failed.";
            scoreText.text = "";
            highscoreText.text = "";
            stars.SetActive(false);
            failed.SetActive(true);
            if (!currlevel.Defeated)
                next.interactable = false;
        }
        currlevel.Currentscore = 0;

    }
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator ShowStar(int index)
    {
        yield return new WaitForSeconds(index * .4f);
        stars.transform.GetChild(index).gameObject.SetActive(true);
    }
}
