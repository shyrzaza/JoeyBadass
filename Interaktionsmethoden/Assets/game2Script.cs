using UnityEngine;
using System.Collections;

public class game2Script : MonoBehaviour {

    public Game2PlayerController player;
    public Game2SpawnerScript spawner;

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
        player.paused = paused;
        spawner.SetPause(paused);
    }
}
