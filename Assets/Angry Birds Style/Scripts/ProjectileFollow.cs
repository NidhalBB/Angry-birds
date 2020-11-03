using UnityEngine;
using System.Collections;

//	Attach this script to the Main Camera.
//	This script will set the transform values for the GameObject it is attached to.

public class ProjectileFollow : MonoBehaviour
{
    public GameControl control;
	public Transform projectile;        // The transform of the projectile to follow.
	public Transform farLeft;           // The transform representing the left bound of the camera's position.
	public Transform farRight;          // The transform representing the right bound of the camera's position.

    void Start()
    {
        control = GetComponent<GameControl>();
        TryFindProjectile();
    }
	void Update () 
    {
        if (control.CurrentGame != null)
        {
            if (projectile)
                UpdateCamera();
            else if (control.CurrentGame.CheckMovementStopped())
            {
                if(ResetCamera())
                TryFindProjectile();
            }
        }
        
	}

    void TryFindProjectile()
    {
        if (GameObject.FindGameObjectWithTag("Projectile"))
        {
            projectile = GameObject.FindGameObjectWithTag("Projectile").transform;
        }
    }

    void UpdateCamera()
    {
        Vector3 newPosition = transform.position;

        newPosition.x = projectile.position.x;
     
        newPosition.x = Mathf.Clamp(newPosition.x, farLeft.position.x, farRight.position.x);
        transform.position = newPosition;
    }

    bool ResetCamera()
    {
        Vector3 newPosition = farLeft.position;
        if (transform.position.x != farLeft.position.x)
        {
            newPosition.x -= Time.deltaTime;
            newPosition.z = -10f;
            newPosition.y = 0;
            newPosition.x = Mathf.Clamp(newPosition.x, farLeft.position.x, farRight.position.x);
            transform.position = newPosition;
            return false;
        }
        return true;

    }
}
