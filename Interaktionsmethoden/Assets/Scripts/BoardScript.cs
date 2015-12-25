using UnityEngine;
using System.Collections;

public class BoardScript : MonoBehaviour {

    public float rotateSpeed;
    public float slerpSpeed;
    public float maxSpeed;
    public GameObject ballPrefab;
	// Use this for initialization
	void Start () {
        StartGame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        float xAxis = -1 * Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //transform.rotation *= Quaternion.Euler(xAxis * rotateSpeed, 0f, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(yAxis * rotateSpeed,0f,xAxis * rotateSpeed), slerpSpeed);
    }

    void StartGame()
    {
        Instantiate(ballPrefab, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
    }
}
