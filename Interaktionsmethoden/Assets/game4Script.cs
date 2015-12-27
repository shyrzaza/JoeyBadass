using UnityEngine;
using System.Collections;

public class game4Script : MonoBehaviour {


    public Game4PlayerController player;
    public Game4levelSpawnerScript spawner;

    private bool paused;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pauseGame(bool newPause)
    {
        paused = newPause;
        player.SetPause(paused);
        spawner.SetPause(paused);
    }
}
