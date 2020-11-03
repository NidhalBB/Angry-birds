using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public GameObject projectilePrefab;

    Text scoreText, highscoreText;
    Text levelText;
    GameObject projectilesDisplay;
    UIManager mgr;
	// Use this for initialization
	void Start () 
    {
        mgr = FindObjectOfType<UIManager>();
        scoreText = transform.Find("Score").GetComponent<Text>();
        highscoreText = scoreText.transform.Find("Highscore").GetComponent<Text>();
        levelText = transform.Find("Level").GetComponent<Text>();
        projectilesDisplay = transform.Find("Projectiles").gameObject;
	}

    public void UpdateHUD(int score, int highscore, int level, int numprojectiles)
    {
        scoreText.text = "SCORE\n" + score.ToString();
        if (highscore > 0)
            highscoreText.text = "highscore\n" + highscore.ToString();
        else
            highscoreText.text = "";
        levelText.text = "LEVEL: " + level.ToString();

        UpdateProjectiles(numprojectiles);
    }

    void UpdateProjectiles(int count)
    {
        int currDisplayed = projectilesDisplay.transform.childCount;
        if (count != currDisplayed)
        {
            if (count < currDisplayed) //too many displayed- destroy one
            {
                for (int i = currDisplayed; i > count; i--)
                {
                    Destroy(projectilesDisplay.transform.GetChild(i - 1).gameObject);
                }
            }
            else if (count > currDisplayed) //too few displayed- display another
            {
                for (int i = currDisplayed; i < count; i++)
                {
                    GameObject temp = mgr.InitUIElement(projectilePrefab, projectilesDisplay.transform);
                    RectTransform rect = temp.GetComponent<RectTransform>();
                    rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 50 * i, 50);
                    rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 50);
                }
            }
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
