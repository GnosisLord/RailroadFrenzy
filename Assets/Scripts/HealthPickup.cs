using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0f,2f,0f));
	}
	void OnTriggerEnter(Collider other)
	{
		Player p = other.GetComponent<Player> ();
		if (p != null) {
			p.Heal(1);
		}	
		//remove this powerup
		Destroy (this.gameObject);
	}
}
