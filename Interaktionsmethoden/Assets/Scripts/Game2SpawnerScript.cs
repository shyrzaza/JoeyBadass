using UnityEngine;
using System.Collections;

public class Game2SpawnerScript : MonoBehaviour {

    private float counter;
    public float spawnTime;
    public float bottom;
    public float top;
    public bool paused;

    public GameObject game2EnemyPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (paused)
            return;
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

    public void SetPause(bool newPause)
    {
        paused = newPause;
        Game2EnemyScript[] enemies = Collider.FindObjectsOfType<Game2EnemyScript>();
        foreach(Game2EnemyScript e in enemies)
        {
            e.SetPause(paused);
        }
    }
}
