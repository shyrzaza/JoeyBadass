using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {


    public Camera[] cameras;

    public LineRenderer[] lines;

    public GameObject test;

    //Singleton Instanz
    public static GameManagerScript Instance { get; private set; }

    //SingletonPattern
    void Awake()
    {
        // First we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }

        // Here we save our singleton instance
        Instance = this;

        // Furthermore we make sure that we don't destroy between scenes (this is optional)
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        /*
        for (int i = 0; i < 4; i++)
        {
            //Instantiate(test, game1Cam.ViewportToWorldPoint(new Vector3(0, 0, 100)), Quaternion.identity);
            lines[i*2].SetPosition(0, cameras[i].ViewportToWorldPoint(new Vector3(0.0f, 0.01f, 100)));
            lines[i*2].SetPosition(1, cameras[i].ViewportToWorldPoint(new Vector3(1f, 0.01f, 100)));

            lines[i*2+1].SetPosition(0, cameras[i].ViewportToWorldPoint(new Vector3(0.999f, 1f, 100)));
            lines[i*2+1].SetPosition(1, cameras[i].ViewportToWorldPoint(new Vector3(0.999f, 0.00f, 100)));
        }
         * */
    }
    public void lost(int number)
    {
        Debug.Log("lost  " + number);
    }






}
