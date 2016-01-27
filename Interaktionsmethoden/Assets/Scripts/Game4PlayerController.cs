using UnityEngine;
using System.Collections;

public class Game4PlayerController : MonoBehaviour {

    public float verticalSpeed;

    private Rigidbody rb;
    private float loseValue;
    public float mercyOffset;
    public float micThreshold;

    public GameObject ParticlePrefab;

    private bool controller;

    private bool paused;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        loseValue = transform.position.z - mercyOffset;
        controller = InputHandler.Instance.isActive;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (paused)
            return;

        if (!controller)
        {
            if (Input.GetButton("Game4"))
            {
                //Debug.Log("adding Force");
                rb.AddForce(Vector3.up * verticalSpeed, ForceMode.Acceleration);
                Instantiate(ParticlePrefab, gameObject.transform.position, Quaternion.identity);
            }
        }
        else
		{	
			//CONTROLLER
			if(Manager.getInstance().advancedon)
			{	
            	if(InputHandler.Instance.micValue >= micThreshold)
            	{
            	    rb.AddForce(Vector3.up * verticalSpeed * InputHandler.Instance.micValue, ForceMode.Acceleration);
            	}
			}
			else{
				if(InputHandler.Instance.buttonUpPressed)
				{
					rb.AddForce(Vector3.up * verticalSpeed * 1, ForceMode.Acceleration);
               //create jump particles
               Instantiate(ParticlePrefab, gameObject.transform.position, Quaternion.identity);
				}
			}
        }

        if(transform.position.z < loseValue)
        {
            GameManagerScript.Instance.lost(4);
        }
    }

    public void SetPause(bool newPause)
    {
        paused = newPause;
        if(rb)
        rb.isKinematic = paused;
    }
}
