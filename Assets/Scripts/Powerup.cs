using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	public float damage; 		//Change in Damage
	public float hp;			//Change in HPMax
    public float maxFuel;       //Change in fuelMax
	public float firerate; 		//Change in FireRate
	public float shotscale; 	//Change in Shotscale
	public float range; 		//Change in Shot Range
	public float shotspeed; 	//Change in Projectile Speed
	public float speed; 		//Change in Movement Speed
	public float scale;			//Change in Scale
	public string upgradename;			//Name of this Upgrade
	public string description;	//Description of this Upgrade
	public bool backshot = false;
	public bool quadshot = false;
	public bool spreadshot = false;
	public bool explosiveshot = false;
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
			p.Upgrade(damage,hp,maxFuel,firerate,shotscale,range,shotspeed,speed,scale);
			if(backshot){
				p.backshot = true;
			}
			if(quadshot){
				p.quadshot = true;
			}
			if(spreadshot){
				p.spreadshot = true;
			}
			if(explosiveshot){
				p.explosiveshot = true;
			}
		}
		GameController.Message (upgradename, description,3f);

		//remove this powerup
		Destroy (this.gameObject);
	}
}
