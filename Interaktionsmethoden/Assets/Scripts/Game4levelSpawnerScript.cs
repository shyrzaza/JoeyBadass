using UnityEngine;
using System.Collections;

public class Game4levelSpawnerScript : MonoBehaviour {


    public GameObject obstaclePrefab;
    public float spawnTime;
    private float counter;

    public float top;
    public float bottom;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(counter < spawnTime)
        {
            counter += Time.deltaTime;
        }
        else
        {
            spawnObs();
            counter = 0f;
        }
	}


    void spawnObs()
    {
        float yOffset = Random.RandomRange(top, bottom);
        Instantiate(obstaclePrefab, new Vector3(transform.position.x, yOffset, transform.position.z), Quaternion.identity);
    }
}
