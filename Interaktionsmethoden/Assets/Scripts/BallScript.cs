using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    public float deathRange;
    public float maximumSpeed;
    private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        if(transform.position.y <= deathRange)
        {
            GameManagerScript.Instance.lost(1);
            Destroy(gameObject);
        }


        /*
        float speed = Vector3.Magnitude(rigidbody.velocity);  // test current object speed

        if (speed > maximumSpeed)
        {
            float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease

            Vector3 normalisedVelocity = rigidbody.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rigidbody.AddForce(-brakeVelocity);  // apply opposing brake force
        }
         * */

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maximumSpeed);
    }
}
