using System;
using UnityEngine;

public class Enemy : Vehicle {
	//Aggro Variables
	public bool aggroed; //Enemy has detected Player
	public float aggrorange; //range at which the Enemy scans for Player
	public float aggrodelay; //time detween checks for aggro
	private float aggrotimer; //time remaining until next aggro check
	//Firing Variables
	public bool shooting; //Shoots at player when aggroed
	public float aimdelay; //time between turning towards Player and firing
	private float aiming; //time until Enemy starts firing
	//Destruct Variables
	public bool selfdestruct;
	public float destructrange;
	public float blastrange;
	//Movement Variables
	public bool seeking; //Seeks player when aggroed
	public bool wandering; //Moves randomly when not aggroed
	private Vector3 desired;
	private Player player;


	// Use this for initialization
	public void Start()
	{
		base.Start();
		friendly = false;
		player = GameController.get ().getPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameController.get ().getPlayer ();
		}
		base.Update();
		if (aggroed) {
			if(seeking){
				Move (Seek ());
			}
			if(aiming>0){
				aiming -= Time.deltaTime;
			}else{
				float arc = Vector3.Angle((player.transform.position - this.transform.position),transform.forward);
				if (arc < 30 && shooting) {
					Fire ();
				}else{
					Face (FindDirection(player.gameObject));
					aiming = aimdelay;
				}
			}
			if(selfdestruct){
				if(Aggro (destructrange)){
					Explode ();
				}
			}
		} else if (aggrotimer <= 0) {
			aggroed = Aggro (aggrorange);
			aggrotimer = aggrodelay;
		} else {
			aggrotimer-=Time.deltaTime;
		}




	}
	public bool Aggro(float range){
		Collider[] nearby = Physics.OverlapSphere (transform.position, range);
		foreach (Collider obj in nearby) {
			if (obj.gameObject.GetComponent<Player>()!=null){
				return true;
			}
		}
		return false;
		
	}
	public float FindDirection(GameObject target){
		Vector3 adjusted = transform.rotation * (target.transform.position - this.transform.position);
		float zangle = Vector3.Angle(adjusted,transform.forward);
		float xangle = Vector3.Angle (adjusted, transform.right);
		if (zangle < 30) {
			return 0f;
		} else if (zangle < 60) {
			if (xangle<=60) {
				return 45f;
			}else{
				return 315f;
			}
		}
		if (zangle >= 150) {
			return 180f;
		} else if (zangle > 120) {
			if (xangle <= 60) {
				return 135f;
			} else {
				return 225f;
			}
		}
		if (xangle<=30) {
			return 90f;
		}
		return 270f;
	}
	public void Wander(){
		if (movement.idle) {
			Vector3 direction = new Vector3 (0f,0f,0f);;
			switch(GameController.Random (9)){
			case 8:
				direction = new Vector3 (0f,0f,1f);
				break;
			case 7:
				direction = new Vector3 (1f,0f,0f);
				break;
			case 6:
				direction = new Vector3 (1f,0f,1f);
				break;
			case 5:
				direction = new Vector3 (0f,0f,-1f);
				break;
			case 4:
				direction = new Vector3 (-1f,0f,0f);
				break;
			case 3:
				direction = new Vector3 (-1f,0f,-1f);
				break;
			case 2:
				direction = new Vector3 (-1f,0f,1f);
				break;
			case 1:
				direction = new Vector3 (1f,0f,-1f);
				break;
			}
			Move (direction);
		}
	}
	// Seek the target
	Vector3 Seek(){
		// vector between the target and the enemy
		desired = player.transform.position - this.transform.position;

		// nullify y translation
		desired.y = 0;

		// determine smaller axis to travel
		if (Math.Abs (desired.x) > Math.Abs (desired.z)) {
			desired.z = 0;
			return desired;
		} 
		else {
			desired.x = 0;
			return desired;
		}
	}
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.GetComponent<Vehicle>() != null)
		{
			if (col.gameObject.GetComponent<Vehicle>().friendly != friendly)
			{
				col.gameObject.GetComponent<Vehicle>().Damage(damage);
			}
		}
	}
	void Explode(){
		Collider[] nearby = Physics.OverlapSphere (transform.position, blastrange);
		foreach (Collider obj in nearby) {
			if (obj.gameObject.GetComponent<Destructible> () != null) {
				obj.gameObject.GetComponent<Destructible>().Damage(damage);
			}
		}
		Destroy(this.gameObject);
	}
}
