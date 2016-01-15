using UnityEngine;
using System.Collections;

public class Game2PlayerController : MonoBehaviour {


    public float speed;
    public float top;
    public float bottom;

    public float slerpSpeed;

    public bool paused;
    private bool controller;
	// Use this for initialization
	void Start () {
        controller = InputHandler.Instance.isActive;
		Debug.Log ("Game 2 Controller: " + controller);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    
    void FixedUpdate()
    {
        if (paused)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
                return;
        }

        if (!controller)
        {
            float vSpeed = Input.GetAxisRaw("Game2");

            GetComponent<Rigidbody>().velocity = new Vector3(0f, vSpeed * speed, 0f);

            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, bottom, top), transform.position.z);
        }
        else
        {
            //CONTROLLER
            //float[4]
            float[] values = InputHandler.Instance.sliderArr;

            float distance = top - Mathf.Abs(bottom);
            float position = ((values[3]/2)+0.5f) * distance;
            position = bottom + position;

            transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, position, transform.position.z), slerpSpeed);
        }
    }
}
