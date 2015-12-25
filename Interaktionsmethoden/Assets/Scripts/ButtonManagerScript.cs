using UnityEngine;
using System.Collections;

public class ButtonManagerScript : MonoBehaviour {

    public float buttonActiveTimer;
    private float counter;

    public ButtonIndicatorScript[] buttons;
    int length;
	// Use this for initialization
	void Start () {
        length = buttons.Length;
	}
	
	// Update is called once per frame
	void Update () {
	    if(counter < buttonActiveTimer)
        {
            counter += Time.deltaTime;
        }
        else
        {
            setButtonActive();
            counter = 0f;
        }
	}

    void setButtonActive()
    {
        int i = Random.Range(0, length);
        if(!buttons[i].active)
        {
            buttons[i].setActive();
        }
    }
}
