using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonIndicatorScript : MonoBehaviour {

    public float number;
    public bool active;

    public static float timeActive = 5f;
    private float timeCounter;

    public Color col;

    public Text text;

    public bool paused;

	// Use this for initialization
	void Start () {
        timeCounter = timeActive;
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

            if(Input.GetButtonDown(number+""))
            {
                active = false;
                GetComponent<MeshRenderer>().material.color = Color.white;
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
