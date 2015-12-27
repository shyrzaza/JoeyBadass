using UnityEngine;
using System.Collections;

public class Game4ObstacleScript : MonoBehaviour {

    public float horizontalSpeed;
    public float deathRange;
    public bool paused;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (paused)
            return;

        transform.Translate(new Vector3(0f,0f,-1f) * horizontalSpeed);
        if(transform.position.z < deathRange)
        {
            Destroy(gameObject);
        }
    }
}
