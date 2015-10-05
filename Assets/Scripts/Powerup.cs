using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	public float damage; 
	public float hp;
	public float firerate; 
	public float shotscale; 
	public float range; 
	public float shotspeed; 
	public float speed; 
	public float scale;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0f,2f,0f));
	}

	// Triggers on collision with object
	void OnTriggerEnter(Collider other)
	{
		Player p = other.GetComponent<Player> ();
		if (p != null) {
			p.Upgrade(damage,hp,firerate,shotscale,range,shotspeed,speed,scale);
		}

		//remove this powerup
		Destroy (this.gameObject);
	}
}
