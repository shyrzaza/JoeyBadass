using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonIndicatorScript : MonoBehaviour {

    public int number;
    public bool active;

    public static float timeActive = 5f;
    private float timeCounter;

    public Color col;

    public Text text;

    public bool paused;

    private bool controller;

	// Use this for initialization
	void Start () {
        timeCounter = timeActive;
        controller = InputHandler.Instance.isActive;
	}
	
	// Update is called once per frame
	void Update () {
        if (paused)
            return;
	    if(active)
        {
            if (timeCounter > 0f)
            {
                timeCounter -= Time.deltaTime;
                text.text = timeCounter + "";
            }
            else
            {
                GameManagerScript.Instance.lost(3);
                Destroy(gameObject);
            }

            if (!controller)
            {
                if (Input.GetButtonDown(number + ""))
                {
                    active = false;
                    GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
            else
            {
                //Controller
                if(InputHandler.Instance.getButtonDowns()[number] == 1)
                {
                    active = false;
                    GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
        }
        else
        {
            text.text = "";
            if (Input.GetButtonDown(number + ""))
            {
                GameManagerScript.Instance.lost(3);
            }
        }
	}

    public void setActive()
    {
        timeCounter = timeActive;
        active = true;
        GetComponent<MeshRenderer>().material.color = col;
    }
}
