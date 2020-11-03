using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    GameObject[] levelPrefabs;
    [SerializeField]
    GameObject canvasPrefab;


    private Transform start;
    private GameObject projectile;

    public GameObject currLevelObj;
    public int targetsRemaining;
    public bool gameOver;
    public int numProjectiles;
    
    // Use this for initialization
    void Start()
    {
        gameOver = false;
        start = GameObject.Find("Game").transform.Find("Start");
        projectile = GameObject.FindGameObjectWithTag("Projectile");
    }

    public void InitLevel(int level, int numprojectiles)
    {
        if (currLevelObj == null && GameObject.FindGameObjectWithTag("Level") == null)
        {
            if (!gameOver)
            {
                numProjectiles = numprojectiles;
                currLevelObj = Instantiate(levelPrefabs[level]) as GameObject;
                currLevelObj.transform.SetParent(transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        UpdateGameOver();
        UpdateProjectile();

    }




    void UpdateGameOver()
    {
        //Update targets remaining
        targetsRemaining = GameObject.FindGameObjectsWithTag("Target").Length;

        if (targetsRemaining == 0 && currLevelObj && !gameOver && !projectile)
        {
            if (CheckMovementStopped() )
            {
                StartCoroutine(WaitToDestroy(.8f));
            }

        }

    }

    void UpdateProjectile()
    {
        //If we haven't assigned a projectile and if one does not exist in the scene
        if (projectile == null && GameObject.FindGameObjectWithTag("Projectile") == null)
        {
            if (!gameOver && currLevelObj && targetsRemaining > 0 && numProjectiles > 0)
            {
                numProjectiles--;
                projectile = Instantiate(projectilePrefab, start.position, start.rotation) as GameObject;

            }
            else if (numProjectiles == 0)
                StartCoroutine(WaitToDestroy(0f));
        }

    }



    public IEnumerator WaitToDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        if (CheckMovementStopped())
        {
            gameOver = true;
            Debug.Log("No more targets- clearing level");
            Destroy(projectile.gameObject);
            Destroy(currLevelObj.gameObject);
        }
        else
        {
            Debug.Log("Still moving");
        }
    }
    /// <summary>
    /// bodies -> Runs through array of all Rigidbodies to find which ones are above ground.
    /// check -> Then runs through the list to see if they've all stopped moving.
    /// </summary>
    /// <returns></returns>
    public bool CheckMovementStopped()
    {
        Rigidbody2D[] bodies = FindObjectsOfType<Rigidbody2D>();
        List<Rigidbody2D> checkBodies = new List<Rigidbody2D>();

        foreach (Rigidbody2D body in bodies)
        {
            if (body.position.y > -8 && body.gameObject.tag == "Damager")
            {
                checkBodies.Add(body);
            }

        }

        int count = 0;
        int compare = checkBodies.Count;
        foreach (Rigidbody2D body in checkBodies)
        {
            if (body.velocity == new Vector2(0, 0))
            {
                count++;
            }
        }

        if (count == compare)
        {
            return true;
        }

        return false;
    }
}
