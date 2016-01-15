using UnityEngine;
using System.Collections;

public class BoardScript : MonoBehaviour {

    public float rotateSpeed;
    public float slerpSpeed;
    public float maxSpeed;
    public GameObject ballPrefab;
    private GameObject ball;
    private bool paused;
    private bool controller;

	public float rotationRange;
	public float slerpScale;
	
	// current x and y euler angle
	float xAngle, yAngle;

	// Use this for initialization
	void Start () {
        StartGame();
        controller = InputHandler.Instance.isActive;
		Debug.Log ("Game 1 controller: " + controller);
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
            float xAxis = -1 * Input.GetAxis("Horizontal");
            float yAxis = Input.GetAxis("Vertical");

            //transform.rotation *= Quaternion.Euler(xAxis * rotateSpeed, 0f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(yAxis * rotateSpeed, 0f, xAxis * rotateSpeed), slerpSpeed);
        }
        else
        {
            //CONTROLLER
			float hAmt = InputHandler.Instance.accArr[1] * rotationRange; 
			float vAmt = InputHandler.Instance.accArr[0] * rotationRange; 
			
			// accumulate x and y euler angles
			xAngle = vAmt;
			yAngle = hAmt;
			
			
			// compute a new orientation from the euler angles
			Quaternion q = Quaternion.Euler(xAngle, 0, yAngle);
			
			// set new orientation via ridgid body
			//GetComponent<Rigidbody>().MoveRotation(q);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, slerpScale * Time.deltaTime);
        }
    }

    void StartGame()
    {
        ball = (GameObject)Instantiate(ballPrefab, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
        ball.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void pauseGame(bool newPaused)
    {
        if(ball)
            ball.GetComponent<Rigidbody>().isKinematic = newPaused;
        paused = newPaused;
    }
}
