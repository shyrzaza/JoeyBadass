using UnityEngine;
using System.Collections;

public class Game2SpawnerScript : MonoBehaviour {

    private float counter;
    public float spawnTime;
    public float bottom;
    public float top;

    public GameObject game2EnemyPrefab;

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
            spawn();
            counter = 0f;

        }
	}

    void spawn()
    {
        float randomY = Random.Range(top, bottom);
        Instantiate(game2EnemyPrefab, new Vector3(transform.position.x, randomY, transform.position.z), Quaternion.identity);
        Debug.Log("Spawned");
    }
}
