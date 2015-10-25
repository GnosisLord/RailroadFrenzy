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
	public bool aimless;	//Shoots randomly even when not facing player
	public float aimdelay; //time between turning towards Player and firing
	private float aiming; //time until Enemy starts firing
	//Destruct Variables
	public bool selfdestruct;	//Self Destructs when within destructrange
	public float destructrange;	//Range from player at which this will Self Destruct
	public float blastrange;	//Range at which destructibles are damaged when Self Destructing
	//Movement Variables
	public bool seeking; //Seeks player when aggroed
	public bool wandering; //Moves randomly when not aggroed
	private Vector3 desired; //Vector of targeted object
	private Player player;	//Reference to Player for Seeking and Aiming


	// Use this for initialization
	public void Start()
	{
		base.Start();
		friendly = false;
		player = GameController.get ().getPlayer ();
	}
	
	// Update is called once per frame
	public override void Update () {
		//Gets Player reference if it doesn't have it
		if (player == null) {
			player = GameController.get ().getPlayer ();
		}
		base.Update();
		if (aggroed) {
			//Seeking Behavior
			if(seeking){
				Move (Seek ());
			}
			//Aiming and Shooting Behavior
			if(aiming>0){
				aiming -= Time.deltaTime;
			}else{
				float arc = Vector3.Angle((player.transform.position - this.transform.position),transform.forward);
				if ((arc < 30 || aimless)&& shooting) {
					Fire ();
				}else{
					Face (FindDirection(player.gameObject));
					aiming = aimdelay;
				}
			}
			//Self Destruct Behavior
			if(selfdestruct){
				if(Aggro (destructrange)){
					Explode ();
				}
			}
		} if (aggrotimer <= 0) {
			//Check for Aggro
			aggrotimer = aggrodelay;
			aggroed = Aggro (aggrorange);
		} else {
			aggrotimer-=Time.deltaTime;
		}
		//Altitude adjustments
		if (transform.position.y >= 15) {
			gameObject.GetComponent<Rigidbody> ().useGravity = false;
		}
		if (transform.position.y >= 50) {
			Destroy ();
		}
		//Rights flipped vehicles on ground
		if (transform.position.y < 2 && (Math.Abs(transform.rotation.x) > 10 || Math.Abs (transform.rotation.z) > 10)) {
			transform.eulerAngles = new Vector3(0,transform.rotation.y,0);
		}
	}
	//Checks for Player object withing range
	public bool Aggro(float range){
		Collider[] nearby = Physics.OverlapSphere (transform.position, range);
		foreach (Collider obj in nearby) {
			if (obj.gameObject.GetComponent<Player>()!=null){
				aggrotimer = 10;
				return true;
			}
		}
		return false;
		
	}
	public override void Damage(float damage){
		base.Damage (damage);
		aggroed = true;
		aggrotimer = 10f;
	}
	//Determines angle to target at 45 degree increments
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
	//Moves in a random direction
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

	//Collision damages Player
	public void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.GetComponent<Vehicle>() != null)
		{
			if (col.gameObject.GetComponent<Vehicle>().friendly != friendly)
			{
				col.gameObject.GetComponent<Vehicle>().Damage(damage);
			}
		}
	}
	//Self Destruct Behavior, damages all destructibles within blastrange and destroys self
	void Explode(){
		Collider[] nearby = Physics.OverlapSphere (transform.position, blastrange);
		foreach (Collider obj in nearby) {
			if (obj.gameObject.GetComponent<Destructible> () != null) {
				obj.gameObject.GetComponent<Destructible>().Damage(damage);
			}
		}
		Destroy(this.gameObject);
	}
	public override void Destroy(){
		base.Destroy ();
	}
}
