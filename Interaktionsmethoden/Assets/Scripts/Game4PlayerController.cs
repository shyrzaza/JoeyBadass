using UnityEngine;
using System.Collections;

public class Game4PlayerController : MonoBehaviour {

    public float verticalSpeed;
    private Rigidbody rb;
    private float loseValue;
    public float mercyOffset;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        loseValue = transform.position.z - mercyOffset;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if(Input.GetButton("Game4"))
        {
            //Debug.Log("adding Force");
            rb.AddForce(Vector3.up * verticalSpeed, ForceMode.Acceleration);
        }

        if(transform.position.z < loseValue)
        {
            GameManagerScript.Instance.lost(4);
        }
    }
}
