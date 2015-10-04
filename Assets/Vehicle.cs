using System;
using UnityEngine;

public class Vehicle : Destructible
{
    public float damage;
    public float firerate;
    protected float firecd;
    public float shotscale;
    public float range;
    public float shotspeed;
	public GameObject shot;
	public Vector3 shotoffset;
	public Movable movement;

	public Vehicle() : base()
	{
        
	}
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
	public void Move(Vector3 direction){
		movement.Move (direction);
	}
    public void Fire(float angle){
        if (firecd <= 0)
        {
			float r = angle - transform.rotation.eulerAngles.y;
			transform.Rotate(new Vector3(0f,r,0f));
			movement.Rotate (-r);
			shot.GetComponent<Projectile>().Stats(transform.forward*shotspeed,damage,range,shotscale);
           	GameObject instantiatedProjectile = (GameObject)Instantiate (shot, transform.position+shotoffset, new Quaternion(0f,0f,0f,0f));
            firecd = 1 / firerate;
        }
    }
	public void Upgrade(float damage, float hp, float firerate, float shotscale, float range, float shotspeed, float speed, float scale){
		this.damage += damage;
		this.hpmax += hp;
		this.hp += hp;
		this.firerate += firerate;
		this.shotscale += shotscale;
		this.range += range;
		this.shotspeed += shotspeed;
		movement.maxspeed += speed;
		this.scale += scale;
	}
}
