using UnityEngine;
using System.Collections;

public class Game2PlayerController : MonoBehaviour {


    public float speed;
    public float top;
    public float bottom;

    public bool paused;

	// Use this for initialization
	void Start () {
	
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

        float vSpeed = Input.GetAxisRaw("Game2");

        GetComponent<Rigidbody>().velocity = new Vector3(0f, vSpeed * speed, 0f);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, bottom, top), transform.position.z);
    }
}
