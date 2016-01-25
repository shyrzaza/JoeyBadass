using UnityEngine;
using System.Collections;

public class jumpparticlesKiller : MonoBehaviour {

	// Use this for initialization
	void Start () {
      Debug.Log("jump particles created");
      gameObject.GetComponent<ParticleSystem>().Play();
      waittoKill();
	}
	
   IEnumerator waittoKill()
   {
	 yield return new WaitForSeconds(2000);
    Debug.Log("jump particles killed");
    Destroy(gameObject);
	}


}
