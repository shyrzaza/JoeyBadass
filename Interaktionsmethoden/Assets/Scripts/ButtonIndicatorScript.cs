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

    public GameObject particlesys;

    private ParticleSystem runningparticlesys;
    private bool controller;

	// Use this for initialization
	void Start () {
        timeCounter = timeActive;
        text.color = Color.white;
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
                if (Input.GetButtonDown(number +1 + ""))
                {
                   deactivate();
                }
            }
            else
            {

                //Controller
				if(number == 0)
				{
					if(InputHandler.Instance.getButtonDowns()[5] == 1)
					{
                  deactivate();
					}
				}
				if(number == 1)
				{
					if(InputHandler.Instance.getButtonDowns()[0] == 1)
					{
                  deactivate();
					}
				}
				if(number == 2)
				{
					if(InputHandler.Instance.getButtonDowns()[1] == 1)
					{
                  deactivate();
					}
				}
				if(number == 3)
				{
					if(InputHandler.Instance.getButtonDowns()[4] == 1)
					{
                  deactivate();
					}
				}
            }
        }
        else
        {
            text.text = "";
			if(!controller)
			{
            	if (Input.GetButtonDown(number +1 + ""))
            	{
            	    GameManagerScript.Instance.lost(3);
            	}
			}
			else{
				//Controller
				if(number == 0)
				{
					if(InputHandler.Instance.getButtonDowns()[5] == 1)
					{
						GameManagerScript.Instance.lost(3);
					}
				}
				if(number == 1)
				{
					if(InputHandler.Instance.getButtonDowns()[0] == 1)
					{
						GameManagerScript.Instance.lost(3);
					}
				}
				if(number == 2)
				{
					if(InputHandler.Instance.getButtonDowns()[1] == 1)
					{
						GameManagerScript.Instance.lost(3);
					}
				}
				if(number == 3)
				{
					if(InputHandler.Instance.getButtonDowns()[4] == 1)
					{
						GameManagerScript.Instance.lost(3);
					}
				}
			}
        }
	}

   public void deactivate()
   {
      //buggen noch
      //Destroy(runningparticlesys);
      active = false;
      GetComponent<MeshRenderer>().material.color = Color.white;
   }

    public void setActive()
    {
        timeCounter = timeActive;
        active = true;
       //particle buggen noch
       /*  runningparticlesys = (ParticleSystem) Instantiate(particlesys, gameObject.transform.position,  Quaternion.identity);
        runningparticlesys.startColor = col;
        runningparticlesys.Simulate(0.2f, false, true);*/
        GetComponent<MeshRenderer>().material.color = col;
    }
}
