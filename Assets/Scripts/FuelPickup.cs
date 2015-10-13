using UnityEngine;
using System.Collections;

public class FuelPickup : MonoBehaviour {
	public float amount = 100f; //Amount of fuel restored
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(2f,0f,0f));
	}

	//Collision method, calls refuel in Player
	void OnTriggerEnter(Collider other)
	{
		Player p = other.GetComponent<Player> ();
		if (p != null) {
			p.refuel (amount);
		}	
		//remove this powerup
		Destroy (this.gameObject);
	}
}
