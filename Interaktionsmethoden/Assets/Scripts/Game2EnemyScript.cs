using UnityEngine;
using System.Collections;

public class Game2EnemyScript : MonoBehaviour {

    public float speed;
    public float destroyDistance;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, speed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.gameObject.name);
        if (c.gameObject.name == "Player")
            GameManagerScript.Instance.lost(2);
    }

    void FixedUpdate()
    {
        if (transform.position.z >= destroyDistance)
            Destroy(gameObject);
    }

    public void SetPause(bool newPause)
    {
        GetComponent<Rigidbody>().isKinematic = newPause;
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, speed);
    }
}
