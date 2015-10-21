using System;
using UnityEngine;

public class Vehicle : Destructible
{
    public float damage;		//Damage dealt per hit
    public float firerate;		//Shots fired per second
    protected float firecd;		//Cooldown until next shot
    public float shotscale;		//Scale of projectile relative to this
    public float range;			//Projectile range in seconds
    public float shotspeed;		//Projectile velocity
	public GameObject shot;		//Prefab of Projectile
	public Vector3 shotoffset;	//Position offset when Projectile spawns
	public Movable movement;	//Movement script of this gameObject

	public bool explosiveshot;	//Fires explosive projectiles
	public bool homingshot;		//Fires homing projectiles
	public bool backshot;		//Fires forward and backward simultaneously
	public bool quadshot;		//Fires in 4 directions simultaneously
	public bool spreadshot;		//Fires 3 projectiles at forward angles
	public float fueldecay = .5f;		//Amount of Fuel consumed per second
	public float fuelmax = 100f;		//Maximum Fuel level
	public float fuel;        //Current Fuel, acts as a timer

    public void Start()
    {
        base.Start();
        firecd = 0f;

    }
    public void Update()
    {
        base.Update();
        if(firecd>0){
            firecd -= Time.deltaTime;
        }
    }
	//Moves in the given direction
	public void Move(Vector3 direction){
		movement.Move (direction);
	}
	//Face the given angle (screen relative)
	public void Face(float angle){
		float r = angle - transform.rotation.eulerAngles.y;
		transform.Rotate(new Vector3(0f,r,0f));
		movement.Rotate (-r);
	}
	//Fire Shot forward
    public void Fire(){
        if (firecd <= 0)
        {
			shot.GetComponent<Projectile>().Stats(transform.forward*shotspeed,damage,range,shotscale);
			shot.GetComponent<Projectile>().Special (homingshot,explosiveshot);
           	Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f,0f,0f,0f));
			if(spreadshot){//Spreadshot fires 2 more projectiles
				Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f, 0.5f,0f, 0.866f));
				Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f, -0.5f,0f, 0.866f));
			}
			if (quadshot) {//Quadshot fires 3 more projectiles
				Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f, 0.7071f,0f, 0.7071f));
				Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f,1f,0f,0f));
				Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f, -0.7071f,0f, 0.7071f));
			}else if(backshot){//Backshot fires another projectile
				Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f,1f,0f,0f));
			}
            firecd = 1 / firerate;
        }

    }
	//Increases stats, called by upgrades
	public void Upgrade(float damage, float hp, float maxFuel,float firerate, float shotscale, float range, float shotspeed, float speed, float scale){
		this.damage += damage;
		this.hpmax += hp;
		this.hp += hp;
        this.fuelmax += maxFuel;
		this.firerate += firerate;
		this.shotscale += shotscale;
		this.range += range;
		this.shotspeed += shotspeed;
		movement.maxspeed += speed;
		this.scale += scale;
		gameObject.GetComponent<Rigidbody> ().mass *= scale;
	}
	//Destruction of this vehicle, informs controller
	public void Destroy()
	{
		GameController.get ().Death (this);
		Destroy(gameObject);
	}
}
