using UnityEngine;
using System.Collections;

public class Game2PlayerController : MonoBehaviour {


    public float speed;
    public float top;
    public float bottom;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    
    void FixedUpdate()
    {
        float vSpeed = Input.GetAxisRaw("Game2");

        GetComponent<Rigidbody>().velocity = new Vector3(0f, vSpeed * speed, 0f);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, bottom, top), transform.position.z);
    }
}
