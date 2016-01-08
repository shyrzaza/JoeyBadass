using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {


    public Camera[] cameras;

    public LineRenderer[] lines;

    public GameObject test;
    public bool gamePaused;
    private float timePlayed;

    public float[] gameStartTimes;

    public  float nextStartTime;
    public bool controller;

    //advanced mode bool
    private bool advancedmode = false;
    //current gamer'S name
    public string name = "";

    //GAMES
    public BoardScript game1Script;
    public game2Script game2Script;
    public ButtonManagerScript game3Script;
    public game4Script game4Script;

    Manager manager;

    public int status = 1;


    //Singleton Instanz
    public static GameManagerScript Instance { get; private set; }

    //SingletonPattern
    void Awake()
    {
        // First we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            Debug.Log("boom");
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
        controller = InputHandler.Instance.isActive;
        status = 1;
       
        cameras[0].rect = new Rect(0f, 0f, 1f, 1f); 
        cameras[1].rect = new Rect(0f, 0f, 0f, 0f);
        cameras[2].rect = new Rect(0f, 0f, 0f, 0f);
        cameras[3].rect = new Rect(0f, 0f, 0f, 0f);

       
        //PAUSE GAMES
        gamePaused = true;
        pauseGames(true,true,true,true);

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
        nextStartTime = gameStartTimes[0];
    }

    void Update()
    {
        if(gamePaused)
        {
            if (!controller)
            {
                //Wait for unpause
                if (Input.GetButtonDown("unpause"))
                {
                    //UNPAUSE
                    Debug.Log("unpaused");
                    gamePaused = false;
                    if (status == 1)
                        pauseGames(false, true, true, true);
                    if (status == 2)
                        pauseGames(false, false, true, true);
                    if (status == 3)
                        pauseGames(false, false, false, true);
                    if (status == 4)
                        pauseGames(false, false, false, false);
                }
            }
            else
            {
                //CONTROLLER
                if(InputHandler.Instance.getButtonDowns()[0] == 1)
                {
                    //UNPAUSE
                    Debug.Log("unpaused");
                    gamePaused = false;
                    if (status == 1)
                        pauseGames(false, true, true, true);
                    if (status == 2)
                        pauseGames(false, false, true, true);
                    if (status == 3)
                        pauseGames(false, false, false, true);
                    if (status == 4)
                        pauseGames(false, false, false, false);
                }
            }
        }
        else
        {
            timePlayed += Time.deltaTime;

            if(timePlayed > nextStartTime)
            {
                ChangeStatus(status+1);
            }
        }
    }
    public void lost(int number)
    {
        Debug.Log("lost  " + number);
        Manager.getInstance().addnewScore(timePlayed);
        //Manager.getInstance().saveToPrefs();
        Manager.getInstance().saveHighscore();
        Destroy(manager);
        Application.LoadLevel(0);
        
    }


    void ChangeStatus(int newStatus)
    {
        gamePaused = true;
        pauseGames(true,true,true,true);
        status = newStatus;
        if (status < 4)
            nextStartTime = gameStartTimes[status - 1];
        else
            nextStartTime = 10000000000000f;

        if(newStatus == 2)
        {
            StartCoroutine(CameraShrink(0, 0f, 0.5f, 1f, 0.5f));
            StartCoroutine(CameraShrink(1, 0f, 0f, 1f, 0.5f));
        }

        if(newStatus == 3)
        {
            StartCoroutine(CameraShrink(0, 0f, 0.5f, 0.5f, 0.5f));
            StartCoroutine(CameraShrink(1, 0f, 0f, 0.5f, 0.5f));
            StartCoroutine(CameraShrink(2, 0.5f, 0f, 0.5f, 0.5f));
        }
        if(newStatus == 4)
        {
            StartCoroutine(CameraShrink(3, 0.5f, 0.5f, 0.5f, 0.5f));
        }
    }

    IEnumerator CameraShrink(int index, float xOffset, float yOffset, float xScale, float yScale)
    {
        float xOffsetDistance = (cameras[index].rect.x - xOffset) / 100;
        float yOffsetDistance = (cameras[index].rect.y - yOffset) / 100;
        float xScaleDistance = (cameras[index].rect.width - xScale) / 100;
        float yScaleDistance = (cameras[index].rect.height - yScale) / 100;


        for (int i = 0; i < 100; i++)
        {
            cameras[index].rect = new Rect(cameras[index].rect.x - xOffsetDistance, cameras[index].rect.y - yOffsetDistance, cameras[index].rect.width - xScaleDistance, cameras[index].rect.height - yScaleDistance);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void pauseGames(bool game1, bool game2, bool game3, bool game4)
    {
        game1Script.pauseGame(game1);
        game2Script.pauseGame(game2);
        game3Script.pauseGame(game3);
        game4Script.pauseGame(game4);
    }
    

}
