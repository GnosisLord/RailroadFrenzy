using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Triggers on collision with object
	void OnTriggerEnter(Collider other)
	{
		//scale up object
		other.gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

		//remove this powerup
		Destroy (this.gameObject);
	}
}
